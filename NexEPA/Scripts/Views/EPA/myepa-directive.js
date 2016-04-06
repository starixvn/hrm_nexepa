/* global epaModule */
'use strict';

epaModule.directive('scrollIf', function () {
    return {
        link: function ($scope, iElement) {
            $scope.$watch(function () {
                var head = $(iElement).find('thead'),
                    body = $(iElement).find('tbody');
                var child = body.find('tr'),
                    len = child.length;

                if (len) {
                    var maxHeight = 0;

                    child.each(function (i, e) {
                        maxHeight += $(e).height();
                    });

                    if (maxHeight > body.height()) {
                        head.addClass('header-fixed');
                    }
                    else {
                        head.removeClass('header-fixed');
                    }
                }
            });
        }
    };
});