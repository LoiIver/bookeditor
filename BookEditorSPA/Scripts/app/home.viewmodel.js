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
	//this.imageUrl = ko.observable(data.imageUrl);
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
	self.needUploadImage = ko.observable(false);

	self.byNameSorted = ko.observable();
	self.byPublishYearSorted = ko.observable();
	self.ascendingSortOrder = ko.observable(true);


	self.buyer = { name: 'Franklin', credits: 250 };
	self.seller = { name: 'Mario', credits: 5800 };

	var saveToStore = function (sortedByName, sortedByPublishYear, ascendingSortOrder) {
		localStorage.setItem("sortedByName", sortedByName);
		localStorage.setItem("sortedByPublishYear", sortedByPublishYear);
		localStorage.setItem("ascendingSortOrder", ascendingSortOrder);
	}
	var sort = function (sortBy) {
		switch (sortBy) {
			case "title":
				self.books.sort(function (left, right) {
					return (left.title() === right.title() ? 0 : (left.title() < right.title() ? -1 : 1))
						* (self.ascendingSortOrder() === true ? 1 : -1);
				});
				self.byNameSorted(true);
				self.byPublishYearSorted(false);
				break;
			case "publishYear":				
				self.books.sort(function (left, right) {
					return (left.publishYear() === right.publishYear() ? 0 : (left.publishYear() < right.publishYear() ? -1 : 1))
						* (self.ascendingSortOrder() === true ? 1 : -1);
				});
				self.byNameSorted(false);
				self.byPublishYearSorted(true);
				break;
		}
		saveToStore(self.byNameSorted(), self.byPublishYearSorted(), self.ascendingSortOrder());
	}
	var restoreSortOrder = function () {
		var name = localStorage.getItem("sortedByName");
		var publishYear = localStorage.getItem("sortedByPublishYear");
		var ascOrder = localStorage.getItem("ascendingSortOrder");
		if (ascOrder != null)
			self.ascendingSortOrder(ascOrder==="true");
		if (name != null) {
			self.byNameSorted(name==="true");
			if (name == "true")
				sort("title");
		}
		if (publishYear != null) {
			self.byPublishYearSorted(publishYear === "true");
			if (publishYear == "true")
				sort("publishYear");
		}
	}
	
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

	self.selectBook = function (item) {
		self.selectedBook(item);
	}
	self.getBooks = function () {
		$.getJSON(app.dataModel.booksUrl,
                function (allData) {
                	var mappedBooks = $.map(allData, function (item) { return new Book(item, self.pubHouses()) });
                	self.books(mappedBooks);
                	self.unchangeableBooks = allData;
                	restoreSortOrder();
                }
            );
	}

	self.addBook = function () {
		self.selectedBook(new Book({ title: "Новая книга", numPages: 1, publisYear: (new Date()).getFullYear(), pubHouseId : null }));
	}

	self.uploadImg = function (item) {
		var formData = new FormData();
		var fileSelect = document.getElementById('inputFile');
		var file = fileSelect.files[0];
		if (!!file && (file.type.match('image.*'))) {
			formData.append('illustration', file, file.name);
			$.ajax({
				url: app.dataModel.bookImgUploadUrl,
				data: formData,
				processData: false,
				contentType: false,
				type: 'POST',
				success: function (data) {
					alert(data);
				}
			});
		}
	}
	
	self.saveBook = function (book) {
		if ($("form input:invalid").length > 0) {
			alert("Сохранение нвеозможно, исправьте, пожалуйста, ошибки");
			return;
		}
		var method = "PUT";
		if (!book.bookId())
			method = "POST";

		$.ajax({
			url: app.dataModel.booksUrl,
			type: method,
			data: ko.toJSON(book),
			contentType: "application/json;charset=utf-8",
			success: function (message) {
				alert(message);
				self.getBooks();
				self.selectedBook(null);
			},
			error: function (data) {
				var message = JSON.parse(data.responseText).message;
				alert(message);
			}
		});
	};

	self.sortBooks = function (sortBy, data) {
		debugger;
		if (sortBy ==="title")
			if (self.byNameSorted())
				self.ascendingSortOrder(!self.ascendingSortOrder());
			else 
				(self.ascendingSortOrder(true));
		if (sortBy === "publishYear")
			if (self.byPublishYearSorted())
				self.ascendingSortOrder(!self.ascendingSortOrder());
			else
				(self.ascendingSortOrder(true));
		sort(sortBy);
	};

	
	self.removeBook = function (book) {
		if (confirm("Вы действительно хотите удалить книгу \""+ book.title()+"\"?")) {
			$.ajax({
				url: app.dataModel.booksUrl + "/" + book.bookId(),
				type: "DELETE",				
				success: function (message) {
					alert(message);
					self.getBooks();
				},
				error: function (data) {
					var message = JSON.parse(data.responseText).message;
					alert(message);
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
	
		//self.selectedBook().imageUrl(book.imageUrl);
		//self.selectedBook().bookId(book.bookId);
		/*или так http://www.knockmeout.net/2011/03/guard-your-model-accept-or-cancel-edits.html*/
	}

	self.cancelEdit = function (item) {
		if (!!item.bookId()) {
			refreshSelected(item.bookId());
		};
		self.selectedBook(null);
	}
	Sammy(function () {
		this.get('#home', function () {
			self.getBooks();
		});
		this.get('#authors', function () {
			self.getAuthors();
		});
		this.get('/', function () { this.app.runRoute('get', '#home') });
	});
	
	return self;
}


app.addViewModel({
	name: "Home",
	bindingMemberName: "home",
	factory: HomeViewModel
});
