var host_location = 'http://localhost:62601';
angular.module('BlankApp', ['ngMaterial', 'cgBusy','angular-jwt', 'md.data.table'])

.config(function Config($httpProvider, jwtInterceptorProvider) {
        // Please note we're annotating the function so that the $injector works when the file is minified
        jwtInterceptorProvider.tokenGetter = [function() {
            var tok = localStorage.getItem('id_token');
            return tok;
        }];

        $httpProvider.interceptors.push('jwtInterceptor');
    })
    .config(function($mdThemingProvider) {
        $mdThemingProvider.theme('default')
            .primaryPalette('green')
            .accentPalette('light-green');
    })
    .controller('AppCtrl', function($scope, $q, $http, $mdDialog, $mdMedia, $mdToast) {
        $scope.clientes = new Array();

        $scope.loadClients = function() {

            $scope.thePromise = $http({
                method: 'GET',
                url: host_location + '/api/Clientes',
                headers: {
                    'Accept': 'applicaton/json'
                }
            }).then(function success(response, status, headers, config) {
                $scope.clientes = response.data;
            }, function error(response) {
                alert(response.status);
            });
        }

        $scope.init = function() {
            if (!$scope.isLogged()) {
                $scope.login();
            } else {
                $scope.loadClients();
            }

        }

        $scope.isLogged = function() {
            return (localStorage.getItem('id_token') != undefined)
        };

        $scope.login = function() {

            var useFullScreen = ($mdMedia('sm') || $mdMedia('xs')) && $scope.customFullscreen;

            $mdDialog.show({
                    controller: LoginController,
                    templateUrl: 'dialogLogin.tmpl.html',
                    parent: angular.element(document.body),
                    fullscreen: useFullScreen
                })
                .then(function(data) {

                    $scope.thePromise = $http({
                        method: 'POST',
                        url: host_location + '/api/Login/Login',
                        headers: {
                            'Content-type': 'application/json',
                        },
                        data: data
                    }).then(function success(response, status, headers, config) {
                        var tok = response.headers('Authorization')
                        localStorage.setItem('id_token', tok);
                        $scope.init();
                    }, function error(response) {
                        alert(response.status);
                    });
                }, function() {
                    $scope.login();
                });
                
        }

        $scope.init();
    });


    function LoginController($scope, $mdDialog) {

        $scope.login = function() {
            $mdDialog.hide({
                username: $scope.username,
                password: $scope.password
            });
        };
    }
