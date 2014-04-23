/* global epaModule */
'use strict';

epaModule.controller('EmployeeEPAController', ['$scope', 'employeeEPAFactory', function ($scope, employeeEPAFactory) {
	$(".boxtitle").on("click", function () {
		var header = $(this);
		//getting the next element
		var content = header.next();
		//open up the content needed - toggle the slide- if visible, slide up, if not slidedown.
		content.slideToggle(500, function () {
		});
	});

	employeeEPAFactory.getMyEPA(39).then(function (data) {
		$scope.myEPA = data;
		var roleArray = [];
		$.each(data.projectRoles, function (i) {
			roleArray.push(data.projectRoles[i].name);
		});

		$scope.employeeRole = roleArray.join(", ");
	});

	$scope.rate = 0;
	$scope.max = 10;
	$scope.isReadonly = false;
	$scope.percent = 0;


} ]);

// View EPA of Current Member Controller
epaModule.controller('ViewCurrentMemberEPAController',
	['$scope', '$location', 'employeeEPAFactory', 'commonFactory', 'ErrorService', 'targetUtility',
	function ($scope, $location, employeeEPAFactory, commonFactory, ErrorService, targetUtility) {

		employeeEPAFactory.getAllProject().then(function (projectList) {
			$scope.projectOptions = projectList;
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
				targetUtility.showErrorMessage($scope);
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
			var targetId = epaItem.target.targetId,
				employeeId = epaItem.employeeId;
			$location.path('/MyEPA/Index').search('targetId', targetId).search('employeeId', employeeId);
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