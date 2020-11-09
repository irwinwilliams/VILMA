// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.5.0

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using bot_VILMA_costinfo.Bots;
using VMManagement;
using Microsoft.Bot.Connector.Authentication;
using BestMatchMiddleware_Sample;
using Microsoft.Extensions.Logging;
using Bot.Builder.Community.Middleware.AzureAdAuthentication;

namespace bot_VILMA_costinfo
{
    public class Startup
    {
        private ILoggerFactory _loggerFactory;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Create the Bot Framework Adapter with error handling enabled.
            services.AddSingleton<IBotFrameworkHttpAdapter, AdapterWithErrorHandler>();

            IConfigurationSection sec = Configuration.GetSection("AzureCredentials");

            services.Configure<AzureCredentialInfo>(sec);

            // Create the bot as a transient. In this case the ASP Controller is expecting an IBot.
            //services.AddTransient<IBot, CostFinderBot>();
            var tokenStorage = new InMemoryAuthTokenStorage();
            services.AddSingleton<IAuthTokenStorage>(tokenStorage);

            services.AddBot<AzureAgentBot>(options =>
            {
                var appId = Configuration.GetSection("MicrosoftAppId")?.Value;
                var appPassword = Configuration.GetSection("MicrosoftAppPassword")?.Value;

                options.CredentialProvider = new SimpleCredentialProvider(appId, appPassword);

                options.Middleware.Add(new ShowTypingMiddleware());
                options.Middleware.Add(new CommonResponsesMiddleware());
                options.Middleware.Add(new AzureAdAuthMiddleware(tokenStorage, Configuration));

                ILogger logger = _loggerFactory.CreateLogger<AzureAgentBot>();
                
                options.OnTurnError = async (context, exception) =>
                {
                    logger.LogError($"Exception caught : {exception}");
                    await context.SendActivityAsync("Sorry, it looks like something went wrong.");
                };
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }



            app.UseDefaultFiles()
                .UseStaticFiles()
                .UseMvc()
                .UseBotFramework();

            //app.UseHttpsRedirection();
        }
    }
}
