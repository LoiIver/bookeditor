function Book(data) {
	this.title = ko.observable(data.title);
	this.authors = ko.observableArray([]);
	this.numPages = ko.observable(data.numPages);
	this.publishName = ko.observable(data.publishName);
	this.publishYear = ko.observable(data.publishYear);
	this.isbn = ko.observable(data.isbn);
	this.imageUrl = ko.observable(data.imageUrl);
}

function HomeViewModel(app, dataModel) {
    var self = this;
	self.books = ko.observableArray([]);
	self.selectedBook = ko.observable();

    Sammy(function () {
        this.get('#home', function () {
            $.getJSON( app.dataModel.getBooksUrl,                
                function (allData) {
                	var mappedBooks = $.map(allData, function (item) { return new Book(item) });
                	self.books(mappedBooks);
					if (mappedBooks.length >0)
						self.selectedBook(mappedBooks[0]);
                }
            );
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
