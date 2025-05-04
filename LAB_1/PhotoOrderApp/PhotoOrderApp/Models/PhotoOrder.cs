using System.ComponentModel.DataAnnotations;

public class PhotoOrder
{
    [Required]
    public string Size { get; set; }

    [Required]
    public string PaperType { get; set; }

    [Required, Range(1, 100)]
    public int Quantity { get; set; }

    public decimal CalculateTotal()
    {
        return GetPrice(Size, PaperType) * Quantity;
    }

    public static decimal GetPrice(string size, string paper)
    {
        if (size == "10x15" && paper == "Глянцевий") return 5;
        if (size == "10x15" && paper == "Матовий") return 6;
        if (size == "15x20" && paper == "Глянцевий") return 8;
        if (size == "15x20" && paper == "Матовий") return 9;
        return 0;
    }
}