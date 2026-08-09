using System.Reflection.Metadata;
using Database;
using Npgsql.Replication.PgOutput.Messages;

namespace API
{
 
    static public class Products
    {
        
       
        public static SingularProduct[] get(int amount)
        {
            
            SingularProduct[] list_product = new SingularProduct[amount];
            for(int i= 0; i <amount; i++)
            {
                list_product[i] = new SingularProduct();
                list_product[i] = Get_Database(i.ToString()); 
            }


            

            return list_product;
        }
        private static SingularProduct Get_Database(string id)
        {
            SingularProduct The_Product = new SingularProduct();

            DatabaseInterface database_instance = new DatabaseInterface();
            The_Product = database_instance.get_product(id);
            
            return The_Product;
        }
        
       
    }
}
public class SingularProduct
        {
            public SingularProduct(){
                Name = "";
                Link = "";
            }
            public int Id { get; set; }
            public int Price { get; set; }
            public string Name { get; set; }
            public string Link { get; set; }
            public string Desctiption { get; set; }
        }