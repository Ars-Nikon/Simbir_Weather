using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Simbirsoft_Weather.Models.Validation
{
    public class NameValidatorAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {

            string patternValid = @"\s|\d|\W";
            if (value != null)
            {
                if (Regex.IsMatch(Convert.ToString(value).Trim(), patternValid, RegexOptions.IgnoreCase))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
