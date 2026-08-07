using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Database;


namespace auth
{
    public class Auth
    {
        private static string secretKey = "your_super_secret_key_with_at_least_256_bits_for_security";
        private static readonly SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        private static readonly SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        private readonly  List<Claim>claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, "user123"),      // Subject (User ID)
                new Claim(JwtRegisteredClaimNames.Email, "user@example.com"),
                new Claim(JwtRegisteredClaimNames.Name, "John Doe"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique token ID
                new Claim("role", "admin"),                              // Custom claims
                new Claim("permission", "read"),
                new Claim("permission", "write")
            };
        public string GenerateJwtToken(){

            var token = new JwtSecurityToken(
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddHours(1),    // 1 hour expiration
            signingCredentials: credentials
            );
            var handler = new JwtSecurityTokenHandler();
            string tokenString = handler.WriteToken(token);
            return tokenString;
            }
        
            void ReadJwtToken(string tokenString)
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(tokenString);
                // Header information
                Console.WriteLine($"Algorithm: {token.Header.Alg}");
                Console.WriteLine($"Type: {token.Header.Typ}");
                // Metadata
                Console.WriteLine($"Issuer: {token.Issuer}");
                Console.WriteLine($"Audience: {string.Join(", ", token.Audiences)}");
                Console.WriteLine($"Valid From: {token.ValidFrom}");
                Console.WriteLine($"Valid To: {token.ValidTo}");
                // All claims
                foreach (var claim in token.Claims)
                {
                    Console.WriteLine($"{claim.Type}: {claim.Value}");
                }
            }
            public void Start()
            {
            string token = GenerateJwtToken();
            Console.WriteLine("Generated Token:");
            Console.WriteLine(token);
            // Read and display
            ReadJwtToken(token);
            }
        
    }
}