using API;
using System.Text.Json.Nodes;
using Database;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using auth;
using System.Diagnostics.CodeAnalysis;

namespace API{
    
        public static class Api
    {
            
            public static void Start_api()
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

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.AllowSynchronousIO = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseCors(MyAllowSpecificOrigins);

            Product_api(app);
            login_rutine(app);

            app.MapPost("/api/protected/new_product", (HttpRequest req) =>
            {   
                string jsonString = new StreamReader(req.Body).ReadToEnd();
                var node = JsonNode.Parse(jsonString);
                string token = node?["token"]?.GetValue<string>();
                 if (Auth.PrivledgeCheck(token, "Admin"))
                {
                    DatabaseInterface PostNewProduct = new DatabaseInterface();
                    Console.WriteLine("The Id: "+node?["product_id"]?.GetValue<string>());
                    PostNewProduct.post_product(node?["product_name"]?.GetValue<string>(),node?["product_price"]?.GetValue<string>(),node?["product_image"]?.GetValue<string>(), node?["product_id"]?.GetValue<string>(), node?["product_description"]?.GetValue<string>());
                }
            });
            app.MapPost("/api/protected/AuthCheck", (HttpRequest req) =>
            {  
                string jsonString = new StreamReader(req.Body).ReadToEnd();
                var node = JsonNode.Parse(jsonString);
                string token = node?["token"]?.GetValue<string>();
                 if (Auth.PrivledgeCheck(token, "Admin"))
                {
                    return Results.Ok();
                }
                return Results.Unauthorized();;
            });
           
            app.Run();

            
        }

        private static void Product_api(WebApplication app)
        {
            const int amount_entries = 5;
            app.MapGet("/API/products", () => Products.get(amount_entries));
        }

        private static void login_rutine(WebApplication app)
        {
            app.MapPost("/API/login", (HttpRequest req) =>
            {   
                string jsonString = new StreamReader(req.Body).ReadToEnd();
                var node = JsonNode.Parse(jsonString);
                string username = node?["username"]?.GetValue<string>();
                string password = node?["password"]?.GetValue<string>();
                DatabaseInterface auth_check = new DatabaseInterface();
                string auth_level = auth_check.Authentication(username, password);
                var token = Auth.GenerateJwtToken(username, auth_level);
                return Results.Ok(new { token });
            });
        }
    }
}