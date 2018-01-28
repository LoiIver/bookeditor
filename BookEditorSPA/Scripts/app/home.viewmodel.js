function Book(data) {
	if (!data)
		return;
	this.title = ko.observable(data.title);
	this.authors = ko.observableArray(data.authors);
	this.authorsNames = ko.observable(data.authorsNames);

	this.numPages = ko.observable(data.numPages);
	this.pubHouseId = ko.observable(data.pubHouseId);
	this.pubHouseName = ko.observable(data.pubHouseName);

	this.publishYear = ko.observable(data.publishYear);
	this.isbn = ko.observable(data.isbn);
	this.imageUrl = ko.observable(data.imageUrl);
	this.bookId = ko.observable(data.bookId);
	this.illustrationUrl = ko.observable(data.illustrationUrl);
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
	self.unchangeableBooks = []; 
	self.authors = ko.observableArray([]);
	self.pubHouses = ko.observableArray([]);
	self.selectedBook = ko.observable();

	self.byNameSorted = ko.observable(false);
	self.byPublishYearSorted = ko.observable(false);
	self.acsendingSorOrder = ko.observable(false);


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
		self.selectedBook(item);
	}
	self.getBooks = function () {
		$.getJSON(app.dataModel.booksUrl,
                function (allData) {
                	var mappedBooks = $.map(allData, function (item) { return new Book(item, self.pubHouses()) });
                	self.books(mappedBooks);
	                self.unchangeableBooks = mappedBooks;
                }
            );
	}
	self.addBook = function () {
		self.selectedBook(new Book({ title: "Новая книга" }));
	}

	self.saveBook = function (book) {
		$.ajax({
			url: app.dataModel.booksUrl,
			type: 'PUT',
			data: ko.toJSON(book),
			contentType: "application/json;charset=utf-8",
			success: function (data) {
				self.getBooks();
				self.selectedBook(null);
				
			},
			error: function (x, y, z) {
				alert(x + '\n' + y + '\n' + z);
			}
		});
	};

	self.sortBooks = function (sortBy, name) {
		switch (sortBy) {
			case "title":
				self.acsendingSorOrder(!self.acsendingSorOrder());
				self.books.sort(function (left, right) {
					return (left.title() === right.title() ? 0 : (left.title() < right.title() ? -1 : 1))
					* (self.acsendingSorOrder() ? 1 : -1);
				});
				self.byNameSorted(true);
				self.byPublishYearSorted(false);
				break;
			case "publishYear":
				self.acsendingSorOrder(!self.acsendingSorOrder());
				self.books.sort(function (left, right) {
					return (left.publishYear() === right.publishYear() ? 0 : (left.publishYear() < right.publishYear() ? -1 : 1))
						* (self.acsendingSorOrder() ? 1 : -1);
				});
				self.byNameSorted(false);
				self.byPublishYearSorted(true);
				break;
			default:
		}
	};

	self.removeBook = function (book) {
		if (confirm()) {
			$.ajax({
				url: app.dataModel.booksUrl + "/" + book.bookId(),
				type: "DELETE",
				//contentType: "application/json;charset=utf-8",
				success: function () {
					self.getBooks();
				},
				error: function () {
					alert('Удаление книги "' + book.title + '" невозможно');
				}
			});
		}
	}

	var refreshSelected = function (bookId) {
		var book = self.unchangeableBooks.find(function(item) { return item.bookId === bookId; });
		self.selectedBook().title(book.title);
		self.selectedBook().authors(book.authors);
		self.selectedBook().authorsNames(book.authorsNames);

		self.selectedBook().numPages(book.numPages);
		self.selectedBook().pubHouseId(book.pubHouseId);
		self.selectedBook().pubHouseName(book.pubHouseName);

		self.selectedBook().publishYear(book.publishYear);
		self.selectedBook().isbn(book.isbn);
		self.selectedBook().imageUrl(book.imageUrl);
		self.selectedBook().bookId(book.bookId);
		/*или так http://www.knockmeout.net/2011/03/guard-your-model-accept-or-cancel-edits.html*/
	}

	self.cancelEdit = function (item) {
		if (!!item.bookId()) {
			refreshSelected(item.bookId());
		};
		self.selectedBook(null);
	}

	
	return self;
}


app.addViewModel({
	name: "Home",
	bindingMemberName: "home",
	factory: HomeViewModel
});
