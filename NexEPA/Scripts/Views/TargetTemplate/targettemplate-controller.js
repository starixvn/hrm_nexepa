/* global targetTemplateModule */
'use strict';

targetTemplateModule.controller('TargetTemplateController', function ($scope, $modal, targetTemplateFactory, notificationFactory) {
    /**
    * Variables. 
    */

    /**
    * Ajax event Loaded. 
    */
    var successGetAllTargetTemplate = function (data) { $scope.tgtemplates = data; };
    var succesGetListKPI = function (data) { $scope.lstKPI = data; };
    var successAddKPI = function () {
        targetTemplateFactory.getAllTargetTemplate().then(successGetAllTargetTemplate);
    };
    var successDeleteTargetTemplate = function () {
        notificationFactory.success();
        return targetTemplateFactory.getAllTargetTemplate().then(successGetAllTargetTemplate);
    };
    var errorNotify = function (data) { notificationFactory.error(data); };
    /**
    * Initialize data to view Index.
    */
    targetTemplateFactory.getAllTargetTemplate().then(successGetAllTargetTemplate);
    targetTemplateFactory.getKPI(null, null).then(succesGetListKPI);

    /**
    * Event: Filter 
    */
    // Delete
    $scope.deleteTemplate = function (template) {
        targetTemplateFactory.deleteTargetTemplate(template.targetId).then(successDeleteTargetTemplate, errorNotify);
    };

    // Add KPI Popup
    $scope.addKPI = function (targetId) {
        var modalInstance = $modal.open({
            templateUrl: '../templates/TargetTemplate/AddKPI.html',
            controller: "ModalInstanceController",
            resolve: {
                lstKPI: function () {
                    return $scope.lstKPI;
                }
            }
        });

        modalInstance.result.then(
        function (itemSelected) {
            var objectKPI = {};
            objectKPI.TargetId = targetId;
            objectKPI.KPIIds = itemSelected.join(',');
            targetTemplateFactory.addKPI(objectKPI).then(successAddKPI, errorNotify);
        },
         function () {

         });
        targetTemplateFactory.getKPI(null, null).then(succesGetListKPI);
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
    var successGetTargetTemplateById = function (data) {
        $scope.editTemplate = data;
        $scope.currentTargetTemplateName = data.name;
    };
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

targetTemplateModule.controller('ModalInstanceController', function ($scope, $modalInstance, lstKPI) {
    $scope.selection = [];
    $scope.lstKPI = lstKPI;
    $scope.lstAllData = [];

    $scope.ok = function () {
        $modalInstance.close($scope.selection);
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };

    $.each($scope.lstKPI, function (key) {
        var data = $scope.lstKPI[key].kpiGroupItems;
        $.each(data, function (k) {
            $scope.lstAllData.push(data[k]);
        });

    });

    $scope.$watch('lstAllData|filter:{selected:true}', function (selectedItems) {
        $scope.selection = selectedItems.map(function (item) {
            return item.kpiId;
        });
    }, true);
});
