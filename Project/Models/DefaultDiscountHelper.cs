namespace ToolsExample.Models
{
  public class DefaultDiscountHelper : IDiscountHelper
  {
    private decimal discountSize;

    public DefaultDiscountHelper(decimal discountSize) => this.discountSize = discountSize;

    public decimal ApplyDiscount(decimal totalParam) => (totalParam - (discountSize / 100m * totalParam));
  }
}
