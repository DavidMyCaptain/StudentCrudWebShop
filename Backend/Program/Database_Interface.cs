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
            }
            return data_Product;
            
        }
        public string[] Authentication(string Username, string Password)
        {

            var connectionString = "Host=localhost;Port=5501;Username=postgres;Password=Datait2026!;Database=WebShop;";
            using var dataSource = NpgsqlDataSource.Create(connectionString);
            
            var command = dataSource.CreateCommand("SELECT * from Auth where Username = '" + Username+"'");
            var reader = command.ExecuteReader();

            string[] str = new string[3];
            while (reader.Read())
            {
                Console.WriteLine(reader.GetString(0));
            str[0] = reader.GetString(0);
                Console.WriteLine(reader.GetString(1));
            str[1] = reader.GetString(1);
                Console.WriteLine(reader.GetString(2));
            str[2] = reader.GetString(2);
            Console.WriteLine(str);
            }

            return str;
            
        }

}
}