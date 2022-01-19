using System;
using System.Collections.Generic;
using System.Globalization;

namespace Autocomp.Nmea.Common
{
    /// <summary>
    /// GLL message
    /// </summary>
    public class NmeaMWV : NmeaHeader
    {
        public double WindAngle { get; private set; }
        public string Reference { get; private set; }
        public double WindSpeed { get; private set; }
        public string WindSpeedUnits { get; private set; }
        public string Status { get; private set; }

        public NmeaMWV(string messege)
        {
            OriginalMessage = messege;
            NmeaMessage nmeaMessage;
            try
            {
                nmeaMessage = NmeaMessage.FromString(messege);
            }
            catch (Exception ex)
            {
                SetErrorMessage("Parse error: " + ex);
                throw;
            }

            SetWindAngle(nmeaMessage.Fields[0]);
            SetReference(nmeaMessage.Fields[1]);
            SetWindSpeed(nmeaMessage.Fields[2]);
            SetWindSpeedUnits(nmeaMessage.Fields[3]);
            SetStatus(nmeaMessage.Fields[4]);
        }

        public new static bool CheckHeader(String header)
        {
            return header.Contains("MWV");
        }

        public new static string[] Headers = new string[] { "Wind Angle", "Reference", "Wind Speed", "Wind Speed Units", "Status", };

        public void SetWindAngle(string field)
        {
            if (Double.TryParse(field.Replace(".", ","), out double d))
            {
                if (d < 0)
                {
                    SetErrorMessage(field + " - Wind angle has to be positive number. ");
                }
                else if (d > 359)
                {
                    SetErrorMessage(field + " - The wind angle must be less than or equal to 359. ");
                }
                else
                {
                    WindAngle = d;
                }
            }
            else
            {
                SetErrorMessage(field + " - Wind angle is not a number. ");
            }
        }

        public void SetReference(string field)
        {
            if ("RT".Contains(field) && field.Length == 1)
            {
                switch (field)
                {
                    case "R":
                        Reference = "R - Relative";
                        break;

                    case "T":
                        Reference = "T - Theoretical";
                        break;
                }
            }
            else
            {
                SetErrorMessage(field + " - Wrong Reference. ");
            }
        }

        public void SetWindSpeed(string field)
        {
            if (Double.TryParse(field.Replace(".", ","), out double d))
            {
                WindAngle = d;
            }
            else
            {
                SetErrorMessage(field + " - Wind Speed is not a number. ");
            }
        }

        public void SetWindSpeedUnits(string field)
        {
            if ("KMNS".Contains(field) && field.Length == 1)
            {
                WindSpeedUnits = field;
            }
            else
            {
                SetErrorMessage(field + " - Wrong Wind Speed Units. ");
            }
        }

        public void SetStatus(string field)
        {
            if ("AV".Contains(field) && field.Length == 1)
            {
                switch (field)
                {
                    case "A":
                        Status = "A - Data valid";
                        break;

                    case "V":
                        Status = "V - Data not valid";
                        break;
                }
            }
            else
            {
                SetErrorMessage(field + " - Wrong Status. ");
            }
        }

        public override List<string> GetAllFields()
        {
            return new List<string>()
            {
                WindAngle.ToString(),
                Reference,
                WindSpeed.ToString(),
                WindSpeedUnits,
                Status,
            };
        }
    }
}