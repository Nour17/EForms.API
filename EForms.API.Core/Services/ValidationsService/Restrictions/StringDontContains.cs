using EForms.API.Core.Models;
using EForms.API.Core.Services.ValidationService;

namespace EForms.API.Core.Services.ValidationsService.Restrictions
{
    public class StringDontContains : IRestriction
    {
        public bool checkRestriction(string userAnswer, RestrictionCore restriction)
        {
            if (!userAnswer.Contains(restriction.RightOperand))
                return true;
            return false;
        }
    }
}
