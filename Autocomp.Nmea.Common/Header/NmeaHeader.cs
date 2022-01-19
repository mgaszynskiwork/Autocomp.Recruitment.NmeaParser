using System;
using System.Collections.Generic;

namespace Autocomp.Nmea.Common
{
    /// <summary>
    /// GLL message
    /// </summary>
    public abstract class NmeaHeader
    {
        public string OriginalMessage { get; set; }
        public bool Error { get; set; } = false;
        public string ErrorMessage { get; set; } = null;

        public static bool CheckHeader(String header)
        { return true; }

        public static string[] Headers = new string[] { };

        public void SetErrorMessage(string errorMessage)
        {
            Error = true;
            ErrorMessage += errorMessage;
        }

        public virtual List<string> GetAllFields()
        { return null; }
    }
}