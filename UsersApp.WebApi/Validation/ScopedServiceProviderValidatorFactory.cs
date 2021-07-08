using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;

namespace UsersApp.WebApi.Validation
{
    public class ScopedServiceProviderValidatorFactory : ValidatorFactoryBase
    {
        private readonly IServiceProvider _serviceProvider;

        public ScopedServiceProviderValidatorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        [DebuggerStepThrough]
        public override IValidator CreateInstance(Type validatorType)
        {
            using (_serviceProvider.CreateScope())
            {
                return _serviceProvider.CreateScope()
                    .ServiceProvider.GetService(validatorType) as IValidator;
            }
        }
    }
}
