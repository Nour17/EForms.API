using EForms.API.Core.Services.ValidationsService.Abstractions;
using EForms.API.Infrastructure.Models;
using System;

namespace EForms.API.Core.Services.ValidationsService.Restrictions
{
    public class NumberTypeRestriction : ValidateDoubleInput
    {
        public override bool checkRestriction(string userAnswer, string rightOperand)
        {
            NumberType numType = (NumberType)int.Parse(rightOperand);

            if (numType == NumberType.Integer)
            {
                int valueHolder;
                return int.TryParse(userAnswer, out valueHolder);
            }
            else if (numType == NumberType.Double)
            {
                double valueHolder;
                return double.TryParse(userAnswer, out valueHolder);
            }

            return false;
        }
    }
}
