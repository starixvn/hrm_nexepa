'use strict';
/*global homeModule*/

homeModule.controller('ViewHomePageController', ['$scope', 'homeFactory', function ($scope, homeFactory) {
	homeFactory.getGeneralInformationOfCurrentEmployee();
} ]);