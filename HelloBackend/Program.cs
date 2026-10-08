using HelloBackend.Core;

var catalogItems = new List<CatalogItem>(){
    new CatalogItem("Book", 10),
    new CatalogItem("Book", 15),
    new CatalogItem("Book", 20),
    new CatalogItem("Book", 25),
    new CatalogItem("Book", 30),
    new CatalogItem("Book", 35),
    new CatalogItem("Book", 40),
    new CatalogItem("Book", 45),
    new CatalogItem("Book", 50),
};

ICatalogQueries queries = new CatalogQueries();
try{
    var lowStockItems = await queries.GetLowStockAsync(catalogItems, 20);
    foreach (var item in lowStockItems){
        Console.WriteLine(item);
    }
}
catch (ArgumentOutOfRangeException ex){
    Console.WriteLine(ex.Message);
}
var book = queries.FindByTitle(catalogItems, "dasd");
if(book == null){
    Console.WriteLine("Book not found");
}
else{
    Console.WriteLine(book.GetInfo());
}