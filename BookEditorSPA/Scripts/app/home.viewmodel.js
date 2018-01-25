function Book(data) {
	this.title = ko.observable(data.title);
	this.isbn = ko.observable(data.isbn);
}

function HomeViewModel(app, dataModel) {
    var self = this;
	self.books = ko.observableArray([]);
 

    Sammy(function () {
        this.get('#home', function () {
            $.getJSON( app.dataModel.getBooksUrl,                
                function (allData) {
                	var mappedBooks = $.map(allData, function (item) { return new Book(item) });
                	self.books(mappedBooks);
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
