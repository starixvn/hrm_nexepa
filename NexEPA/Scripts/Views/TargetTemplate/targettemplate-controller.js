/* global targetTemplateModule */
'use strict';

targetTemplateModule.controller('TargetTemplateController', function ($scope, targetTemplateFactory, notificationFactory) {
    /**
    * Variables. 
    */

    /**
    * Ajax event Loaded. 
    */
    var successGetAllTargetTemplate = function (data) { $scope.tgtemplates = data; };
    var successDeleteTargetTemplate = function () {
        notificationFactory.success();
        return targetTemplateFactory.getAllTargetTemplate().then(successGetAllTargetTemplate);
    };
    var errorDeleteTargetTemplate = function (data) { notificationFactory.error(data); };
    /**
    * Initialize data to view Index.
    */
    targetTemplateFactory.getAllTargetTemplate().then(successGetAllTargetTemplate);

    /**
    * Event: Filter 
    */
    // Delete
    $scope.deleteTemplate = function (template) {
        targetTemplateFactory.deleteTargetTemplate(template.targetId).then(successDeleteTargetTemplate, errorDeleteTargetTemplate);
    };
});

targetTemplateModule.controller('CreateTargetTemplateController', function ($scope, $location, $window, targetTemplateFactory) {
    /**
    * Variables. 
    */
    $scope.errors = [];
    /**
    * Ajax event Loaded. 
    */
    var successGetAllTargetType = function (data) { $scope.tgTypes = data; };
    var successAddTargetTemplate = function () { $location.url('/TargetTemplate/Index'); };
    var errorAddTargetTemplate = function (response) { $scope.errors = response; };
    /**
    * Initialize data to view Index.
    */
    targetTemplateFactory.getAllTargetType().then(successGetAllTargetType);

    /**
    * Event: Filter 
    */
    // Submit
    $scope.addTargetType = function (template) {
        targetTemplateFactory.addTargetTemplate(template).then(successAddTargetTemplate, errorAddTargetTemplate);
    };

    // Cancel
    $scope.cancel = function () { $window.history.back(); };
});

targetTemplateModule.controller('EditTargetTemplateController', function ($scope, $location, $window, $routeParams, targetTemplateFactory) {
    /**
    * Variables. 
    */
    $scope.errors = [];
    var targetId = $routeParams.targetId;
    /**
    * Ajax event Loaded. 
    */
    var successGetAllTargetType = function (data) { $scope.tgTypes = data; };
    var successGetTargetTemplateById = function (data) { $scope.editTemplate = data; };
    var successUpdateTargetTemplate = function () { $location.url('/TargetTemplate/Index'); };
    var errorUpdateTargetTemplate = function (response) { $scope.errors = response; };
    /**
    * Initialize data to view Index.
    */
    targetTemplateFactory.getAllTargetType().then(successGetAllTargetType);
    targetTemplateFactory.getTargetTemplateById(targetId).then(successGetTargetTemplateById);

    /**
    * Event: Filter 
    */
    // Submit
    $scope.updateTargetTemplate = function (template) {
        targetTemplateFactory.updateTargetTemplate(template).then(successUpdateTargetTemplate, errorUpdateTargetTemplate);
    };

    // Cancel
    $scope.cancel = function () { $window.history.back(); };
});

