'use strict';
/*global kpiSettingModule*/

kpiSettingModule.factory('KPIRepository', ['$http', function ($http) {
	var url = '/api/KPI/';

	return {
		viewKPI: function (targetTypeId, groupId) {
			return $http.get('/api/KPI/ViewKPI',
				{ params: { targetTypeId: targetTypeId, 
							groupId: groupId} });
		},
		getKPIById: function (kpiId) {
			return $http.get('/api/KPI/GetKPIById', { params: { kpiId: kpiId} });
		},
		createKPI: function (kpiItem) {
			return $http.post(url, kpiItem);
		},
		updateKPI: function (kpiItem) {
			return $http.put(url, kpiItem);
		},
		deleteKPI: function (kpiId) {
			return $http['delete'](url, { params: { kpiId: kpiId} });
		}
	};
}]);