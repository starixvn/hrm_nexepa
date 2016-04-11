'use strict';
/*global registrationModule*/

registrationModule.factory('commonFactory', ['$filter', 'notificationFactory', function ($filter, notificationFactory) {
    var common = {};
    var limitNumber = 100;

    common.numberEntryList = function ($scope) {
        $scope.entries = [10, 15, 20, 50, 100, 200];
    };

    common.getDateFormat = function ($scope) {
        $scope.dateFormat = "dd MMM, yyyy";
    };

    common.maxLengthDescription = function ($scope) {
        $scope.maxLengthDescription = limitNumber;
    };

    common.readMore = function (item) {
        item.numLimit = 10000;
        item.showReadMore = true;
    };

    common.readLess = function (item) {
        item.numLimit = limitNumber;
        item.showReadMore = false;
    };

    common.openPopupStartDate = function ($event, $scope) {
        $event.preventDefault();
        $event.stopPropagation();

        $scope.calendar.startDateOpened = true;
        $scope.calendar.endDateOpened = false;
    };

    common.openPopupEndDate = function ($event, $scope) {
        $event.preventDefault();
        $event.stopPropagation();

        $scope.calendar.endDateOpened = true;
        $scope.calendar.startDateOpened = false;
    };

    // For many datepicker in a single page
    // Milestone Add and Edit inline
    // Add Mode
    common.openPopupAddModeStartDate = function ($event, $scope) {
        $event.preventDefault();
        $event.stopPropagation();

        $scope.calendar.addModeStartDateOpened = true;
        $scope.calendar.addModeEndDateOpened = false;
    };

    common.openPopupAddModeEndDate = function ($event, $scope) {
        $event.preventDefault();
        $event.stopPropagation();

        $scope.calendar.addModeEndDateOpened = true;
        $scope.calendar.addModeStartDateOpened = false;
    };

    // Edit Mode
    common.openPopupEditModeStartDate = function ($event, $scope, milestoneId) {
        $event.preventDefault();
        $event.stopPropagation();
        var propertyStart = 'editModeStartDateOpened'.concat(milestoneId);
        var propertyEnd = 'editModeEndDateOpened'.concat(milestoneId);

        $scope.calendar[propertyStart] = true;
        $scope.calendar[propertyEnd] = false;
    };

    common.openPopupEditModeEndDate = function ($event, $scope, milestoneId) {
        $event.preventDefault();
        $event.stopPropagation();

        var propertyStart = 'editModeStartDateOpened'.concat(milestoneId);
        var propertyEnd = 'editModeEndDateOpened'.concat(milestoneId);

        $scope.calendar[propertyEnd] = true;
        $scope.calendar[propertyStart] = false;
    };

    common.tablePagination = function ($scope, dataPagination, itemPerPage) {
        /**
        * Scope Variable. 
        */
        $scope.gap = 7;
        $scope.filteredItems = [];
        $scope.groupedItems = [];
        if (itemPerPage != null) {
            $scope.itemsPerPage = itemPerPage;
        }
        else {
            $scope.itemsPerPage = 20;
        }

        $scope.pagedItems = [];
        $scope.currentPage = 0;

        /**
        * Init. 
        */
        $scope.sort = {
            sortingOrder: 'id',
            reverse: false
        };

        /**
        * Search local data when data in table loaded. 
        */
        var searchMatch = function (haystack, needle) {
            if (!needle) {
                return true;
            }
            return haystack.toLowerCase().indexOf(needle.toLowerCase()) !== -1;
        };

        // init the filtered items
        $scope.search = function () {
            $scope.filteredItems = $filter('filter')(dataPagination, function (item) {

                for (var attr in item) {
                    if (item[attr] != null) {
                        if (searchMatch(item[attr].toString(), $scope.queryData)) {
                            return true;
                        }
                    }
                }
                return false;
            });
            // take care of the sorting order
            //			if ($scope.sort.sortingOrder !== '') {
            //				$scope.filteredItems = $filter('orderBy')($scope.filteredItems, $scope.sort.sortingOrder, $scope.sort.reverse);
            //			}
            $scope.currentPage = 0;
            // now group by pages
            $scope.groupToPages();
        };

        // calculate page in place
        $scope.groupToPages = function () {
            $scope.pagedItems = [];
            for (var i = 0; i < $scope.filteredItems.length; i++) {
                $scope.filteredItems[i].itemIndex = i;
                if (i % $scope.itemsPerPage === 0) {
                    $scope.pagedItems[Math.floor(i / $scope.itemsPerPage)] = [$scope.filteredItems[i]];
                } else {
                    $scope.pagedItems[Math.floor(i / $scope.itemsPerPage)].push($scope.filteredItems[i]);
                    //$scope.gap = $scope.pagedItems.length;
                }
            }
        };

        /**
        * Event. 
        */
        $scope.range = function (size, start, end) {
            var ret = [];
            if (size < end) {
                end = size;
                start = size - $scope.gap;
                if (start < 0) {
                    start = 0;
                }
            }
            for (var i = start; i < end; i++) {
                ret.push(i);
            }
            return ret;
        };

        $scope.firstPage = function () {
            if ($scope.currentPage > 0) {
                $scope.currentPage = 0;
            }
        };

        $scope.lastPage = function () {
            if ($scope.currentPage < $scope.pagedItems.length - 1) {
                $scope.currentPage = $scope.pagedItems.length - 1;
            }

        };

        $scope.prevPage = function () {
            if ($scope.currentPage > 0) {
                $scope.currentPage--;
            }
        };

        $scope.nextPage = function () {
            if ($scope.currentPage < $scope.pagedItems.length - 1) {
                $scope.currentPage++;
            }
        };

        $scope.setPage = function () {
            $scope.currentPage = this.n;
        };

        // functions have been describe process the data for display
        $scope.search();
    };

    var createLabel = function (departmentItem, level) {
        var label = '',
            space = '',
            levelTemp = level;

        if (level > 0) {
            while (levelTemp > 0) {
                label = label + " " + space;
                space = String.fromCharCode(160) + String.fromCharCode(160) + String.fromCharCode(160);
                levelTemp--;
            }

            label = label + "└── " + departmentItem.name;
        }
        else {
            label = departmentItem.name;
        }

        return label;
    };

    common.generateOptions = function (departments, options, level) {
        angular.forEach(departments, function (departmentItem) {
            options.push({ label: createLabel(departmentItem, level), value: departmentItem.departmentId });
            common.generateOptions(departmentItem.children, options, level + 1);
        });

        return options;
    };

    common.getSelectedData = function (data, propName) {
        var selectedProperties = [],
            allProperties = [];

        angular.forEach(data, function (item) {
            var selectedValue = propName ? item[propName] : item;

            if (item.ticked) {
                selectedProperties.push(selectedValue);
            }

            allProperties.push(selectedValue);
        });

        var result = null;

        if (selectedProperties.length > 0) {
            result = propName ? selectedProperties.join(',') : selectedProperties;
        } else if (allProperties.length > 0) {
            result = propName ? allProperties.join(',') : allProperties;
        }

        return result;
    };

    common.getOnlySelectedData = function (data, propName) {
        var selectedProperties = [],
            result = '';

        angular.forEach(data, function (item) {
            var selectedValue = propName ? item[propName] : item;

            if (item.ticked) {
                selectedProperties.push(selectedValue);
            }
        });

        if (selectedProperties.length > 0) {
            result = propName ? selectedProperties.join(',') : selectedProperties;
        }

        return result;
    };

    common.scrollToBottom = function (additionalHeight) {
        $('html, body').animate({
            scrollTop: $(document).height() - $(window).height() + (additionalHeight ? additionalHeight : 0)
        }, 1400, "easeOutQuint");
    };

    common.validateFromToDate = function (fromDate, toDate) {
        var isValid = true;

        if (!fromDate) {
            isValid = false;
            notificationFactory.error("FromRequire");
        } else if (!toDate) {
            isValid = false;
            notificationFactory.error("ToRequire");
        } else {
            fromDate = new Date(fromDate.toDateString());
            toDate = new Date(toDate.toDateString());
            var maxDate = new Date(fromDate.getFullYear() + 1, fromDate.getMonth(), fromDate.getDate());

            if (fromDate > toDate) {
                isValid = false;
                notificationFactory.error("ValidateFromToDate");
            }
            else if (toDate > maxDate) {
                isValid = false;
                notificationFactory.error("ValidateMaximumOneYear");
            }
        }

        return isValid;
    };

    common.calculateFromDate = function (period, day) {
        var date = new Date(),
            year = date.getFullYear(),
            month = date.getMonth(),
            expectedDay = day ? day : date.getDate();

        var fromDate = new Date(year, month - period, expectedDay);

        return fromDate;
    };

    common.calculateLastDateOfCurrentMonth = function () {
        var date = new Date(),
            year = date.getFullYear(),
            month = date.getMonth();

        var lastDate = new Date(year, month + 1, 0);

        return lastDate;
    };

    common.getCurrentDateWithoutTime = function () {
        var date = new Date(),
            year = date.getFullYear(),
            month = date.getMonth(),
            day = date.getDate();

        var currentDate = new Date(year, month, day);

        return currentDate;
    };

    // Function to get the Max value in Array
    common.max = function (array) {
        return Math.max.apply(Math, array);
    };

    // Function to get the Min value in Array
    common.min = function (array) {
        return Math.min.apply(Math, array);
    };

    return common;
}]);

