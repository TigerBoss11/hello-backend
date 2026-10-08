namespace HelloBackend.Core.Tests;

public class CatalogQueriesTests
{
    [Fact]
    public void LowStock_WhenThresholdIs20_Returns2Items()
    {
        var catalogItems = new List<CatalogItem>(){
            new CatalogItem("Book", 10),
            new CatalogItem("Book", 15),
            new CatalogItem("Book", 20),
            new CatalogItem("Book", 25),
            new CatalogItem("Book", 30),
            new CatalogItem("Book", 35),
            new CatalogItem("Book", 40),
        };

        var queries = new CatalogQueries();
        var queriesLowStock = queries.GetLowStock(catalogItems, 20);

        Assert.Equal(2, queriesLowStock.Count);
        Assert.Equal("Book", queriesLowStock[0].Title);
        Assert.Equal(10, queriesLowStock[0].Quantity);
        Assert.Equal("Book", queriesLowStock[1].Title);
        Assert.Equal(15, queriesLowStock[1].Quantity);
    }

    [Fact]
    public void LowStock_WhenThresholdIs0_ThrowsArgumentOutOfRangeException()
    {
        var catalogItems = new List<CatalogItem>(){
            new CatalogItem("Book", 10),
            new CatalogItem("Book", 15),
            new CatalogItem("Book", 20),
        };

        var queries = new CatalogQueries();

        Assert.Throws<ArgumentOutOfRangeException>(
            () => queries.GetLowStock(catalogItems, 0));
    }

    [Fact]
    public void FindByTitle_WhenTitleIsEmpty_ThrowsArgumentException()
    {
        var catalogItems = new List<CatalogItem>(){
            new CatalogItem("Book", 10),
            new CatalogItem("Book", 15),
            new CatalogItem("Book", 20),
        };

        var queries = new CatalogQueries();

        Assert.Throws<ArgumentException>(
            () => queries.FindByTitle(catalogItems, ""));
        Assert.Throws<ArgumentException>(
            () => queries.FindByTitle(catalogItems, " "));
        Assert.Throws<ArgumentException>(
            () => queries.FindByTitle(catalogItems, null));
    }
}
