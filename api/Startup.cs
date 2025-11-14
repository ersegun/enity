namespace api
{
    using api.Configuration;
    using api.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Enity Code Test API v1");
                c.RoutePrefix = string.Empty; // Serve Swagger UI at root
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            // Register Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Enity Code Test API",
                    Version = "v1",
                    Description = "API for account balance conversion and transaction analysis"
                });
            });
            
            // Register configuration options
            services.Configure<ServiceUrlsOptions>(Configuration.GetSection(ServiceUrlsOptions.SectionName));
            
            // Register HttpClient for external API calls
            services.AddHttpClient<IAccountService, AccountService>();
            services.AddHttpClient<IExchangeRateService, ExchangeRateService>();
            
            // Register services
            services.AddTransient<IConversionService, ConversionService>();
            services.AddTransient<ITransactionService, TransactionService>();
        }
    }
}