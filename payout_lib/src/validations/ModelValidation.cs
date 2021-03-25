using Payout.Lib.Base;
using Payout.Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Payout.Lib.Validations
{
    public class ModelValidation : IModelValidation
    {
        public void ValidateRequest(BaseRequest request)
        {
            ICollection<ValidationResult> results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(request, new ValidationContext(request), results, true))
                throw new Exception(string.Join("\n", results.Select(o => o.ErrorMessage)));
        }
    }
}
