using Payout.Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Payout.Lib.Validations
{
    public class RequestValidation : IRequestValidation
    {
        public void ModelValidation<T>(T requestModel) where T : class
        {
            ICollection<ValidationResult> results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(requestModel, new ValidationContext(requestModel), results, true))
                throw new Exception(string.Join("\n", results.Select(o => o.ErrorMessage)));
        }
    }
}
