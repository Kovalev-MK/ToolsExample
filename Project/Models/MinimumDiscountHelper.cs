namespace ToolsExample.Models
{
  public class MinimumDiscountHelper : IDiscountHelper
  {
    public decimal ApplyDiscount(decimal totalParam)
    {
      if (totalParam < 0)
        throw new ArgumentOutOfRangeException(nameof(totalParam));
      if (totalParam > 100)
        return totalParam * 0.9m;
      if (totalParam >= 10 && totalParam <= 100)
        return totalParam - 5;
      return totalParam;
    }
  }
}
