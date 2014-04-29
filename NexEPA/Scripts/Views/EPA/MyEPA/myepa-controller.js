/* global epaModule */
'use strict';

epaModule.controller('EmployeeEPAController', ['$scope', 'employeeEPAFactory', 'notificationFactory', '$routeParams', '$window',
function ($scope, employeeEPAFactory, notificationFactory, $routeParams, $window) {
	/**
	* Scope variables. 
	*/
	$scope.max = 10;
	$scope.isReadonly = false;
	var targetId = $routeParams.targetId;
	$scope.finalScore = 0;

	/**
	* Ajax event. 
	*/
	var successAddOrUpdateEPA = function () { notificationFactory.success('SuccessSaveData', ''); };
	var errorAddOrUpdateEPA = function (response) { notificationFactory.error('', response); };
	var successGetEmployeeEPA = function (data) {
		$scope.employeeEPA = data;
		var roleArray = [];
		$.each(data.projectRoles, function (i) {
			roleArray.push(data.projectRoles[i].name);
		});
		$scope.employeeRole = roleArray.join(", ");
	};

	var errorGetEmployeeEPA = function (response) { notificationFactory.error('', response); };

	/**
	* Initialize data to view Edit Project. 
	*/
	employeeEPAFactory.getEmployeeEPA(targetId).then(successGetEmployeeEPA, errorGetEmployeeEPA);

	/**
	* Events 
	*/
	$scope.save = function (employeeEPAResult) {
		employeeEPAResult.totalScore = $scope.totalScore;
		employeeEPAFactory.addEmployeeEPA(employeeEPAResult).then(successAddOrUpdateEPA, errorAddOrUpdateEPA);
	};

	$scope.cancel = function () { $window.history.back(); };

	$scope.hoveringOver = function (value, object) {
		object.percent = (100 * value) / $scope.max;
	};

	$scope.hoveringLeave = function (rate, object) {
		object.percent = (100 * rate) / $scope.max;

		var kpiInfo = $scope.employeeEPA.kpiInfo;
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
			}
			if (totalGroupScore > 0) {
				totalGroupWeight += kpiGroupWeight;
			}
		});

		$scope.finalScore = (totalGroupScore / totalGroupWeight) * 10;

	};

	$(".boxtitle").on("click", function () {
		var header = $(this);
		//getting the next element
		var content = header.next();
		//open up the content needed - toggle the slide- if visible, slide up, if not slidedown.
		content.slideToggle(500, function () {
		});
	});


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
				$location.path('/MyEPA/Index').search('targetId', targetId).search('employeeId', employeeId);
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
	'$scope', '$location', 'employeeEPAFactory', 'ErrorService', 'commonFactory', 'notificationFactory',
	function ($scope, $location, employeeEPAFactory, ErrorService, commonFactory, notificationFactory) {

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
				$location.path('/MyEPA/Index').search('targetId', targetId).search('employeeId', employeeId);
			}
		};
} ]);