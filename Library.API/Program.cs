
using CloudinaryDotNet;
using Library.API.Middleware;
using Library.APPLICATION.DTO.Auth;
using Library.APPLICATION.DTO.Checkout;
using Library.APPLICATION.DTO.Publisher;
using Library.APPLICATION.Mapping;
using Library.APPLICATION.Service;
using Library.APPLICATION.UseCase.Auth;
using Library.APPLICATION.UseCase.Book;
using Library.APPLICATION.UseCase.Category;
using Library.APPLICATION.UseCase.Publisher;
using Library.APPLICATION.UseCase.Read;
using Library.APPLICATION.Utils;
using Library.BERSISTENCE;
using Library.BERSISTENCE.Repository;
using Library.DOMAIN.Interface;
using Library.DOMAIN.MODEL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Stripe;
using System.Globalization;
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
            builder.Services.AddScoped<EmailSettings>();
            builder.Services.AddScoped<ConfirmEmailUseCase>();
            builder.Services.AddScoped<sendEmail>();
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
            builder.Services.AddScoped<IPublisherInterface, PublisherRepository>();
            builder.Services.AddScoped<TransactionPublisherDTOs>();
            builder.Services.AddScoped<GetPublisherDTOs>();
            builder.Services.AddScoped<DeletePublisherUseCase>();
            builder.Services.AddScoped<GetPublisherUseCase>();
            builder.Services.AddScoped<AddPublisherUseCase>();
            builder.Services.AddScoped<PublisherMap>();
            builder.Services.AddScoped<StripeService>();
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
            builder.Services.AddScoped<IBookPublisherInterface, BookPublisherRepository>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("allowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            // Cloudinary Settings
            builder.Services.AddScoped<UploadFile>();

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
                  ValidateLifetime = false,
                  IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["jwt:secretkey"])
                  ),

              };
          });

            builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
            StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
            const string defaultCulture = "en";

            var supportedCultures = new[]
            {
                new CultureInfo(defaultCulture),
                new CultureInfo("ar")
            };

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(defaultCulture);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

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
            app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
            app.UseHttpsRedirection();
            app.UseCors("allowAll");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();


            app.Run();
        }

    }
}
