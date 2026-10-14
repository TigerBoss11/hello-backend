using HelloBackend.Core;
using Npgsql;

var connectionString = File.ReadAllText("connection.local.txt").Trim();
if (string.IsNullOrWhiteSpace(connectionString))
{
    Console.WriteLine("connection.local.txt is empty");
    return;
}

await using var connection = new NpgsqlConnection(connectionString);
await connection.OpenAsync();

await using var command = new NpgsqlCommand("SELECT 1", connection);
var result = await command.ExecuteScalarAsync();
Console.WriteLine($"Connected. SELECT 1 => {result}");

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