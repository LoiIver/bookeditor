function Book(data, pubHouses) {
	if (!data)
		return;
	this.title = ko.observable(data.title);
	this.authors = ko.observableArray([]);
	this.numPages = ko.observable(data.numPages);
	if (!!data.pubHouse) {
		this.pubHouseId = ko.observable( data.pubHouse.pubHouseId);
		this.pubHouseName = ko.computed(function() {
			
			return self.test;
		});
	}

	this.publishYear = ko.observable(data.publishYear);
	this.isbn = ko.observable(data.isbn);
	this.imageUrl = ko.observable(data.imageUrl);
	this.bookId = ko.observable(data.bookId);
}

function PubHouse(data) {
	this.pubHouseId = ko.observable(data.pubHouseId);
	this.name = ko.observable(data.name);
}

function Author(data) {
	this.authorId = ko.observable(data.authorId);
	this.firstName = ko.observable(data.firstName);
	this.lastName = ko.observable(data.lastName);
}

function HomeViewModel(app, dataModel) {
	var self = this;
	self.books = ko.observableArray([]);
	self.authors = ko.observableArray([]);
	self.pubHouses = ko.observableArray([]);
	self.selectedBook = ko.observable();
	self.test = 5;
	var test2 = 2;
	self.getLookUps = function () {
		$.getJSON(app.dataModel.getLookUps,
			function (data) {
				if (!!data.authors) {
					var authors = $.map(data.authors, function (item) { return new Author(item) });
					self.authors(authors);
				}
				if (!!data.pubHouses) {
					var pubHouses = $.map(data.pubHouses, function (item) { return new PubHouse(item) });
					self.pubHouses(pubHouses);
				}
			}
		);
	}

	self.getLookUps();

	Sammy(function () {
		this.get('#home', function () {
			self.getBooks();
		});
		this.get('/', function () { this.app.runRoute('get', '#home') });
	});


	self.selectBook = function (item) {
		alert(item.title());
		self.selectedBook(item);
	}
	self.getBooks = function () {
		$.getJSON(app.dataModel.getBooksUrl,
                function (allData) {
                	var mappedBooks = $.map(allData, function (item) { return new Book(item, self.pubHouses()) });
                	self.books(mappedBooks);
                }
            );
	}
	self.addBook = function () {
		//self.books.push(new Book());

	}

	self.saveBook = function (book) {
		$.ajax({
			url: app.dataModel.saveBookUrl,
			type: 'POST',
			data:   ko.toJSON(book) ,
			contentType: "application/json;charset=utf-8",
			success: function (data) {
				alert("ok");
			},
			error: function (x, y, z) {
				alert(x + '\n' + y + '\n' + z);
			}
		});
	};

	return self;
}


app.addViewModel({
	name: "Home",
	bindingMemberName: "home",
	factory: HomeViewModel
});
