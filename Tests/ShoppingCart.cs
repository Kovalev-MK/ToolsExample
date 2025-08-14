using Moq;
using ToolsExample.Models;

namespace ToolsExamle.Tests
{
  public class ShoppingCart
  {
    private Product[] products =
    {
      new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
      new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
      new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
      new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
    };

    [Fact]
    public void Sum_Products_Correctly()
    {
      var mock = new Mock<IDiscountHelper>();
      mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(total => total);
      var target = new LinqValueCalculator(mock.Object);
      var goalTotal = products.Sum(e => e.Price);
      var result = target.ValueProducts(products);
      Assert.Equal(goalTotal, result);
    }

    [Fact]
    public void Pass_Through_Variable_Discounts()
    {
      Product[] CreateProduct(decimal value) => [new Product { Price = value }];

      var mock = new Mock<IDiscountHelper>();
      mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>()))
                                      .Returns<decimal>(total => total);
      mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v == 0)))
                                      .Throws<System.ArgumentOutOfRangeException>();
      mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v > 100)))
                                      .Returns<decimal>(total => (total * 0.9M));
      mock.Setup(m => m.ApplyDiscount(It.IsInRange<decimal>(10, 100, Moq.Range.Inclusive)))
                                      .Returns<decimal>(total => total - 5);
      var target = new LinqValueCalculator(mock.Object);
      var FiveDollarDiscount = target.ValueProducts(CreateProduct(5));
      var TenDollarDiscount = target.ValueProducts(CreateProduct(10));
      var FiftyDollarDiscount = target.ValueProducts(CreateProduct(50));
      var HundredDollarDiscount = target.ValueProducts(CreateProduct(100));
      var FiveHundredDollarDiscount = target.ValueProducts(CreateProduct(500));
      Assert.Equal(5, FiveDollarDiscount);
      Assert.Equal(5, TenDollarDiscount);
      Assert.Equal(45, FiftyDollarDiscount);
      Assert.Equal(95, HundredDollarDiscount);
      Assert.Equal(450, FiveHundredDollarDiscount);
      Assert.Throws<ArgumentOutOfRangeException>(() => target.ValueProducts(CreateProduct(0)));
    }
  }
}
