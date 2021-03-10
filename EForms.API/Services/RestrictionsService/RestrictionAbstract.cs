using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EForms.API.Services.Restrictions
{
    public abstract class RestrictionAbstract
    {
        public abstract bool checkRestriction(string userAnswer, string rightOperand);
    }
}
