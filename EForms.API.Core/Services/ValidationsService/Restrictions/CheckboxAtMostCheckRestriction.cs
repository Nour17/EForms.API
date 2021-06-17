﻿using EForms.API.Core.Services.ValidationsService.Abstractions;
using System;


namespace EForms.API.Core.Services.ValidationsService.Restrictions
{
    public class CheckboxAtMostCheckRestriction : ValidateDoubleInput
    {
        public override bool checkRestriction(string userAnswer, string rightOperand)
        {
            int rightOperandToInt = StringToIntConverstion(rightOperand);
            int numberOfChosenAnswers = StringToIntConverstion(userAnswer);

            // Check if rightOperandToInt and user's answer is not 0 as it is an indicator that the conversion is done successfuly
            if (rightOperandToInt != 0 && numberOfChosenAnswers != 0)
                // Check if the restriction is fullfilled 
                if (numberOfChosenAnswers <= rightOperandToInt)
                    return true;

            return false;
        }
    }
}
