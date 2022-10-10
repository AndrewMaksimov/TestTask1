using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestTask1.Infrastructure.Exceptions
{
    public class InvalidNameException : Exception
    {
        private readonly IList<ValidationResult> _validationResults;
        public InvalidNameException() { }

        public InvalidNameException(IList<ValidationResult> validationResults) 
        {
            _validationResults = validationResults;
        }
        public override string Message
        {
            get
            {
                var validationErrorMsg = string.Empty;
                foreach(var error in _validationResults)
                {
                    validationErrorMsg = $"{validationErrorMsg}{Environment.NewLine}{error}";
                }
                return validationErrorMsg;
            }
        }
    }
}
