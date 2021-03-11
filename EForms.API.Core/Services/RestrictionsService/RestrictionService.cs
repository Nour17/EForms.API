
namespace EForms.API.Core.Services.RestrictionsService
{
    public abstract class RestrictionService
    {
        public abstract bool checkRestriction(string userAnswer, string rightOperand);
        public static bool StringToIntConverstion(string operand, out int operandToInt)
        {
            return int.TryParse(operand, out operandToInt);
        }
    }
}
