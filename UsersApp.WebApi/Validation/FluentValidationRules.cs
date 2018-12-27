using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Validators;
using MicroElements.Swashbuckle.FluentValidation;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace UsersApp.WebApi.Validation
{
    public class FluentValidationRules : ISchemaFilter
    {
        private readonly IValidatorFactory _validatorFactory;

        private readonly ILogger _logger;

        private readonly IReadOnlyList<FluentValidationRule> _rules;

        public FluentValidationRules(
            IValidatorFactory validatorFactory = null,
            IEnumerable<FluentValidationRule> rules = null,
            ILoggerFactory loggerFactory = null)
        {
            _validatorFactory = validatorFactory;
            _rules = CreateDefaultRules();
            _logger = loggerFactory?.CreateLogger(typeof(FluentValidationRules));
        }

        public static FluentValidationRule[] CreateDefaultRules()
        {
            return new[]
            {
                new FluentValidationRule("Required")
                {
                    Matches = propertyValidator => propertyValidator is INotNullValidator
                                                || propertyValidator is INotEmptyValidator,
                    Apply = context =>
                    {
                        if (context.Schema.Required == null)
                        {
                            context.Schema.Required = new List<string>();
                        }

                        if (!context.Schema.Required.Contains(context.PropertyKey))
                        {
                            context.Schema.Required.Add(context.PropertyKey);
                        }
                    }
                },
                new FluentValidationRule("NotEmpty")
                {
                    Matches = propertyValidator => propertyValidator is INotEmptyValidator,
                    Apply = context => { context.Schema.Properties[context.PropertyKey].MinLength = 1; }
                },
                new FluentValidationRule("Length")
                {
                    Matches = propertyValidator => propertyValidator is ILengthValidator,
                    Apply = context =>
                    {
                        var lengthValidator = (ILengthValidator) context.PropertyValidator;
                        if (lengthValidator.Max > 0)
                        {
                            context.Schema.Properties[context.PropertyKey].MaxLength = lengthValidator.Max;
                        }
                        else
                        {
                            context.Schema.Properties[context.PropertyKey].MinLength = lengthValidator.Min;
                        }
                    }
                }
            };
        }

        public void Apply(Schema schema, SchemaFilterContext context)
        {
            IValidator validator = null;
            try
            {
                validator = _validatorFactory.GetValidator(context.SystemType);
            }
            catch (Exception e)
            {
                _logger?.LogWarning(0, e, $"GetValidator for type '{context.SystemType}' fails.");
            }

            if (validator == null)
            {
                return;
            }

            ApplyRulesToSchema(schema, context, validator);
        }

        private void ApplyRulesToSchema(Schema schema, SchemaFilterContext context, IValidator validator)
        {
            var validatorDescriptor = validator.CreateDescriptor();

            foreach (var key in schema?.Properties?.Keys ?? Array.Empty<string>())
            {
                foreach (var propertyValidator in validatorDescriptor.GetValidatorsForMemberIgnoreCase(key).NotNull())
                {
                    foreach (var rule in _rules)
                    {
                        if (!rule.Matches(propertyValidator)) continue;

                        try
                        {
                            rule.Apply(new RuleContext(schema, context, key, propertyValidator));
                        }
                        catch (Exception e)
                        {
                            _logger?.LogWarning(0, e, $"Error on apply rule '{rule.Name}' for key '{key}'.");
                        }
                    }
                }
            }
        }
    }
}
