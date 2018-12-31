using FunctionMonkey.Abstractions;
using FunctionMonkey.Abstractions.Builders;
using FunctionMonkey.FluentValidation;
using Microsoft.Extensions.Configuration;

namespace UsersApp.WebApi.AzureFunctions
{
    public class AzureFunctionsConfigurator : IFunctionAppConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureFunctionsConfigurator"/> class.
        /// </summary>
        public AzureFunctionsConfigurator()
        {
        }

        /// <inheritdoc/>
        public void Build(IFunctionHostBuilder builder)
        {
            builder.Setup((serviceCollection, commandRegistry) =>
            {
                IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                    .AddEnvironmentVariables();
            }).AddFluentValidation()
            .Functions(functions =>
            {

            });
        }
    }
}
