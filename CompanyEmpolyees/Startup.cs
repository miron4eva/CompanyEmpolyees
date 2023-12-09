using Microsoft.AspNetCore.HttpOverrides;
using NLog;
using CompanyEmpolyees.Extensions;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using CompanyEmpolyees.ActionFilters;
using Entities.DataTransferObjects;
using Repository;

namespace CompanyEmployees;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),
            "/nlog.config"));
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.ConfigureCors();
        services.ConfigureIISIntegration();
        services.ConfigureLoggerService();
        services.ConfigureSqlContext(Configuration);
        services.ConfigureRepositoryManager();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddAutoMapper(typeof(Startup));
        services.AddControllers(config => 
        {
            config.RespectBrowserAcceptHeader = true;
            config.ReturnHttpNotAcceptable = true;
        }).AddNewtonsoftJson()
            .AddXmlDataContractSerializerFormatters()
            .AddCustomCSVFormatter();
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        services.AddScoped<ValidationFilterAttribute>();
        services.AddScoped<ValidateCompanyExistsAttribute>();
        services.AddScoped<ValidateEmployeeForCompanyExistsAttribute>();
        services.AddScoped<ValidateLibraryExistsAttribute>();
        services.AddScoped<ValidateReaderForLibraryExistAttribute>();
        services.AddScoped<IDataShaper<EmployeeDto>, DataShaper<EmployeeDto>>();
        services.AddScoped<IDataShaper<ReaderDto>, DataShaper<ReaderDto>>();
        services.AddScoped<IAuthenticationManager, AuthenticationManager>();
        services.ConfigureVersioning();
        services.AddAuthentication();
        services.ConfigureIdentity();
        services.ConfigureJWT(Configuration);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.ConfigureExceptionHandler(logger);
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseCors("CorsPolicy");
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.All
        });

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}