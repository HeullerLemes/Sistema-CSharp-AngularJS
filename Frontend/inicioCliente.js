var host_location = 'http://localhost:62601';
angular.module('inicioCliente', ['ngMaterial', 'cgBusy','angular-jwt', 'md.data.table', 'ngMessages'])
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
.controller('iniciocliente', function($scope, $http, $mdMedia, $mdToast, $window, jwtHelper) {
    $scope.servico = [];
    $scope.$watch('prestadores', function () {
        $scope.prestadores;
    });
    $scope.prestadores = [];
    $scope.dataInicio = new Date();
    $scope.dataFim = new Date();

    //verificação de token
    if(localStorage.getItem('id_token') == null){

            $window.location.href = "http://localhost/aulaWebApihtml/inicio.html"
           
    }else{
        $scope.tok = jwtHelper.decodeToken(localStorage.getItem('id_token'));
        if($scope.tok.tipo != "cliente"){
            $window.location.href = "http://localhost/aulaWebApihtml/inicio.html"
        }
    }
    // get prestadores
    $scope.cadastrar  = function (){
            var data = JSON.stringify({
            Id: $scope.servico,
            });

            $http({
                method: 'POST',
                url: host_location + '/api/PrestadorServicos/GetPrestadores',
                headers: 
                {
                    'Content-type': 'application/json'
                },
                data:data
                
            }).then(function success(responce, data, status, headers, config) {
                $scope.prestadores = responce.data;
            }, function error(response) {
                alert(response.status);
            });
    };
    
    //get de serviços
    $http.get(host_location + "/api/Servicoes").success(function(data,status,headers, config){
      $scope.servicos = data
    }, function error(response) {
     alert(response.status);
             });

    //mensagem de sucesso
    $scope.showAlert = function () {
        $mdDialog.show(
            $mdDialog.alert()
                .parent(angular.element(document.querySelector('#popupContainer')))
                .clickOutsideToClose(true)
                .title('Cliente cadastrado!')
                .textContent('Cliente cadastrado com Sucesso!')
                .ariaLabel('Cliente Cadastrado com Sucesso')
                .ok('Ok')
        );
    };

    //filtro calandario
    $scope.onlyWeekendsPredicate = function(date) {
    var day = date.getDay();
    return day === 1 || day === 2 || day === 3 || day === 4 || day === 5;
  };
});