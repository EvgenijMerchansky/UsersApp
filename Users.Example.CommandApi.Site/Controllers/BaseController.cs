using System.Runtime.CompilerServices;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Users.Example.CommandApi.Site.Controllers;

public class BaseController(ILogger logger, IServiceProvider serviceProvider) : ControllerBase
{
    public List<string> ValidateSingle<T>(T entity, [CallerMemberName] string methodName = "")
    {
        var validator = GetValidator<T>();

        List<string> errors = [];
        if (validator == null) return errors;

        ProcessEntityValidation(entity, validator, errors, methodName);
        return errors;
    }

    public List<string> ValidateList<T>(List<T> entityList, [CallerMemberName] string methodName = "")
    {
        var validator = GetValidator<T>();

        List<string> errors = [];
        if (validator == null) return errors;

        foreach (T entity in entityList)
        {
            ProcessEntityValidation(entity, validator, errors, methodName);
        }
        return errors;
    }

    private IValidator<T>? GetValidator<T>()
    {
        var validatorType = typeof(IValidator<>).MakeGenericType(typeof(T));
        var validator = serviceProvider.GetService(validatorType) as IValidator<T>;
        if (validator == null)
        {
            logger.LogWarning("No validator is registered for type: {type}", typeof(T).Name);
        }
        return validator;
    }

    private void ProcessEntityValidation<T>(T entity, IValidator<T> validator, List<string> errorTargetList, string methodName)
    {
        var validationResult = validator.Validate(entity);
        if (validationResult.IsValid)
        {
            return;
        }

        var errors = validationResult.Errors.Select(e => e.ErrorMessage).Distinct().ToList();
        var fullErrorString = string.Join(',', errors);
        logger.LogWarning("Method {caller}; Errors: {errors}; Entity: {@entity}", methodName, fullErrorString, entity);

        errorTargetList.AddRange(errors);
    }
}