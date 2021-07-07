using EForms.API.Core.Models;
using EForms.API.Core.Services.ValidationService;

namespace EForms.API.Core.Services.ValidationsService.Restrictions
{
    class IsNumberDouble : IRestriction
    {
        public bool checkRestriction(string userAnswer, RestrictionCore restriction)
        {
            double valueHolder;
            return double.TryParse(userAnswer, out valueHolder);
        }
    }
}
