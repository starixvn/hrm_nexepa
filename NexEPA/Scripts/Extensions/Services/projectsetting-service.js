/*global projectsSettingModule, registrationModule*/
'use strict';

var epaGeneralService = function () {
	var property;
	return {
		getProperty: function () {
			return property;
		},
		setProperty: function (value) {
			property = value;
		}
	};
};

projectsSettingModule.service('projectMilestoneService', epaGeneralService);

projectsSettingModule.service('projectPermissionService', epaGeneralService);

projectsSettingModule.service('projectMemberService', epaGeneralService);

registrationModule.service('tabService', epaGeneralService);

registrationModule.service('linkToEPAService', epaGeneralService);
