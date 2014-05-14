/* global epaModule */
'use strict';

var calculateScore = function ($scope, kpiInfo) {
	var totalGroupScore = 0;
	var totalGroupWeight = 0;
	$scope.finalScore = 0;

	$.each(kpiInfo, function (item) {
		var kpiGroupItem = kpiInfo[item].kpiGroupItems;
		var kpiGroupWeight = kpiInfo[item].kpiGroupWeight;
		var totalItemScore = 0;
		var totalItemWeight = 0;
		$.each(kpiGroupItem, function (i) {
			var currentItem = kpiGroupItem[i];
			if (currentItem.score > 0) {
				totalItemWeight += currentItem.weight;
				totalItemScore += currentItem.score * currentItem.weight;
			}
		});

		var temp = (totalItemScore / totalItemWeight) * kpiGroupWeight;
		if (isNaN(temp)) {
			totalGroupScore += 0;
		}
		else {
			totalGroupScore += temp;
			totalGroupWeight += kpiGroupWeight;
		}

		kpiInfo[item].kpiGroupScore = temp;
		//kpiInfo[item].groupFinalScore = ((temp / kpiGroupWeight) * 10).toFixed(1);
	});

	$.each(kpiInfo, function (item) {
		var currentKPIInfo = kpiInfo[item];
		currentKPIInfo.groupFinalScore = ((currentKPIInfo.kpiGroupScore / totalGroupWeight) * 10).toFixed(1);
	});

	$scope.finalScore = ((totalGroupScore / totalGroupWeight) * 10).toFixed(1);

	if (isNaN($scope.finalScore)) {
		$scope.finalScore = 0;
	}
};

epaModule.controller('EmployeeEPAController', ['$scope', 'employeeEPAFactory', 'notificationFactory',
'$routeParams', '$window', 'ErrorService', '$modal',
function ($scope, employeeEPAFactory, notificationFactory, $routeParams, $window, ErrorService, $modal) {
	/**
	* Scope variables. 
	*/
	$scope.max = 10;
	$scope.isReadonly = false;
	var targetId = $routeParams.targetId;
	var employeeId = $routeParams.employeeId;
	var currentEPAStatus;
	var newEPAStatus;
	$scope.showMenuContext = true;

	/**
	* Function. 
	*/

	var checkStatus = function (status) {
		if (status === 1) {
			$scope.isReadonly = true;
			$scope.hoveringOver = null;
			$scope.hoveringLeave = null;
		}
		else {
			$scope.isReadonly = false;
			// Rating Star Hover.
			$scope.hoveringOver = function (value, object) {
				object.percent = (100 * value) / $scope.max;
			};

			// Rating Star Mouse Leave
			$scope.hoveringLeave = function (item) {
				item.percent = (100 * item.score) / $scope.max;
				calculateScore($scope, $scope.employeeEPA.kpiInfo);
			};
		}
	};

	/**
	* Ajax event. 
	*/
	var successAddOrUpdateEPA = function () { notificationFactory.success('SuccessSaveData', ''); };
	var errorAddOrUpdateEPA = function (response) { notificationFactory.error('', response); };
	var successGetEmployeeEPA = function (data) {
		checkStatus(data.status);
		$scope.employeeEPA = data;
		$scope.finalScore = data.finalScore;
		var roleArray = [];
		$.each(data.projectRoles, function (i) {
			roleArray.push(data.projectRoles[i].name);
		});
		$scope.employeeRole = roleArray.join(", ");

		calculateScore($scope, data.kpiInfo);
	};

	var successUpdateEPAStatus = function () {
		notificationFactory.success('SuccessSaveData', '');
		checkStatus(newEPAStatus);
	};
	var errorUpdateEPAStatus = function (response) {
		$scope.employeeEPA.status = currentEPAStatus;
		checkStatus(currentEPAStatus);
		notificationFactory.error('', response);
	};

	var errorGetEmployeeEPA = function (status) {
		notificationFactory.error('', status);
		ErrorService.errorHandle(status);
	};

	/**
	* Initialize data to view Edit Project. 
	*/
	employeeEPAFactory.getEmployeeEPA(targetId, employeeId).then(successGetEmployeeEPA, errorGetEmployeeEPA);

	/**
	* Events 
	*/
	$scope.save = function (employeeEPAResult) {
		var allowSubmit = true;
		if (employeeEPAResult.comment === "") {
			allowSubmit = false;
			employeeEPAResult.checkNullComment = true;
		}
		else {
			employeeEPAResult.checkNullComment = false;
			$.each(employeeEPAResult.kpiInfo, function (item) {
				var kpiGroupItem = employeeEPAResult.kpiInfo[item].kpiGroupItems;
				$.each(kpiGroupItem, function (i) {
					var currentItem = kpiGroupItem[i];
					if (currentItem.isRequireComment && currentItem.comment === "") {
						currentItem.checkNullComment = true;
						allowSubmit = false;
					}
					else {
						currentItem.checkNullComment = false;
					}
				});
			});
		}

		if (allowSubmit) {
			employeeEPAResult.finalScore = $scope.finalScore;
			employeeEPAResult.targetId = targetId;
			employeeEPAFactory.addEmployeeEPA(employeeEPAResult).then(successAddOrUpdateEPA, errorAddOrUpdateEPA);
		}
	};

	//Cancel Rating
	$scope.cancelRating = function (item) {
		item.score = 0;
		item.percent = 0;
		calculateScore($scope, $scope.employeeEPA.kpiInfo);
	};

	// Back to previous history link.
	$scope.cancel = function () { $window.history.back(); };

	// EPA Status Context Menu
	$scope.epaStatus = [{ name: 'Open', id: 0 }, { name: 'Closed', id: 1}];
	$scope.toggleDropdown = function ($event) {
		$event.preventDefault();
		$event.stopPropagation();
		$scope.status.isopen = !$scope.status.isopen;
	};

	$scope.updateEPAStatus = function (employeeEPA, status) {
		var updateEPAModalInstance = $modal.open({
			templateUrl: '/templates/Common/ModalConfirmContent.html',
			controller: "ModalConfirmController",
			resolve: {
				name: function () {
					return "";
				},
				action: function () {
					return "change status";
				}
			}
		});
		updateEPAModalInstance.result.then(function () {
			currentEPAStatus = employeeEPA.status;
			newEPAStatus = status;
			employeeEPA.status = status;
			employeeEPA.targetId = targetId;
			employeeEPAFactory.updateEPAStatus(employeeEPA).then(successUpdateEPAStatus, errorUpdateEPAStatus);
		});
	};

} ]);

epaModule.controller('MyEPAController', ['$scope', 'employeeEPAFactory', 'notificationFactory',
'$routeParams', '$window', 'ErrorService',
function ($scope, employeeEPAFactory, notificationFactory, $routeParams, $window, ErrorService) {
	/**
	* Scope variables. 
	*/
	$scope.max = 10;
	$scope.isReadonly = true;
	$scope.showMenuContext = false;
	var targetId = $routeParams.targetId;
	var employeeId = $routeParams.employeeId;

	/**
	* Ajax event. 
	*/
	var successGetEmployeeEPA = function (data) {
		$scope.employeeEPA = data;
		$scope.finalScore = data.finalScore;
		var roleArray = [];
		$.each(data.projectRoles, function (i) {
			roleArray.push(data.projectRoles[i].name);
		});
		$scope.employeeRole = roleArray.join(", ");

		calculateScore($scope, data.kpiInfo);
	};

	var errorHandle = function (status) {
		ErrorService.errorHandle(status);
	};

	/**
	* Initialize data to view. 
	*/
	employeeEPAFactory.getMyEPA(targetId, employeeId).then(successGetEmployeeEPA, errorHandle);

	/**
	* Events 
	*/

	$scope.cancel = function () { $window.history.back(); };

} ]);

// View EPA of Current Member Controller
epaModule.controller('ViewCurrentMemberEPAController',
	['$scope', '$location', 'employeeEPAFactory', 'commonFactory', 'ErrorService', 'notificationFactory',
	function ($scope, $location, employeeEPAFactory, commonFactory, ErrorService, notificationFactory) {

		// get all project to filter
		employeeEPAFactory.getAllProject().then(function (projectList) {
			$scope.projectOptions = projectList;
		});

		// get all status to filter
		employeeEPAFactory.getAllStatus().then(function (statusList) {
			$scope.statusOptions = statusList;
		});

		var successGetEPA = function (listEPA) {
			$scope.listEPA = listEPA;
			$scope.submitted = false;
		};

		var errorHandle = function (response, status) {
			if (status === 405) {
				ErrorService.errorHandle(status);
			}
			else {
				notificationFactory.error('', "ErrorMessage");
			}

			$scope.submitted = false;
		};

		// get all EPA of current member
		var getEPAOfCurrentMember = function (projectId, status) {
			employeeEPAFactory.getEPAOfCurrentMember(projectId, status).then(successGetEPA, errorHandle);
		};

		getEPAOfCurrentMember(null, null);

		// view detail of EPA
		$scope.EPADetail = function (epaItem) {
			if (epaItem.target) {
				var targetId = epaItem.target.targetId,
				employeeId = epaItem.employeeId;
				$scope.viewEPADetailUrl = '/MyEPA/Index' + '?targetId=' + targetId + '&employeeId=' + employeeId;
			}
		};

		// filter EPA
		$scope.query = function (filter) {
			$scope.submitted = true;

			if (filter) {
				getEPAOfCurrentMember(filter.projectId, filter.status);
			}
			else {
				$scope.submitted = false;
			}
		};
	} ]);

// View EPA of Member by Manager Controller
	epaModule.controller('ViewMemberByManagerEPAController', [
	'$scope', '$location', 'employeeEPAFactory', 'ErrorService', 'commonFactory', 'notificationFactory', '$modal',
	function ($scope, $location, employeeEPAFactory, ErrorService, commonFactory, notificationFactory, $modal) {

		// get all project to filter
		employeeEPAFactory.getAllProject().then(function (projectList) {
			$scope.projectOptions = projectList;
		});

		// get all status to filter
		employeeEPAFactory.getAllStatus().then(function (statusList) {
			$scope.statusOptions = statusList;
		});

		var successGetEPA = function (listEPA) {
			$scope.listEPA = listEPA;
			$scope.submitted = false;
		};

		var successChangeEPAStatus = function () {
			notificationFactory.success('SuccessSaveData', '');
			getEPAOfEmployeeByManager(null, null);
		};
		var errorChangeEPAStatus = function (response) {
			notificationFactory.error('', response);
			getEPAOfEmployeeByManager(null, null);
		};

		var errorHandle = function () {
			if (status === 405) {
				ErrorService.errorHandle(status);
			}
			else {
				notificationFactory.error('', "ErrorMessage");
			}

			$scope.submitted = false;
		};

		// get EPA of Employee by manager
		var getEPAOfEmployeeByManager = function (projectId, status) {
			employeeEPAFactory.getEPAOfEmployeeByManager(projectId, status).then(successGetEPA, errorHandle);
		};

		getEPAOfEmployeeByManager(null, null);

		// filter EPA
		$scope.query = function (filter) {
			$scope.submitted = true;

			if (filter) {
				getEPAOfEmployeeByManager(filter.projectId, filter.status);
			}
			else {
				$scope.submitted = false;
			}
		};

		// view detail of EPA
		$scope.EPADetail = function (epaItem) {
			if (epaItem.target) {
				var targetId = epaItem.target.targetId,
				employeeId = epaItem.employeeId;
				$scope.viewEPADetailUrl = '/EmployeeEPA/Index' + '?targetId=' + targetId + '&employeeId=' + employeeId;
			}
		};
		// EPA Status Context Menu
		$scope.epaStatus = [{ name: 'Open', id: 0 }, { name: 'Closed', id: 1}];
		$scope.toggleDropdown = function ($event) {
			$event.preventDefault();
			$event.stopPropagation();
			$scope.status.isopen = !$scope.status.isopen;
		};

		$scope.updateEPAStatus = function (epaItem, status) {
			var updateEPAModalInstance = $modal.open({
				templateUrl: '/templates/Common/ModalConfirmContent.html',
				controller: "ModalConfirmController",
				resolve: {
					name: function () {
						return "";
					},
					action: function () {
						return "change status";
					}
				}
			});
			updateEPAModalInstance.result.then(function () {
				epaItem.status = status;
				epaItem.targetId = epaItem.target.targetId;
				employeeEPAFactory.updateEPAStatus(epaItem).then(successChangeEPAStatus, errorChangeEPAStatus);
			});
		};

	} ]);