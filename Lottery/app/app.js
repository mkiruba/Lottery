var lotteryGeneratorModule = angular.module('lotteryGeneratorModule', []);

lotteryGeneratorModule.controller('lotteryGeneratorController', ['$scope', 'lotteryGeneratorRepository',
    function ($scope, lotteryGeneratorRepository) {

        $scope.getLotteryResults = function () {

            var promise = lotteryGeneratorRepository.getLotteryResults();
            promise.then(function (results) {
                $scope.lotteryResults = results;

            }, function (reason) {
                alert('Failed: ' + reason);
            }, function (update) {
                alert('Got notification: ' + update);
            });
        };
    }]);


lotteryGeneratorModule.factory('lotteryGeneratorRepository', function ($http, $q) {
    return {
        getLotteryResults: function (results) {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: '/api/lottery', data: results
            })
                .success(function (data) {
                    deferred.resolve(data);
                })
                .error(function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;
        }
    };
});