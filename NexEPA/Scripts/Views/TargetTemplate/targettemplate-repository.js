/* global targetTemplateModule */
'use strict';

targetTemplateModule.factory("targetTemplateFactory", ['$http', '$q', function ($http, $q) {
	var url = '/api/targetTemplate/';
	return {
		getTargetTemplateById: function (targetTemplateId) {
			var deferred = $q.defer();
			$http.get('/api/targetTemplate/GetTargetTemplateById/', { params: { targetTemplateId: targetTemplateId} })
			.success(deferred.resolve).error(function (data, status) {
             deferred.reject(status);
            });
			return deferred.promise;
		},
		getAllTargetType: function () {
			var deferred = $q.defer();
			$http.get('/api/targetTemplate/GetAllTargetType')
			.success(deferred.resolve).error(function (data, status) {
				deferred.reject(status);
			});
			return deferred.promise;
		},
		getAllTargetTemplate: function (targetTypeId, isDisabled) {
			var deferred = $q.defer();
			$http.get('/api/targetTemplate/GetAllTargetTemplate', { params: { targetTypeId: targetTypeId, isDisabled: isDisabled} })
			.success(deferred.resolve).error(function (data, status) {
				deferred.reject(status);
			});
			return deferred.promise;
		},
		getKPI: function (targetTemplateId, targetTypeId, groupId, onlySelected) {
			var deferred = $q.defer();
			$http.get('/api/targetTemplate/GetKPI/', { params: {
				targetTemplateId: targetTemplateId,
				targetTypeId: targetTypeId,
				groupId: groupId,
				onlySelected: onlySelected
			}
			})
            .success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		getSelectedKPIByTargetTemplate: function (targetTemplateId) {
			var deferred = $q.defer();
			$http.get('/api/targetTemplate/GetSelectedKPIByTargetTemplate/', { params: { targetTemplateId: targetTemplateId} })
            .success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		getSelectedKPIGroupByTargetTemplate: function (targetTemplateId) {
			var deferred = $q.defer();
			$http.get('/api/targetTemplate/GetSelectedKPIGroupByTargetTemplate', { params: { targetTemplateId: targetTemplateId} })
			.success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		addTargetTemplate: function (template) {
			var deferred = $q.defer();
			var response = {};
			$http.post('/api/targetTemplate/Post', template)
			.success(function (data) {
				deferred.resolve(response = { data: data });
			}).error(function (data, status) {
				deferred.reject(response = { status: status });
			});
			return deferred.promise;
		},
		addKPIGroups: function (objectKPIGroup) {
			var deferred = $q.defer();
			var response = {};
			$http.post('/api/targetTemplate/AddKPIGroupWeight/', objectKPIGroup)
			.success(function (data) {
				deferred.resolve(response = { data: data });
			}).error(function (data, status) {
				deferred.reject(response = { status: status });
			});
			return deferred.promise;
		},
		updateTargetTemplate: function (template) {
			var deferred = $q.defer();
			var response = {};
			$http.put('/api/TargetTemplate/Put', template)
			.success(function (data) {
				deferred.resolve(response = { data: data });
			}).error(function (data, status) {
				deferred.reject(response = { status: status });
			});
			return deferred.promise;
		},
		deleteTargetTemplate: function (targetTemplateId) {
			var deferred = $q.defer();
			var response = {};
			$http['delete'](url, { params: { targetTemplateId: targetTemplateId} })
			.success(function (data) {
				deferred.resolve(response = { data: data });
			}).error(function (data, status) {
				deferred.reject(response = { status: status });
			});
			return deferred.promise;
		},
		updateTargetTemplateStatus: function (template) {
			var deferred = $q.defer();
			$http.put('/api/TargetTemplate/UpdateTargetTemplateStatus', template).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		removeKPIGroup: function (model) {
			var deferred = $q.defer();
			var response = {};
			$http.post('/api/targetTemplate/RemoveKPIGroup', model)
			.success(function (data) {
				deferred.resolve(response = { data: data });
			}).error(function (data, status) {
				deferred.reject(response = { status: status });
			});
			return deferred.promise;
		}
	};
} ]);

targetTemplateModule.factory('targetTemplateUtility', ['notificationFactory', function (notificationFactory) {
	var utility = {};

	utility.targetTemplateValidation = function (targetTemplate) {
		if (!targetTemplate) {
			return false;
		}
		else {
			if (!targetTemplate.name) {
				return false;
			}
			if (!targetTemplate.targetTypeId) {
				return false;
			}
		}

		return true;
	};

	utility.showMessageAfterValidate = function (scope) {
		scope.$watchCollection('[submitted, targetTemplateForm.$invalid, \
						targetTemplateForm.targetName.$error.required, \
						targetTemplateForm.targetType.$error.required ]', function (newVals) {
				if (newVals[0] && newVals[1]) {
					if (newVals[3]) {
						notificationFactory.error("TargetTypeRequire");
					}
					if (newVals[2]) {
						notificationFactory.error("NameRequire");
					}

					scope.submitted = false;
				}
			});
	};

	return utility;
} ]);