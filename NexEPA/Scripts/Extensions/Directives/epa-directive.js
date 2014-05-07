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

registrationModule.directive('treeModel', ['$compile', function ($compile) {
	return {
		restrict: 'A',
		link: function (scope, element, attrs) {
			//tree id
			var treeId = attrs.treeId;

			//tree model
			var treeModel = attrs.treeModel;

			//node id
			var nodeId = attrs.nodeId || 'id';

			//node label
			var nodeLabel = attrs.nodeLabel || 'label';

			//children
			var nodeChildren = attrs.nodeChildren || 'children';

			//tree template
			var template =
					'<ul>' +
						'<li data-ng-repeat="node in ' + treeModel + '">' +
							'<i class="collapsed" data-ng-show="node.'
							+ nodeChildren + '.length && node.collapsed" data-ng-click="'
							+ treeId + '.selectNodeHead(node)"></i>'
							+ '<i class="expanded" data-ng-show="node.'
							+ nodeChildren + '.length && !node.collapsed" data-ng-click="'
							+ treeId + '.selectNodeHead(node)"></i>' + '<i class="normal" data-ng-hide="node.'
							+ nodeChildren + '.length"></i> ' +
							'<span data-ng-class="node.selected" data-ng-click="'
							+ treeId + '.selectNodeLabel(node)">{{node.' + nodeLabel + '}}</span>' +
							'<div data-ng-hide="node.collapsed" data-tree-id="'
							+ treeId + '" data-tree-model="node.' + nodeChildren + '" data-node-id='
							+ nodeId + ' data-node-label=' + nodeLabel + ' data-node-children='
							+ nodeChildren + '></div>' +
						'</li>' +
					'</ul>';


			//check tree id, tree model
			if (treeId && treeModel) {

				//root node
				if (attrs.angularTreeview) {

					//create tree object if not exists
					scope[treeId] = scope[treeId] || {};
					//if node head clicks,
					scope[treeId].selectNodeHead = scope[treeId].selectNodeHead || function (selectedNode) {
						//Collapse or Expand

						selectedNode.collapsed = !selectedNode.collapsed;
					};

					//if node label clicks,
					scope[treeId].selectNodeLabel = scope[treeId].selectNodeLabel || function (selectedNode) {

						//remove highlight from previous node
						if (scope[treeId].currentNode && scope[treeId].currentNode.selected) {
							scope[treeId].currentNode.selected = undefined;
						}

						//set highlight to selected node
						selectedNode.selected = 'selected';

						//set currentNode
						scope[treeId].currentNode = selectedNode;
					};
				}

				//Rendering template.
				element.html('').append($compile(template)(scope));
			}
		}
	};
} ]);

registrationModule.directive('ngContextMenu', function () {
    var renderContextMenu = function ($scope, event, options) {
        $(event.target).addClass('context');
        var $contextMenu = $('<div>');
        $contextMenu.addClass('dropdown clearfix');
        var $ul = $('<ul>');
        $ul.addClass('dropdown-menu');
        $ul.attr({ 'role': 'menu' });
        $ul.css({
            display: 'block',
            position: 'absolute',
            left: event.pageX + 'px',
            top: event.pageY + 'px'
        });
        angular.forEach(options, function (item) {
            var $li = $('<li>');
            if (item === null) {
                $li.addClass('divider');
            } else {
                var $a = $('<a>');
                $a.attr({ tabindex: '-1', href: '#' });
                $a.text(item[0]);
                $li.append($a);
                $li.on('click', function () {
                    $scope.$apply(function() {
                        item[1].call($scope, $scope);
                    });
                });
            }
            $ul.append($li);
        });
        $contextMenu.append($ul);
        $contextMenu.css({
            width: '100%',
            height: '100%',
            position: 'absolute',
            top: 0,
            left: 0,
            zIndex: 9999
        });
        $(document).find('body').append($contextMenu);
        $contextMenu.on("click", function () {
            $(event.target).removeClass('context');
            $contextMenu.remove();
        }).on('click', function (event) {
            $(event.target).removeClass('context');
            event.preventDefault();
            $contextMenu.remove();
        });
    };
    return function ($scope, element, attrs) {
        element.on('click', function (event) {
            $scope.$apply(function () {
                event.preventDefault();
                var options = $scope.$eval(attrs.ngContextMenu);
                if (options instanceof Array) {
                    renderContextMenu($scope, event, options);
                } else {
                    throw '"' + attrs.ngContextMenu + '" not an array';                    
                }
            });
        });
    };
});