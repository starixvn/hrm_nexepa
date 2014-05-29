'use strict';
/*global registrationModule*/

registrationModule.factory('commonFactory', function () {
	var common = {};
	var limitNumber = 100;

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

	common.tablePagination = function ($scope, $filter, dataPagination) {
		/**
		* Scope Variable. 
		*/
		$scope.gap = 7;
		$scope.filteredItems = [];
		$scope.groupedItems = [];
		$scope.itemsPerPage = 18;
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
						if (searchMatch(item[attr].toString(), $scope.query)) {
							return true;
						}
					}
				}
				return false;
			});
			// take care of the sorting order
			if ($scope.sort.sortingOrder !== '') {
				$scope.filteredItems = $filter('orderBy')($scope.filteredItems, $scope.sort.sortingOrder, $scope.sort.reverse);
			}
			$scope.currentPage = 0;
			// now group by pages
			$scope.groupToPages();
		};

		// calculate page in place
		$scope.groupToPages = function () {
			$scope.pagedItems = [];

			for (var i = 0; i < $scope.filteredItems.length; i++) {

				if (i % $scope.itemsPerPage === 0) {
					$scope.pagedItems[Math.floor(i / $scope.itemsPerPage)] = [$scope.filteredItems[i]];
				} else {
					$scope.pagedItems[Math.floor(i / $scope.itemsPerPage)].push($scope.filteredItems[i]);
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
				start = 0;
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

	return common;
});

