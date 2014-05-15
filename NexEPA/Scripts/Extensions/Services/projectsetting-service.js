/*global projectsSettingModule, registrationModule*/
'use strict';

registrationModule.service('projectService', function () {
	var property;
	return {
		getProperty: function () {
			return property;
		},
		setProperty: function (value) {
			property = value;
		}
	};
});

projectsSettingModule.service('projectMilestoneService', function () {
	var property;
	return {
		getProperty: function () {
			return property;
		},
		setProperty: function (value) {
			property = value;
		}
	};
});

registrationModule.service('tabService', function () {
	var property;
	return {
		getProperty: function () {
			return property;
		},
		setProperty: function (value) {
			property = value;
		}
	};
});