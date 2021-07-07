using EForms.API.Core.Services.ValidationsService.Restrictions;
using EForms.API.Core.Models;
using EForms.API.Infrastructure.Enums;
using EForms.API.Core.Services.ValidationService;
using System;

namespace EForms.API.Core.Services.RestrictionsServices.Factory
{
    public static class RestrictionsFactory
    {
        public static bool ApplyRestriction(RestrictionCore restriction, string userAnswer)
        {
            // Restriction class must have the same name as the Restriction Type
            // Create new instance based on the restriction type
            Type type = Type.GetType("EForms.API.Core.Services.ValidationsService.Restrictions." + restriction.Condition.ToString());
            var restrictionObject =  (IRestriction)Activator.CreateInstance(type);
            return restrictionObject.checkRestriction(userAnswer, restriction);
        }
    }
}
