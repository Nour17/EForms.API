using EForms.API.Services.Restrictions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EForms.API.Services.RestrictionsService.Restrictions
{
    public class DateAfterRestriction : RestrictionAbstract
    {
        public override bool checkRestriction(string userAnswer, string rightOperand)
        {
            throw new NotImplementedException();
        }
    }
}
