/* global epaModule */
'use strict';

epaModule.controller('MyEPAController', function ($scope) {
	$(".boxtitle").on("click", function () {
		var header = $(this);
		//getting the next element
		var content = header.next();
		//open up the content needed - toggle the slide- if visible, slide up, if not slidedown.
		content.slideToggle(500, function () {
		});
	});
});