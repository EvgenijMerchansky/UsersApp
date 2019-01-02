using FunctionMonkey.Abstractions;
using FunctionMonkey.Abstractions.Builders;
using FunctionMonkey.FluentValidation;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace UsersApp.WebApi.AzureFunctions
{
    public class UsersAppConfigurator : IFunctionAppConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureFunctionsConfigurator"/> class.
        /// </summary>
        public UsersAppConfigurator()
        {
        }

        /// <inheritdoc/>
        public void Build(IFunctionHostBuilder builder)
        {
            builder.Setup((serviceCollection, commandRegistry) =>
            {
                IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                    .AddEnvironmentVariables();

                if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("IsLocal")))
                {
                    configurationBuilder = configurationBuilder
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("local.settings.json");
                }

                IConfiguration config = configurationBuilder.Build();

                commandRegistry.Discover<UsersAppConfigurator>();

            }).AddFluentValidation()
            .Functions(functions =>
            {

            });
        }
    }
}
