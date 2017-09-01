var host_location = 'http://localhost:62601';
angular.module('Cliente', ['ngMaterial', 'cgBusy', 'md.data.table'])
    .config(function($mdThemingProvider) {
        $mdThemingProvider.theme('default')
            .primaryPalette('green')
            .accentPalette('light-green');
    })
.controller('cliente', function($scope, $http, $mdMedia, $mdToast, $window) {
    //model cliente
    $scope.cliente = {
      nomeCompleto: '',
      username: '', 
      senha: '',
    };

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

    // metodo post cadastrar
    $scope.cadastrar = function (){
        var data = JSON.stringify({
            nomeCompleto: $scope.cliente.nomeCompleto,
            username: $scope.cliente.username,
            senha: $scope.cliente.senha,        
        });
        $http({
            method: 'POST',
            url: host_location + "/api/Clientes",
            headers:
                    {
                    'Content-type': 'application/json'
                    },
            data: data
            }).then(function (response) {
                console.log(JSON.stringify(response.status));
                $scope.showAlert();
            });
    } 
});