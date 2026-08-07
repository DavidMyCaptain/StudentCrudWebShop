using API;
using System.Text.Json.Nodes;
using Database;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

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

            const int amount_entries = 5;
            app.MapGet("/API/products", () => Products.get(amount_entries));


            app.MapPost("/API/login", (HttpRequest req) =>
            {   
                string jsonString = new StreamReader(req.Body).ReadToEnd();
                var node = JsonNode.Parse(jsonString);
                string username = node?["username"]?.GetValue<string>();
                string password = node?["password"]?.GetValue<string>();
                DatabaseInterface auth_check = new DatabaseInterface();
                auth_check.Authentication(username, password);
                /*var token = GenerateJwtTokenFor(loginRequest.Username);
                return Results.Ok(new { token });*/
            });
           
            app.Run();

            
        }
    }
}