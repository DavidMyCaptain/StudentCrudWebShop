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

}
}