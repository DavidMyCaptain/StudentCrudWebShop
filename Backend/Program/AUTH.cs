using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Database;



namespace auth
{
    public static class Auth
    {
        private static string secretKey = "your_super_secret_key_with_at_least_256_bits_for_security";
        private static readonly SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        private static readonly SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        public static string GenerateJwtToken(string username, string auth_level){

            List<Claim>claims = new List<Claim>
            {
                new Claim("User", username), 
                new Claim("Auth_level", auth_level)                              // Custom claims

            };

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
        
            public static void ReadJwtToken(string tokenString)
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
                
                //return();
            }
            private static bool ValidateTokenPayLoad(string tokenString){
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(tokenString);
                string Claimed_Auth = "no_claim";
                string Claimed_Username = "Claimed_Username";
                foreach (var claim in token.Claims)
                {
                    if(claim.Type == "Auth_level")
                    {
                        Claimed_Auth = claim.Value;
                    }
                    if(claim.Type == "User")
                    {
                        Claimed_Username = claim.Value;
                    }                   
                }
                List<Claim>claims = new List<Claim>
                {
                // Unique token ID
                    new Claim("User", Claimed_Username), 
                    new Claim("Auth_level", Claimed_Auth)                              // Custom claims
                };

                var Claimed_token = new JwtSecurityToken(
                claims: claims,
                notBefore: token.ValidFrom,
                expires: token.ValidTo,    // 1 hour expiration
                signingCredentials: credentials
                );

                var New_handler = new JwtSecurityTokenHandler();
                string ClaimedtokenString = New_handler.WriteToken(Claimed_token);
                
                return ClaimedtokenString==tokenString;
                
            }
            
            public static bool PrivledgeCheck(string tokenString, string auth_level)
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(tokenString);
               
                string Claimed_Auth = "No_Privledge";
                foreach (var claim in token.Claims)
                {
                    if(claim.Type == "Auth_level")
                    {
                        Claimed_Auth = claim.Value;
                    }                   
                }

            
            Console.WriteLine("time is" + DateTime.UtcNow);
            if(auth_level ==  Claimed_Auth && token.ValidTo > DateTime.UtcNow && ValidateTokenPayLoad(tokenString))
            {
                
                Console.WriteLine(ValidateTokenPayLoad(tokenString)+ "The privldged go checked");
                return true;
            } else{
                Console.WriteLine("false");
                return false;
            }
            }

    }
}