using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Simbirsoft_Weather.Models.Validation
{
    public class EmailValidatorAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {

            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            if (value != null)
            {
                if (Regex.IsMatch(Convert.ToString(value).Trim(), pattern, RegexOptions.IgnoreCase))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
