﻿using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Internal;
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

        /// <summary>
        /// Creates new instance of <see cref="FluentValidationRules"/>
        /// </summary>
        /// <param name="validatorFactory">Validator factory.</param>
        /// <param name="rules">External FluentValidation rules. Rule with the same name replaces default rule.</param>
        /// <param name="loggerFactory"><see cref="ILoggerFactory"/> for logging. Can be null.</param>
        public FluentValidationRules(
           IValidatorFactory validatorFactory = null,
            IEnumerable<FluentValidationRule> rules = null,
            ILoggerFactory loggerFactory = null)
        {
            _validatorFactory = validatorFactory;
            _logger = loggerFactory?.CreateLogger(typeof(FluentValidationRules));
            _rules = CreateDefaultRules();
            if (rules != null)
            {
                var ruleMap = _rules.ToDictionary(rule => rule.Name, rule => rule);
                foreach (var rule in rules)
                {
                    // Add or replace rule
                    ruleMap[rule.Name] = rule;
                }

                _rules = ruleMap.Values.ToList();
            }
        }

        /// <summary>
        /// Creates default rules.
        /// Can be overriden by name.
        /// </summary>
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
                    Apply = context =>
                    {
                        context.Schema.Properties[context.PropertyKey].MinLength = 1;
                    }
                },
                new FluentValidationRule("Length")
                {
                    Matches = propertyValidator => propertyValidator is ILengthValidator,
                    Apply = context =>
                    {
                        var lengthValidator = (ILengthValidator)context.PropertyValidator;
                        if (lengthValidator.Max > 0)
                        {
                            context.Schema.Properties[context.PropertyKey].MaxLength = lengthValidator.Max;
                        }
                        else
                        {
                            context.Schema.Properties[context.PropertyKey].MinLength = lengthValidator.Min;
                        }
                    }
                },
                new FluentValidationRule("Pattern")
                {
                    Matches = propertyValidator => propertyValidator is IRegularExpressionValidator,
                    Apply = context =>
                    {
                        var regularExpressionValidator = (IRegularExpressionValidator)context.PropertyValidator;
                        context.Schema.Properties[context.PropertyKey].Pattern = regularExpressionValidator.Expression;
                    }
                },
                new FluentValidationRule("Comparison")
                {
                    Matches = propertyValidator => propertyValidator is IComparisonValidator,
                    Apply = context =>
                    {
                        var comparisonValidator = (IComparisonValidator)context.PropertyValidator;
                        if (comparisonValidator.ValueToCompare.IsNumeric())
                        {
                            var valueToCompare = Convert.ToDouble(comparisonValidator.ValueToCompare);
                            var schemaProperty = context.Schema.Properties[context.PropertyKey];

                            if (comparisonValidator.Comparison == Comparison.GreaterThanOrEqual)
                            {
                                schemaProperty.Minimum = valueToCompare;
                            }
                            else if (comparisonValidator.Comparison == Comparison.GreaterThan)
                            {
                                schemaProperty.Minimum = valueToCompare;
                                schemaProperty.ExclusiveMinimum = true;
                            }
                            else if (comparisonValidator.Comparison == Comparison.LessThanOrEqual)
                            {
                                schemaProperty.Maximum = valueToCompare;
                            }
                            else if (comparisonValidator.Comparison == Comparison.LessThan)
                            {
                                schemaProperty.Maximum = valueToCompare;
                                schemaProperty.ExclusiveMaximum = true;
                            }
                        }
                    }
                },
                new FluentValidationRule("Between")
                {
                    Matches = propertyValidator => propertyValidator is IBetweenValidator,
                    Apply = context =>
                    {
                        var betweenValidator = (IBetweenValidator)context.PropertyValidator;
                        var schemaProperty = context.Schema.Properties[context.PropertyKey];

                        if (betweenValidator.From.IsNumeric())
                        {
                            schemaProperty.Minimum = Convert.ToDouble(betweenValidator.From);

                            if (betweenValidator is ExclusiveBetweenValidator)
                            {
                                schemaProperty.ExclusiveMinimum = true;
                            }
                        }

                        if (betweenValidator.To.IsNumeric())
                        {
                            schemaProperty.Maximum = Convert.ToDouble(betweenValidator.To);

                            if (betweenValidator is ExclusiveBetweenValidator)
                            {
                                schemaProperty.ExclusiveMaximum = true;
                            }
                        }
                    }
                }
            };
        }

        /// <inheritdoc />
        public void Apply(Schema schema, SchemaFilterContext context)
        {
            if (_validatorFactory == null)
            {
                _logger?.LogWarning(0, "ValidatorFactory is not provided. Please register FluentValidation.");
                return;
            }

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

            try
            {
                // Note: IValidatorDescriptor doesn't return IncludeRules so we need to get validators manually.
                var includeRules = (validator as IEnumerable<IValidationRule>).NotNull().OfType<IncludeRule>();
                var childAdapters = includeRules.SelectMany(includeRule => includeRule.Validators).OfType<ChildValidatorAdaptor>();
                foreach (var adapter in childAdapters)
                {
                    var propertyValidatorContext = new PropertyValidatorContext(new ValidationContext(null), null, string.Empty);
                    var includeValidator = adapter.GetValidator(propertyValidatorContext);
                    ApplyRulesToSchema(schema, context, includeValidator);
                }
            }
            catch (Exception e)
            {
                _logger?.LogWarning(0, e, $"Applying IncludeRules for type '{context.SystemType}' fails.");
            }
        }

        private void ApplyRulesToSchema(Schema schema, SchemaFilterContext context, IValidator validator)
        {
            IValidatorDescriptor validatorDescriptor = validator.CreateDescriptor();

            foreach (var key in schema?.Properties?.Keys ?? Array.Empty<string>())
            {
                foreach (var propertyValidator in validatorDescriptor.GetValidatorsForMemberIgnoreCase(key).NotNull())
                {
                    foreach (var rule in _rules)
                    {
                        if (rule.Matches(propertyValidator))
                        {
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
}
