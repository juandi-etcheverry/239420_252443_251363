﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public static class StringValidator
    {
        public static bool HasSpaces(string stringToValidate)
        {
            return stringToValidate.Contains(" ");
        }

        public static bool HasTrailingSpaces(string stringToValidate)
        {
            return stringToValidate.StartsWith(" ") || stringToValidate.EndsWith(" ");
        }
    }
}
