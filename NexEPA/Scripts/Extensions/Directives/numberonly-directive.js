/*global registrationModule*/
'use strict';

registrationModule.directive('percentage', function () {
	return {
		restrict: 'A',
		require: 'ngModel',
		link: function (scope, element, attrs, modelCtrl) {
			modelCtrl.$parsers.push(function (inputValue) {
				if (inputValue === undefined) {
					return '';
				}

				var transformedInput = inputValue.replace(/[^0-9]/g, '');
				if (transformedInput < 0 || transformedInput > 100) {
					modelCtrl.$setViewValue('');
					modelCtrl.$render();
				}
				else {
					if (transformedInput !== inputValue) {
						modelCtrl.$setViewValue(transformedInput);
						modelCtrl.$render();
					}
				}

				return transformedInput;
			});
		}
	};
});