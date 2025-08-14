namespace ToolsExample.Models
{
  public interface IValueCalculator
  {
    decimal ValueProducts(IEnumerable<Product> products);
  }
}
