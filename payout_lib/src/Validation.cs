using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Payout.Lib
{
    public class Validation
    {
        public void ModelValidation()
        {
            ICollection<ValidationResult> results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(this, new ValidationContext(this), results, true))
                throw new Exception(string.Join("\n", results.Select(o => o.ErrorMessage)));
        }
    }
}
