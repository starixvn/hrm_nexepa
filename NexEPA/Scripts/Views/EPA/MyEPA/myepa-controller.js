/* global epaModule */
'use strict';

epaModule.controller('MyEPAController', function ($scope, myEPAFactory) {
	$(".boxtitle").on("click", function () {
		var header = $(this);
		//getting the next element
		var content = header.next();
		//open up the content needed - toggle the slide- if visible, slide up, if not slidedown.
		content.slideToggle(500, function () {
		});
	});

	myEPAFactory.getMyEPA(6).then(function () {
	});

	$scope.rate = 0;
	$scope.max = 5;
	$scope.isReadonly = false;
	$scope.percent = 0;


});