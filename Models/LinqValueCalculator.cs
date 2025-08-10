namespace ToolsExample.Models
{
  public class LinqValueCalculator : IValueCalculator
  {
    private IDiscountHelper discounter;

    public LinqValueCalculator(IDiscountHelper discounter) => this.discounter = discounter;

    public decimal ValueProducts(IEnumerable<Product> products) =>
      discounter.ApplyDiscount(products.Sum(p => p.Price));
  }
}
