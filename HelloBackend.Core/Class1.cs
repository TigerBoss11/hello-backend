namespace HelloBackend.Core;

public interface ICatalogQueries
{
    List<CatalogItemInfo> GetLowStock(IEnumerable<CatalogItem> items, int threshold);
    Task<List<CatalogItemInfo>> GetLowStockAsync(IEnumerable<CatalogItem> items, int threshold);
    CatalogItem? FindByTitle(IEnumerable<CatalogItem> items, string title);
}
public class CatalogItem
{
    public string Title { get; private set; }
    public int Quantity { get; private set; }
    public CatalogItem(string title, int quantity){
        Title = title;
        Quantity = quantity;
    }
    public void AddStock(int amount){
        Quantity += amount;
    }
    public CatalogItemInfo GetInfo(){
        return new CatalogItemInfo(Title, Quantity);
    }
}
public record CatalogItemInfo(string Title, int Quantity);
public class CatalogQueries : ICatalogQueries
{
    public List<CatalogItemInfo> GetLowStock(IEnumerable<CatalogItem> items, int threshold){
        if(threshold <= 0){
            throw new ArgumentOutOfRangeException(nameof(threshold), "Threshold must be greater than 0");
        }
        return items
            .Where(item => item.Quantity < threshold)
            .Select(item => item.GetInfo())
            .ToList();
    }
    public async Task<List<CatalogItemInfo>> GetLowStockAsync(IEnumerable<CatalogItem> items, int threshold){
        if(threshold <= 0){
            throw new ArgumentOutOfRangeException(nameof(threshold), "Threshold must be greater than 0");
        }
        await Task.Delay(1000);
        return items
            .Where(item => item.Quantity < threshold)
            .Select(item => item.GetInfo())
            .ToList();
    }
    public CatalogItem? FindByTitle(IEnumerable<CatalogItem> items, string title){
        if(string.IsNullOrWhiteSpace(title)){
            throw new ArgumentException("Title cannot be empty or whitespace", nameof(title));
        }
        return items.FirstOrDefault(item => item.Title == title);
    }
}