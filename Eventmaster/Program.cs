
using AutoMapper;
using Eventmaster.API.Hubs;
using Eventmaster.BLL.Mapper;
using Eventmaster.BLL.Services.Abstraction;
using Eventmaster.BLL.Services.Implementation;
using Eventmaster.DAL.DataBase;
using Eventmaster.DAL.Entity;
using Eventmaster.DAL.Repo.Abstraction;
using Eventmaster.DAL.Repo.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace WebApi.net
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<IEventRepo, EventRepo>();
            builder.Services.AddScoped<IEventService, EventService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<ISavedEventServices, SavedEventServices>();
            builder.Services.AddScoped<ISavedEvent, SavedEvent>();
            builder.Services.AddScoped<IAttachmentRepo, AttachmentRepo>();
            builder.Services.AddScoped<IAttachmentServices, AttachmentServices>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<IAdminRepo, AdminRepo>();
            builder.Services.AddAutoMapper(typeof(DomainProfile));

            // Add signalR services
            builder.Services.AddSignalR();

            // builder.Services.AddAutoMapper(typeof(DomainProfile));
            // The error is caused by missing using directives for AutoMapper and its extensions.
            // Add services to the container.
            //----------------------------------------------------------------------------
            // to suppress automatic model state validation
            // so that we can handle it manually
            // in the controller actions
            // this is useful when we want to return
            // custom error responses
            // instead of the default 400 Bad Request
            // with validation errors
            // see DTO/GeneralResponse.cs and Controllers/EmployeeController.cs for usage
            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                options.SuppressModelStateInvalidFilter = true);
            //----------------------------------------------------------------------------
            builder.Services.AddDbContext<Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("cs")));
            //----------------------------------------------------------------------------
            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<Context>();
            // enabling CORS
            // for all the incoming requests
            // you can specify particular origins also
            // e.g. .WithOrigins("http://example.com")
            // here we are allowing any origin, method and header
            // for demonstration purposes
            // in production, restrict these as needed
            // CORS - Cross-Origin Resource Sharing
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("myPolicy", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
            //----------------------------------------------------------------------------
            // add authentication services
            // this is required to use authentication middleware
            // which validates the incoming requests
            // based on the authentication scheme configured
            builder.Services.AddAuthentication(options =>
            {
                // check JWT token in the Authorization header
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // when user is authenticated
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // when authentication fails return 401 Unauthorized
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; // used by default for all the other schemes
            }).AddJwtBearer(options => // configuring JWT Bearer authentication to verify JWT tokens
            {
                options.SaveToken = true; // save the token in the authentication properties after a successful authorization
                options.RequireHttpsMetadata = false; // for development only , in production set it to true to enforce HTTPS
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:Audience"], // expected audience value
                    ValidIssuer = builder.Configuration["JWT:Issuer"], // expected issuer value
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                         System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])) // secret key used to sign the token
                };
            });
            //----------------------------------------------------------------------------
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token.\r\n\r\nExample: \"Bearer eyJhbGciOi...\""
                });

                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
             });


            var app = builder.Build();
            // --------------------------------------------------------------------------
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                string[] roles = { "Admin", "Registered", "Participant" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            // to serve static files from wwwroot folder 
            // like html,css,js,image files
            app.UseStaticFiles();

            // enabling CORS middleware
            // must be added before UseAuthorization
            // to allow cross-origin requests

            app.UseCors("myPolicy");
            app.UseAuthentication();   //==> by default authentication middleware is not added

            app.UseAuthorization();


            app.MapControllers();
            app.MapHub<NotificationHub>("/notificationHub");
            app.Run();
            
        }
    }
}