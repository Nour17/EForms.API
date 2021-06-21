using EForms.API.Core.Services.ValidationsService.Abstractions;
using EForms.API.Infrastructure.Models;
using System;

namespace EForms.API.Core.Services.ValidationsService.Restrictions
{
    public class NumberTypeIsIntegerRestriction : ValidateSingleInput
    {
        public override bool checkRestriction(string userAnswer)
        {
            int valueHolder;
            return int.TryParse(userAnswer, out valueHolder);
        }
    }
}
