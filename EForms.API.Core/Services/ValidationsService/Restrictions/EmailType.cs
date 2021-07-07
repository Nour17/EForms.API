using EForms.API.Core.Models;
using EForms.API.Core.Services.ValidationService;
using System.Text.RegularExpressions;

namespace EForms.API.Core.Services.ValidationsService.Restrictions
{
    class EmailType : IRestriction
    {
        public bool checkRestriction(string userAnswer, RestrictionCore restriction)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(userAnswer);
            if (match.Success)
                return true;
            return false;
        }
    }
}
