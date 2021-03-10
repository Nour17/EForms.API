using EForms.API.Services.Restrictions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EForms.API.Services.RestrictionsServcie.Restrictions
{
    public class StringContainsRestriction : RestrictionAbstract
    {
        public override bool checkRestriction(string userAnswer, string rightOperand)
        {
            if (userAnswer.Contains(rightOperand))
                return true;
            return false;
        }
    }
}
