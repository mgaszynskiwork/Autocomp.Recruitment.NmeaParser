using System;
using System.Collections.Generic;
using System.Globalization;

namespace Autocomp.Nmea.Common
{
    /// <summary>
    /// GLL message
    /// </summary>
    public class NmeaGLL : NmeaHeader
    {
        public double Latitude { get; private set; }
        public string LatitudeDirection { get; private set; }
        public double Longitude { get; private set; }
        public string LongitudeDirection { get; private set; }
        public DateTime UTC { get; private set; }
        public string Status { get; private set; }
        public string ModeIndicator { get; private set; }

        public NmeaGLL(string messege)
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

            SetLatitude(nmeaMessage.Fields[0]);
            SetLatitudeDirection(nmeaMessage.Fields[1]);
            SetLongitude(nmeaMessage.Fields[2]);
            SetLongitudeDirection(nmeaMessage.Fields[3]);
            SetUTC(nmeaMessage.Fields[4]);
            SetStatus(nmeaMessage.Fields[5]);
            SetModeIndicator(nmeaMessage.Fields[6]);
        }

        public new static bool CheckHeader(String header)
        {
            return header.Contains("GLL");
        }

        public new static string[] Headers = new string[] { "Latitude", "Latitude Direction", "Longitude", "Longitude Direction", "UTC", "Status", "Mode Indicator" };

        public void SetLatitude(string field)
        {
            if (Double.TryParse(field.Replace(".", ","), out double d))
            {
                Latitude = d;
            }
            else
            {
                SetErrorMessage(field + " - Latitude is not a number. ");
            }
        }

        public void SetLatitudeDirection(string field)
        {
            if ("NS".Contains(field) && field.Length == 1)
            {
                LatitudeDirection = field;
            }
            else
            {
                SetErrorMessage(field + " - Wrong Latitude direction. ");
            }
        }

        public void SetLongitude(string field)
        {
            if (Double.TryParse(field.Replace(".", ","), out double d))
            {
                Longitude = d;
            }
            else
            {
                SetErrorMessage(field + " - Longitude is not a number. ");
            }
        }

        public void SetLongitudeDirection(string field)
        {
            if ("EW".Contains(field) && field.Length == 1)
            {
                LongitudeDirection = field;
            }
            else
            {
                SetErrorMessage(field + " - Wrong Longitude direction. ");
            }
        }

        public void SetUTC(string field)
        {
            if (DateTime.TryParseExact(field, "HHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime d))
            {
                UTC = d;
            }
            else
            {
                SetErrorMessage(field + " - Longitude is not a number. ");
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

        public void SetModeIndicator(string field)
        {
            if ("ADEMSN".Contains(field) && field.Length == 1)
            {
                switch (field)
                {
                    case "A":
                        ModeIndicator = "A - Autonomous mode";
                        break;

                    case "D":
                        ModeIndicator = "D - Differential mode";
                        break;

                    case "E":
                        ModeIndicator = "E - Estimated (dead reckoning) mode";
                        break;

                    case "M":
                        ModeIndicator = "M - Manual input mode";
                        break;

                    case "S":
                        ModeIndicator = "S - Simulator mode";
                        break;

                    case "N":
                        ModeIndicator = "N - Data not valid";
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
                Latitude.ToString(),
                LatitudeDirection,
                Longitude.ToString(),
                LongitudeDirection,
                UTC.ToString("HH:mm:ss"),
                Status,
                ModeIndicator,
            };
        }
    }
}