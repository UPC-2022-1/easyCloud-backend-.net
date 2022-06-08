namespace easyCloud.Quote.Resources;

public class QuoteResource
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string Title { get; set; }
    public string Date { get; set; }
    public float Price { get; set; }
    public string CloudService { get; set; }
    public int UserId { get; set; }
}