using monitorr.logger.Infrastructure.Configuration;
using monitorr.logger.Infrastructure.Filters;
using monitorr.logger.Infrastructure.Interfaces;
using monitorr.logger.Infrastructure.Repositories;
using monitorr.logger.Infrastructure.Repositories.Interfaces;
using monitorr.logger.Infrastructure.Services;
using monitorr.logger.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace monitorr.logger
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

        }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddApplicationInsightsTelemetry(Configuration);
            services.Configure<Settings>(Configuration);
            services.AddScoped<IErrorRepository, ErrorRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IViewRenderService, ViewRenderService>();
            services.AddScoped<IEmailSender, AmazonEmailService>();

            services.AddDistributedMemoryCache();

            services.AddMvc(o =>
            {
                o.Filters.Add(typeof(ApiExceptionFilter));
            });

            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            if (env.IsProduction())
            {
                loggerFactory.AddFile("Logs/production-log-{Date}.txt");
            }
            else if (env.IsStaging())
            {
                loggerFactory.AddFile("Logs/staging-log-{Date}.txt");
            }

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseMvc();
        }
    }
}
