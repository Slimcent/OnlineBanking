using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.domain.Models
{
    public class CustomBirthday : Attribute, IModelValidator
    {
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            if (Convert.ToDateTime(context.Model) > DateTime.Now)
                return new List<ModelValidationResult>
                {
                    new ModelValidationResult("", "You Cannot be in the future")
                };
            else if (Convert.ToDateTime(context.Model) < new DateTime(1900, 1, 1))
                return new List<ModelValidationResult>
                {
                    new ModelValidationResult("", "Are you that old?")
                };
            else
                return Enumerable.Empty<ModelValidationResult>();
        }
    }
}
