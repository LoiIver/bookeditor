function AppDataModel() {
    var self = this;
    // Routes  
    self.siteUrl = "/";

    // Route operations
    self.getBooksUrl = "/api/books";

    self.getLookUps = "/api/lookups";

	self.saveBookUrl = "/api/books";
    
    // Other private operations

    // Operations

    // Data
    self.returnUrl = self.siteUrl;   
}
