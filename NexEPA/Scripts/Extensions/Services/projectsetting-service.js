/*global projectsSettingModule, registrationModule, flowManagementModule*/
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

projectsSettingModule.service('sharedProperties', function () {
    var memberProject = [];

    return {
        getMembersProject: function () {
            return memberProject;
        },
        setMembersProject: function (value) {
            memberProject = value;
        }
    };
});

registrationModule.service('tabService', epaGeneralService);

registrationModule.service('linkToEPAService', epaGeneralService);

flowManagementModule.service('flowManagementService', epaGeneralService);
