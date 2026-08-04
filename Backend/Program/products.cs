using API;
namespace API{
    
        public class Products_api
    {
            
            public static void Start()
        {
            var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            var builder = WebApplication.CreateBuilder();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                policy  =>
                                {
                                    policy.AllowAnyOrigin()
                                          .AllowAnyHeader()
                                          .AllowAnyMethod();                                });
            });


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

            app.UseCors(MyAllowSpecificOrigins);

            const int amount_entries = 5;
            app.MapGet("/API/products", () => Products.get(amount_entries));
           
            app.Run();

            
        }
    }
}