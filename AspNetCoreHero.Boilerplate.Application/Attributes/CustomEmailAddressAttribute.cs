using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreHero.Boilerplate.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
       AllowMultiple = false)]
    public class CustomEmailAddressAttribute : DataTypeAttribute
    {
       
        public CustomEmailAddressAttribute() :base(DataType.EmailAddress)
        {
        }
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return true;
            }

            if (!(value is string valueAsString))
            {
                return false;
            }

            // only return true if there is only 1 '@' character
            // and it is neither the first nor the last character
            int index = valueAsString.IndexOf('@');


            bool isValid = index > 0 &&
                index != valueAsString.Length - 1 &&
                index == valueAsString.LastIndexOf('@');

            /*if(isValid)
            {
                int index2 = valueAsString.IndexOf('.');


                isValid = index2 > 0 &&
                    index2 != valueAsString.Length - 1 &&
                    index2 == valueAsString.LastIndexOf('.');
            }*/
            return isValid;
        }

    }
}
