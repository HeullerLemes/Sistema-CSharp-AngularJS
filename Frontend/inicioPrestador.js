var host_location = 'http://localhost:62601';
angular.module('inicioPrestador', ['ngMaterial', 'cgBusy','angular-jwt', 'md.data.table', 'ngMessages'])
.config(function Config($httpProvider, jwtInterceptorProvider,jwtOptionsProvider) {
        // Please note we're annotating the function so that the $injector works when the file is minified
        jwtInterceptorProvider.tokenGetter = [function() {
            var tok = localStorage.getItem('id_token');
            return tok;
        }];

        $httpProvider.interceptors.push('jwtInterceptor');
        jwtOptionsProvider.config({
      whiteListedDomains: ['localhost']
    });
    })
    .config(function($mdThemingProvider) {
        $mdThemingProvider.theme('default')
            .primaryPalette('green')
            .accentPalette('light-green');
    })
.controller('inicioprestador', function($scope, $http, $mdMedia, $mdToast, $window, jwtHelper) {
$scope.prestadores = [];
$scope.servicos = [];
$scope.alocacoes = [];

//verificação de token
if(localStorage.getItem('id_token') == null){
    $window.location.href = "http://localhost:81/aulaWebApihtml/inicio.html"
}else{
    $scope.tok = jwtHelper.decodeToken(localStorage.getItem('id_token'));
    if($scope.tok.tipo != "prestador"){
        $window.location.href = "http://localhost:81/aulaWebApihtml/inicio.html"
    }
}

// logout
$scope.logout = function(){
    localStorage.removeItem('id_token');
    $window.location.href = "http://localhost:81/aulaWebApihtml/inicio.html"
}

// list de prestadores
$scope.checkStatus = function(servico, prestador){
    $scope.status = false;
    angular.forEach(prestador.Servicos, function(servicoPres, index) {
        if(servicoPres.Tipo === servico.Tipo){
            $scope.status = true;
        }
    });
    return $scope.status;
}

//get de serviços
$http.get(host_location + "/api/Servicoes").success(function(data,status,headers, config){
  $scope.servicos = data
}, function error(response) {
 alert(response.status);
         });

//get de prestadores
$http.get(host_location + "/api/Prestadors").success(function(data,status,headers, config){
    $scope.prestadores = data
}, function error(response) {
   alert(response.status);
           });

// get alocações
$scope.getAlocacoes = function (){
        var data = JSON.stringify({
            prestador: $scope.tok["sub"],
        })
        $http({
            method: 'POST',
            url: host_location + '/api/PrestadorAlocacao/GetAlocacoes',
            headers:
            {
             'Content-type': 'application/json'
            },
            data:data

        }).then(function success(responce, data, status, headers, config) {
            $scope.alocacoes = responce.data;
        }, function error(response) {
            alert(response.status);
        });
    }
});
