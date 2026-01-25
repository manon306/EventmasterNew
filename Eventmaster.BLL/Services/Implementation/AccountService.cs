using Eventmaster.DAL.Entity;

namespace Eventmaster.BLL.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> userManager;
        private readonly IConfiguration config;

        public AccountService(UserManager<User> userManager, IConfiguration config)
        {
            this.userManager = userManager;
            this.config = config;
        }

        public async Task<(bool success, string message)> Register(RegisterVM dto)
        {
            // 1. تعريف المستخدم في متغير أولاً
            var user = new User
            {
                Name = dto.FullName,
                UserName = dto.FullName, // يفضل يكون الـ Email أو اسم بدون مسافات
                Email = dto.Email
            };

            // 2. تمرير المتغير للدالة
            var result = await userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                // الآن 'user' موجود ومعروف في هذا النطاق (Context)
                await userManager.AddToRoleAsync(user, "Registered");
                return (true, "User registered successfully");
            }

            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return (false, errors);
        }

        public async Task<(bool success, string message, string token, DateTime expiration)> Login(LoginVM dto)
        {
            var user = await userManager.FindByNameAsync(dto.userName);
            if (user == null)
                return (false, "Invalid username or password", null, DateTime.MinValue);

            var valid = await userManager.CheckPasswordAsync(user, dto.password);
            if (!valid)
                return (false, "Invalid username or password", null, DateTime.MinValue);

            // Build Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            // Build Token
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(config["JWT:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.Now.AddHours(1);

            var token = new JwtSecurityToken(
                issuer: config["JWT:Issuer"],
                audience: config["JWT:Audience"],
                expires: expiration,
                claims: claims,
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return (true, "Login successful", tokenString, expiration);
        }
        
    }

}
