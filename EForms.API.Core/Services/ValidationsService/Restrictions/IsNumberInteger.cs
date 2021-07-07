using EForms.API.Core.Models;
using EForms.API.Core.Services.ValidationService;

namespace EForms.API.Core.Services.ValidationsService.Restrictions
{
    public class IsNumberInteger : IRestriction
    {
        public bool checkRestriction(string userAnswer, RestrictionCore restriction)
        {
            int valueHolder;
            return int.TryParse(userAnswer, out valueHolder);
        }
    }
}
