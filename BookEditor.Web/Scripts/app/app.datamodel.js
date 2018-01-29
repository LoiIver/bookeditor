function AppDataModel() {
    var self = this;
    // Routes  
    self.siteUrl = "/";

    // Route operations
    self.booksUrl = "/api/books";
    self.bookImgUploadUrl = "/api/books/upload";

    self.getLookUps = "/api/lookups";
    
    // Other private operations

    // Operations

    // Data
    self.returnUrl = self.siteUrl;   
}
