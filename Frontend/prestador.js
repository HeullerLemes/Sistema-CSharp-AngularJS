var host_location = 'http://localhost:62601';
angular.module('Prestador', ['ngMaterial', 'cgBusy', 'md.data.table'])
    .config(function($mdThemingProvider) {
        $mdThemingProvider.theme('default')
            .primaryPalette('green')
            .accentPalette('light-green');
    })
.controller('prestador', function($scope, $http, $mdMedia, $mdToast, $window) {
  $scope.servicos = [];
  $scope.selecionados = [];

  //get de serviços
  $http.get(host_location + "/api/Servicoes").success(function(data,status,headers, config){
      $scope.servicos = data
  }, function error(response) {
     alert(response.status);
             });

  //funcoes checkbox
  $scope.toggle = function (servico, list) {
  var idx = list.indexOf(servico);
    if (idx > -1) {
        list.splice(idx, 1);
    }
    else {
        list.push(servico);
    }
    };
  $scope.exists = function (servico, list) {
    return list.indexOf(servico) > -1;
    };

    //model prestador
    $scope.prestador = {
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
                .title('Prestador cadastrado!')
                .textContent('Prestador cadastrado com Sucesso!')
                .ariaLabel('Prestador Cadastrado com Sucesso')
                .ok('Ok')
        );
    };

    // metodo post cadastrar
    $scope.cadastrar = function (){
        var data = JSON.stringify({
            nomeCompleto: $scope.prestador.nomeCompleto,
            username: $scope.prestador.username,
            senha: $scope.prestador.senha,
            selecionados: $scope.selecionados          
        });
        $http({
            method: 'POST',
            url: host_location + "/api/Prestadors",
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