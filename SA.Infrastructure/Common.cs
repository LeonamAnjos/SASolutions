using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace SA.Infrastructure
{
    public static class Common
    {
        public static string GetColumnValidationMessage(string columnName, ValidationResults validationResults)
        {
            if (validationResults == null)
                return null;

            if (String.IsNullOrEmpty(columnName))
                return null;

            IEnumerable<ValidationResult> columnValidationResults = validationResults.Where(vr => vr.Key.Equals(columnName));
            if (columnValidationResults.Count() == 0)
                return null;

            string validationMessage = String.Empty;
            string newLine = String.Empty;

            foreach (var r in columnValidationResults)
            {
                validationMessage += newLine + r.Message;
                newLine = "\n";
            }
            return validationMessage;
        }

    }
}
