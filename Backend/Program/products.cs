using API;
namespace API{
    
        public class Products_api
    {
            
            public static void Start()
        {
            var builder = WebApplication.CreateBuilder();

            // Add services to the container.
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            

            app.MapGet("/API/products", () => Products.Get());
           
            app.Run();

            
        }
    }
}