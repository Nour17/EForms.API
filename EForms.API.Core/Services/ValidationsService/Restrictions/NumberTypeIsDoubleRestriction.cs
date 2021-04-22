using EForms.API.Core.Services.ValidationsService.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Core.Services.ValidationsService.Restrictions
{
    class NumberTypeIsDoubleRestriction : ValidateSingleInput
    {
        public override bool checkRestriction(string userAnswer)
        {
            double valueHolder;
            return double.TryParse(userAnswer, out valueHolder);
        }
    }
}
