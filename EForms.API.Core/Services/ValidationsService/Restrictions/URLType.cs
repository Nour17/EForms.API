using EForms.API.Core.Models;
using EForms.API.Core.Services.ValidationService;
using System.Text.RegularExpressions;

namespace EForms.API.Core.Services.ValidationsService.Restrictions
{
    class URLType : IRestriction
    {
        public bool checkRestriction(string userAnswer, RestrictionCore restriction)
        {
            Regex regex = new Regex(@"/https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)/");
            Match match = regex.Match(userAnswer);
            if (match.Success)
                return true;
            return false;
        }
    }
}
