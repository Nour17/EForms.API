using EForms.API.Infrastructure.Models;
using System;

namespace EForms.API.Core.Services.RestrictionsService.Restrictions
{
    public class NumberTypeRestriction : RestrictionService
    {
        public override bool checkRestriction(string userAnswer, string rightOperand)
        {
            NumberType numType = (NumberType)int.Parse(rightOperand);

            if (numType == NumberType.Integer)
            {
                var valueHolder = 0;
                return int.TryParse(userAnswer, out valueHolder);
            }
            else if (numType == NumberType.Double)
            {
                var valueHolder = 0.0;
                return double.TryParse(userAnswer, out valueHolder);
            }

            return false;
        }
    }
}
