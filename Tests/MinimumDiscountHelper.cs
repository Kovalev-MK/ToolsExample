using ToolsExample.Models;

namespace ToolsExamle.Tests
{
  public class MinimumDiscountHelper
  {
    private IDiscountHelper GetTestObject() => new ToolsExample.Models.MinimumDiscountHelper();

    [Fact]
    public void Discount_Above_100()
    {
      var target = GetTestObject();
      var total = 200m;
      var discountTotal = target.ApplyDiscount(total);
      Assert.Equal(total * 0.9m, discountTotal);
    }

    [Fact]
    public void Discount_Between_10_and_100()
    {
      var target = GetTestObject();
      var tenDollarDiscount = target.ApplyDiscount(10);
      var hundredDollarDiscount = target.ApplyDiscount(100);
      var fiftyDollarDiscount = target.ApplyDiscount(50);
      Assert.Equal(5, tenDollarDiscount);
      Assert.Equal(95, hundredDollarDiscount);
      Assert.Equal(45, fiftyDollarDiscount);
    }

    [Fact]
    public void Discount_Less_Than_10()
    {
      var target = GetTestObject();
      
      var discount5 = target.ApplyDiscount(5);
      var discount0 = target.ApplyDiscount(0);
      
      Assert.Equal(5, discount5);
      Assert.Equal(0, discount0);
    }

    [Fact]
    public void Discount_Negative_Total()
    {
      var target = GetTestObject();
      Assert.Throws<ArgumentOutOfRangeException>(() => target.ApplyDiscount(-1));
    }
  }
}