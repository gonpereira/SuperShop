using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SuperShop.Data;
using SuperShop.Data.Entities;
using SuperShop.Helpers;
using System.Text;
using Vereyon.Web;

namespace SuperShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Web Authen.
            services.AddIdentity<User, IdentityRole>(configure =>
            {
                configure.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
                configure.SignIn.RequireConfirmedEmail = true;
                configure.User.RequireUniqueEmail = true;
                configure.Password.RequireDigit = false;
                configure.Password.RequireLowercase = false;
                configure.Password.RequireUppercase = false;
                configure.Password.RequiredUniqueChars = 0;
                configure.Password.RequireNonAlphanumeric = false;
                configure.Password.RequiredLength = 6;


            })
              .AddDefaultTokenProviders()
              .AddEntityFrameworkStores<DataContext>(); //é onde ele separa o datacontext do Identity do nosso

            //API Authen.
            services.AddAuthentication()
                .AddCookie()
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["Tokens:Issuer"],
                        ValidAudience = Configuration["Tokens:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                    };
                });
       
            services.AddDbContext<DataContext>(configure =>
            {   //é aqui que eu digo qual o SQL que vou usar. Neste caso é o SQLServer
                configure.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddFlashMessage(); //Vereyon

            services.AddTransient<SeedDb>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IUserHelper, UserHelper>();
            services.AddScoped<IBlobHelper, BlobHelper>();
            services.AddScoped<IConverterHelper, ConverterHelper>();
            services.AddScoped<IMailHelper, MailHelper>();

            
            //Apos criar o IGenericRepository
            /*services.AddScoped<IRepository, Repository>();*/ //isto fica smp aqui pq n sabemos quando vamos carregar os produtos

            //passa a ser isto
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IOrderRepository, OrderRepository>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/NotAuthorized";
                options.AccessDeniedPath = "/Account/NotAuthorized";
            });

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Errors/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/error/{0}"); //Agora temos de configurar no primeiro controlador para ser a partir daí que assume outras páginas
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();//
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
