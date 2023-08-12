using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Common.Behaviors
{
    //public class ValidationBehaviour<TRequest, TResult> : IPipelineBehavior<TRequest, TResult> where TRequest : class
    //{
    //    private readonly IEnumerable<IValidator<TRequest>> _validators;

    //    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    //    {
    //        _validators = validators;
    //    }

    //    public async Task<TResult> Handle(TRequest request, RequestHandlerDelegate<TResult> next, CancellationToken cancellationToken)
    //    {
    //        var ctx = new ValidationContext<TRequest>(request);
    //        var validationResult = _validators
    //        .Select(v => v.Validate(ctx))
    //        .SelectMany(v => v.Errors)
    //        .Where(x => x is not null)
    //        .ToList();
    //        if (validationResult.Any())
    //            throw new ValidationException(validationResult);
    //        return await next();
    //    }
    //}
}
