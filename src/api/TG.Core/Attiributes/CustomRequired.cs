using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TG.Core.Attiributes
{
    public class CustomRequired : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            ErrorMessage = "Lütfen bu alanı doldurunuz.";

            if (value == null || string.IsNullOrWhiteSpace(value.ToString()) ||
                value != null && value.ToString() == "1.01.0001 00:00:00")
                return false;

            return true;
        }
    }
}
