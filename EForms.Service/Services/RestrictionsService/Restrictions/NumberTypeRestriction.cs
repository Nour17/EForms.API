using EForms.API.Models;
using EForms.API.Services.Restrictions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EForms.API.Services.RestrictionsServcie.Restrictions
{
    public class NumberTypeRestriction : RestrictionAbstract
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
