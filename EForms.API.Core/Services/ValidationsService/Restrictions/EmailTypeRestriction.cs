using EForms.API.Core.Services.ValidationsService.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EForms.API.Core.Services.ValidationsService.Restrictions
{
    class EmailTypeRestriction : ValidateSingleInput
    {
        public override bool checkRestriction(string userAnswer)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(userAnswer);
            if (match.Success)
                return true;
            return false;
        }
    }
}
