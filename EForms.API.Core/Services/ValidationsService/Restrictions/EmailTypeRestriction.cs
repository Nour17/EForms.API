using EForms.API.Core.Services.ValidationsService.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EForms.API.Core.Services.ValidationsService.Restrictions
{
    class EmailTypeRestriction : ValidateSingleInput
    {
        public override bool checkRestriction(string userAnswer)
        {
            throw new NotImplementedException();
        }
    }
}
