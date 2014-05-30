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
				totalItemScore += currentItem.score * 2 * currentItem.weight;
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
	$scope.max = 5;
	$scope.isReadonly = false;
	var targetId = $routeParams.targetId;
	var employeeId = $routeParams.employeeId;
	var currentEPAStatus;
	var newEPAStatus;
	$scope.showMenuContext = true;
	$scope.isPMPage = true;

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
	var successAddOrUpdateEPA = function () {
		notificationFactory.success('SuccessSaveData', '');
		$scope.dataSaved = true;
	};
	var errorAddOrUpdateEPA = function (response) { notificationFactory.error('', response); };
	var successGetEmployeeEPA = function (data) {
		if (data !== "null") {
			if (data.kpiInfo.length > 0) {
				$.each(data.kpiInfo, function (i) {
					var currentKPIInfo = data.kpiInfo[i];
					var totalGroupItems = currentKPIInfo.kpiGroupItems.length;
					var averageMark = 100 / totalGroupItems;
					if (currentKPIInfo.isSpreadKPI && totalGroupItems > 0) {
						var currentKPIGroup = currentKPIInfo.kpiGroupItems;
						$.each(currentKPIGroup, function (k) {
							currentKPIGroup[k].weight = averageMark;
						});
					}
				});
			}

			if (data.permission === 2) {
				checkStatus(data.status);
			}
			else {
				$scope.isReadonly = true;
				$scope.showMenuContext = false;
			}

			$scope.employeeEPA = data;
			$scope.finalScore = data.finalScore;
			var roleArray = [];
			$.each(data.projectRoles, function (i) {
				roleArray.push(data.projectRoles[i].name);
			});
			$scope.employeeRole = roleArray.join(", ");

			calculateScore($scope, data.kpiInfo);
		}
		else {
			notificationFactory.error('', 'There is no EPA content available !');
		}
	};

	var successUpdateEPAStatus = function (response) {
		if (response.data !== "") {
			notificationFactory.error('', response.data);
		}
		else {
			notificationFactory.success('SuccessSaveData', '');
		}
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

	$scope.generalCommentValid = true;

	$scope.save = function (employeeEPAResult) {
		$scope.itemCommentValid = true;

		if (employeeEPAResult.comment === "" || employeeEPAResult.comment === null) {
			$scope.generalCommentValid = false;
		}
		else {
			$scope.generalCommentValid = true;
		}

		$.each(employeeEPAResult.kpiInfo, function (item) {
			var kpiGroupItem = employeeEPAResult.kpiInfo[item].kpiGroupItems;
			$.each(kpiGroupItem, function (i) {
				var currentItem = kpiGroupItem[i];
				if (currentItem.isRequireComment && currentItem.score > 0
				&& (currentItem.comment === "" || currentItem.comment === null)) {
					currentItem.checkNullComment = true;
					$scope.itemCommentValid = false;
				}
				else {
					currentItem.checkNullComment = false;
				}
			});
		});

		if ($scope.generalCommentValid && $scope.itemCommentValid) {
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
	$scope.max = 5;
	$scope.isReadonly = true;
	$scope.showMenuContext = false;
	var targetId = $routeParams.targetId;
	var employeeId = $routeParams.employeeId;
	$scope.generalCommentValid = true;
	$scope.isPMPage = false;

	/**
	* Ajax event. 
	*/
	var successGetEmployeeEPA = function (data) {
		if (data != null) {
			if (data.kpiInfo.length > 0) {
				$.each(data.kpiInfo, function (i) {
					var currentKPIInfo = data.kpiInfo[i];
					var totalGroupItems = currentKPIInfo.kpiGroupItems.length;
					var averageMark = 100 / totalGroupItems;
					if (currentKPIInfo.isSpreadKPI && totalGroupItems > 0) {
						var currentKPIGroup = currentKPIInfo.kpiGroupItems;
						$.each(currentKPIGroup, function (k) {
							currentKPIGroup[k].weight = averageMark;
						});
					}
				});
			}

			$scope.employeeEPA = data;
			$scope.finalScore = data.finalScore;
			var roleArray = [];
			$.each(data.projectRoles, function (i) {
				roleArray.push(data.projectRoles[i].name);
			});
			$scope.employeeRole = roleArray.join(", ");

			calculateScore($scope, data.kpiInfo);
		}

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
	['$scope', '$location', '$route', 'employeeEPAFactory', 'commonFactory', 'ErrorService', 'notificationFactory',
	function ($scope, $location, $route, employeeEPAFactory, commonFactory, ErrorService, notificationFactory) {

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

		$scope.reload = function () {
			$route.reload();
		};

	} ]);

// View EPA of Member by Manager Controller
	epaModule.controller('ViewMemberByManagerEPAController', [
	'$scope', '$routeParams', '$route', 'employeeEPAFactory', 'ErrorService', 'commonFactory', 'notificationFactory', '$modal',
	'employeeRepository', 'linkToEPAService',
	function ($scope, $routeParams, $route, employeeEPAFactory, ErrorService, commonFactory,
	notificationFactory, $modal, employeeRepository, linkToEPAService) {

		var targetId = $routeParams.targetId;
		targetId = targetId ? parseInt(targetId) : null;

		employeeEPAFactory.getCurrentUser().then(function (currentUser) {
			$scope.currentUser = currentUser;
		}, function () {
			notificationFactory.error("ErrorMessage");
		});

		var objectQuery = linkToEPAService.getProperty();

		// only SA or PM can see Target filter
		$scope.$watch("currentUser", function (newValue) {
			if (newValue) {
				var roles = newValue.roles;

				$.each(roles, function (index, value) {
					if (value.name === 'SA' || value.name === 'PM') {
						$scope.showTargetFilter = true;
						return false;
					}
				});
			}
		});

		if (targetId) {
			$scope.filter = {
				targetId: targetId
			};
		}
		// get all Target to filter
		employeeEPAFactory.getTargetToFilter().then(function (targetList) {
			$scope.targetOptions = targetList;
		});
		// get all project to filter
		employeeEPAFactory.getAllProject().then(function (projectList) {
			$scope.projectOptions = projectList;
		});

		// get all status to filter
		employeeEPAFactory.getAllStatus().then(function (statusList) {
			$scope.statusOptions = statusList;
		});

		$scope.search = {
			searchKey: ''
		};

		// get all employee by current user
		$scope.getEmployee = function (searchKey) {
			employeeRepository.getEmployeeByCurrentUserId(searchKey, null).success(function (employees) {
				$scope.searchEmployees = employees;
			}).error(function () {
				notificationFactory.error("ErrorMessage");
			});
		};

		var successGetEPA = function (listEPA) {
			$scope.listEPA = listEPA;
			$scope.submitted = false;
		};

		var successChangeEPAStatus = function (response) {
			getEPAOfEmployeeByManager(null, null);
			if (response.data !== "") {
				notificationFactory.error('', response.data);
			}
			else {
				notificationFactory.success('SuccessSaveData', '');
			}

		};

		var errorChangeEPAStatus = function (response) {
			notificationFactory.error('', response);
			getEPAOfEmployeeByManager(null, null);
		};

		var errorHandle = function (status) {
			if (status === 405) {
				ErrorService.errorHandle(status);
			}
			else {
				notificationFactory.error("ErrorMessage");
			}

			$scope.submitted = false;
		};

		// get EPA of Employee by manager
		var getEPAOfEmployeeByManager = function (targetId, projectId, status, employeeId) {
			employeeEPAFactory.getEPAOfEmployeeByManager(targetId, projectId, status, employeeId).then(successGetEPA, errorHandle);
		};

		if (objectQuery != null) {
			if (objectQuery.status != null) {
				getEPAOfEmployeeByManager(targetId, objectQuery.projectId, objectQuery.status, null);
			}
			else {
				getEPAOfEmployeeByManager(targetId, objectQuery.projectId, null, null);
			}

			if ($scope.filter) {
				$scope.filter.projectId = objectQuery.projectId;
				$scope.filter.status = objectQuery.status;
			}
			else {
				$scope.filter = {
					projectId: objectQuery.projectId,
					status: objectQuery.status
				};
			}
		}
		else {
			getEPAOfEmployeeByManager(targetId, null, null, null);
		}

		// filter EPA
		$scope.query = function (targetId, projectId, status, employeeId) {
			$scope.submitted = true;
			getEPAOfEmployeeByManager(targetId, projectId, status, employeeId);
		};

		$scope.$watch('search.searchKey', function (newValue) {
			if (newValue === "") {
				$scope.employee = null;
			}
		});

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
				epaItem.statusName = status.name;
				epaItem.status = status.id;
				epaItem.targetId = epaItem.target.targetId;
				employeeEPAFactory.updateEPAStatus(epaItem).then(successChangeEPAStatus, errorChangeEPAStatus);
			});
		};

		$scope.reload = function () {
			$route.reload();
		};
	} ]);