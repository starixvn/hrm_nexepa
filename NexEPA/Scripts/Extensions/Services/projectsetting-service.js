/*global projectsSettingModule*/
'use strict';

projectsSettingModule.service('projectService', function () {
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