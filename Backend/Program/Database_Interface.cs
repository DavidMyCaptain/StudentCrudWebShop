using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API;

namespace Database{
public class DatabaseInterface{
    public SingularProduct get_product(string id)
        {

            var connectionString = "Host=localhost;Port=5501;Username=postgres;Password=Datait2026!;Database=WebShop;";
            using var dataSource = NpgsqlDataSource.Create(connectionString);
            
            var command = dataSource.CreateCommand("Select *from Product where productid =" + id);
            var reader = command.ExecuteReader();
            SingularProduct data_Product = new SingularProduct();
            while (reader.Read())
            {

                data_Product.Id = reader.GetInt32(0);
                data_Product.Price = reader.GetInt32(1);
                data_Product.Name = reader.GetString(2);
                data_Product.Link = reader.GetString(3);
                data_Product.Desctiption = reader.GetString(4);
            }
            return data_Product;
            
        }
        public void post_product(string ProductName, string ProductValue, string ProductLink, string ProductId, string ProductDescription)
        {

            var connectionString = "Host=localhost;Port=5501;Username=postgres;Password=Datait2026!;Database=WebShop;";
            using var dataSource = NpgsqlDataSource.Create(connectionString);
            
            var command = dataSource.CreateCommand("INSERT INTO Product (Productid,ProductPrice, ProductName, ProductLink, ProductDescription)VALUES ("+ProductId+","+ ProductValue+",'"+ProductName+"','"+ProductLink + "','"+ProductDescription +"');");
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader.GetString(0));
            }
            
        }
        public string Authentication(string Username, string Password)
        {

            var connectionString = "Host=localhost;Port=5501;Username=postgres;Password=Datait2026!;Database=WebShop;";
            using var dataSource = NpgsqlDataSource.Create(connectionString);
            
            var command = dataSource.CreateCommand("SELECT Auth_level from Auth where Username = '" + Username+"'");
            var reader = command.ExecuteReader();

            string[] str = new string[1];
            while (reader.Read())
            {
                Console.WriteLine(reader.GetString(0));
            str[0] = reader.GetString(0);
            }

            return str[0];
            
        }

}
}