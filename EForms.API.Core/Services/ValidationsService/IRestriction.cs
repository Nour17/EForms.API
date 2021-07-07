using EForms.API.Core.Models;

namespace EForms.API.Core.Services.ValidationService
{
    public interface IRestriction
    {
        bool checkRestriction(string userAnswer, RestrictionCore restriction);
    }
}
