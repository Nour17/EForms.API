using EForms.API.Core.Services.ValidationsService.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EForms.API.Core.Services.ValidationsService.Restrictions
{
    class URLTypeRestriction : ValidateSingleInput
    {
        public override bool checkRestriction(string userAnswer)
        {
            Regex regex = new Regex(@"/https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)/");
            Match match = regex.Match(userAnswer);
            if (match.Success)
                return true;
            return false;
        }
    }
}
