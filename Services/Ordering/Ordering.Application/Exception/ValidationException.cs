using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions.Impl;
using FluentValidation.Results;

namespace Ordering.Application.Exception
{
    public class ValidationException : ApplicationException
    {
        public IDictionary<string, String[]> Errors { get; set; }

        public ValidationException() : base("One or more validation errors(s) occured.")
        {
            Errors = new Dictionary<string, string[]>();

        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failure => failure.Key, failure => failure.ToArray());
        }

    }
}
