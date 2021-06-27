using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Simbirsoft_Weather.Models.Validation
{
    public class NameAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {

            string patternValid = "^(?![0-9]*$)[^@_!#$%^&*()<>?/\\|}{~:][a-zA-Z0-9]+$";
            if (value != null)
            {
                if (Regex.IsMatch(Convert.ToString(value),patternValid, RegexOptions.IgnoreCase))
                {
                    return true;
                }
            }
           
            return false;
        }
    }
}
