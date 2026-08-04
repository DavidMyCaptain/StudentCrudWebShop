using System.Reflection.Metadata;

namespace API
{
 
    static public class Products
    {
        
       
        public static SingularProduct[] get(int amount)
        {
            SingularProduct[] list_product = new SingularProduct[amount];
            list_product[0] = new SingularProduct();
            list_product[0].Name = "cookie";
            list_product[0].Link = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b4/Choco_chip_cookie.png/500px-Choco_chip_cookie.png";
            list_product[0].Price = 10;
            list_product[0].Id = 0;
            return list_product;
        }
        public static SingularProduct[] get()
        {
            SingularProduct[] list_product = new SingularProduct[10];
            list_product[0] = new SingularProduct();
            list_product[0].Name = "cookie";
            list_product[0].Link = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b4/Choco_chip_cookie.png/500px-Choco_chip_cookie.png";
            list_product[0].Price = 10;
            list_product[0].Id = 0;
            return list_product;
        }
        public class SingularProduct
        {
            public int Id { get; set; }
            public int Price { get; set; }
            public string Name { get; set; }
            public string Link { get; set; }
        }
       
    }
}
