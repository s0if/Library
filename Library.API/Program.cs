
using Library.API.Middleware;
using Library.APPLICATION.DTO.Auth;
using Library.APPLICATION.Mapping;
using Library.APPLICATION.Service;
using Library.APPLICATION.UseCase.Auth;
using Library.APPLICATION.UseCase.Book;
using Library.APPLICATION.UseCase.Category;
using Library.APPLICATION.UseCase.Read;
using Library.APPLICATION.Utils;
using Library.BERSISTENCE;
using Library.BERSISTENCE.Repository;
using Library.DOMAIN.Interface;
using Library.DOMAIN.MODEL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;
using System.Threading.Tasks;


namespace Library.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                ));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.User.AllowedUserNameCharacters = null;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddScoped<ApplicationDbContext>();
            builder.Services.AddScoped<RegisterDTOs>();
            builder.Services.AddScoped<RegisterMap>();
            builder.Services.AddScoped<RegisterUseCase>();
            builder.Services.AddScoped<LoginUseCase>();
            builder.Services.AddScoped<AuthServices>();
            builder.Services.AddScoped<LoginDTOs>();
            builder.Services.AddScoped< EmailSettings>();
            builder.Services.AddScoped<ConfirmEmailUseCase>();
            builder.Services.AddScoped<sendEmail> ();
            builder.Services.AddScoped<ChangePasswordUseCase>();
            builder.Services.AddScoped<ForgetPasswordUseCase>();
            builder.Services.AddScoped<resetPasswordUseCase>();
            builder.Services.AddScoped<ChangePasswordDTOs>();
            builder.Services.AddScoped<ISBlockUseCase>();
            builder.Services.AddScoped<SeedData>();
            builder.Services.AddScoped<IBookInterface, BookRepository>();
            builder.Services.AddScoped<ICategoryInterface, CategoryRepository>();
            builder.Services.AddScoped<AddCategoryUseCase>();
            builder.Services.AddScoped<GetCategoryUseCase>();
            builder.Services.AddScoped<UpdateCategoryUseCase>();
            builder.Services.AddScoped<DeleteCategoryUseCase>();
            builder.Services.AddScoped<CategoryMap>();
            builder.Services.AddScoped<BookMap>();
            builder.Services.AddScoped<AddBookUseCase>();
            builder.Services.AddScoped<UpdateBookUseCase>();
            builder.Services.AddScoped<DeleteBookUseCase>();
            builder.Services.AddScoped<GetBookUseCase>();
            builder.Services.AddScoped<IReadInterface, ReadRepository>();
            builder.Services.AddScoped<Library.APPLICATION.Service.TimeZone>();
            builder.Services.AddScoped<ReadMap>();
            builder.Services.AddScoped<StartReadUseCase>();
            builder.Services.AddScoped<FinishReadUseCase>();
            builder.Services.AddScoped<GetRoleUseCase>();
            builder.Services.AddScoped<GetAllReadsByBookUseCase>();
            builder.Services.AddScoped<GetAllReadsByUserUseCase>();
            builder.Services.AddScoped<GetUserMap>();

            builder.Services.AddAuthentication(
          options =>
          {
              options.DefaultAuthenticateScheme = "Bearer";
              options.DefaultChallengeScheme = "Bearer";
          }
          ).AddJwtBearer(options =>
          {
              options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
              {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  //from model AuthServices
                  ValidIssuer = builder.Configuration.GetSection("jwt")["issuer"],
                  ValidAudience = builder.Configuration.GetSection("jwt")["audience"],
                  ValidateLifetime = true,
                  IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["jwt:secretkey"])
                  ),
                 
              };
          }
          );


            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }
            // Seed Data
            using var scope = app.Services.CreateScope();
            var seedData = scope.ServiceProvider.GetRequiredService<SeedData>();
             await seedData.DataSeedings();
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
