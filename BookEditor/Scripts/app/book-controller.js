angular.module('BookEditorApp', [])
    .controller('BookCtrl', function ($scope, $http) {
    	var error = "Ошибка получения информации";
    	//$scope.answered = false;
		$scope.publishYear = 1801;
    	$scope.title = "получение информации...";
    //	$scope.options = [];
    	//	$scope.correctAnswer = false;

    	$scope.working = false;

    	//$scope.answer = function () {
    	//	return $scope.correctAnswer ? 'correct' : 'incorrect';
    	//};
    	$scope.getBook= function () {
		    $http.get("/api/book/1")
			    .then(function successCallback(info, status, headers, config) {
				    //	$scope.options = data.options;
			    	$scope.title = info.data.title;
			    	$scope.publishYear = info.data.publishYear;
				    $scope.working = false;
			    }, function errorCallback(data, status, headers, config) {
				    $scope.title = error;
				    $scope.working = false;
			    });
	    }
    	//$scope.addBook = function (data) {
    	//	$scope.working = true;
    	 

    	//	$http.post('/api/book', {
    	//		'title': data.title,
    	//		'publishYear': data.publishYear,
    	//		'numPages' : data.numPages
    	//	}).success(function (data, status, headers, config) {
    	//	//	$scope.correctAnswer = (data === true);
    	//		$scope.working = false;
    	//	}).error(function (data, status, headers, config) {
    	//		$scope.title = error;
    	//		$scope.working = false;
    	//	});
    	//};
    	//$scope.getBooks = function() {
		//	$http.get("/api/book").success(function(data, status, headers, config) {
		//		//	$scope.options = data.options;
		//		var books = JSON.parse(data);
		//		//books.forEach(function (book))
		//		//{

		//		//}
		//		$scope.title = data.title;
		//		$scope.publishYear = data.publishYear;
		//		$scope.working = false;
		//	}).error(function(data, status, headers, config) {
		//		$scope.title = error;
		//		$scope.working = false;
		//	});
		//}
	});