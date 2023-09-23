using Kdega.ScormEngine.Application.Behavior;
using System.Globalization;
using System.Text;
using static System.Decimal;
using static System.Text.RegularExpressions.Regex;

namespace Kdega.ScormEngine.Application.Helpers;

public class ScormDataValidatorHelper
{
    internal const string TimeDelimiter = ":";

    /// <summary>
    /// CMIString (SPM: 255)
    /// </summary>
    /// <param name="dataValue"></param>
    /// <returns></returns>
    public static bool IsCmiString255(string dataValue) =>
        dataValue.Length < 255;
    /// <summary>
    /// CMIString (SPM: 4096)
    /// </summary>
    /// <param name="dataValue"></param>
    /// <returns></returns>
    public static bool IsCmiString4096(string dataValue) =>
        dataValue.Length < 4096;

    /// <summary>
    /// CMIString (SPM: 1000)
    /// </summary>
    /// <param name="dataValue"></param>
    /// <returns></returns>
    public static bool IsCmiString1000(string dataValue) =>
        dataValue.Length < 1000;
    /// <summary>
    /// CMIDecimal
    /// </summary>
    /// <param name="dataValue"></param>
    /// <returns></returns>
    public static bool IsCmiDecimal(string dataValue) =>
        TryParse(dataValue, out var result);
    /// <summary>
    /// Non-negative Decimal
    /// </summary>
    /// <param name="dataValue"></param>
    /// <returns></returns>
    public static bool IsCmiDecimalPositive(string dataValue)
    {
        TryParse(dataValue, out var result);
        return result >= 0;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataValue"></param>
    /// <returns></returns>
    public static bool IsInteger(string dataValue) =>
        double.TryParse(dataValue, NumberStyles.Integer, null, out _);
    /// <summary>
    /// CMIInteger
    /// </summary>
    /// <param name="dataValue"></param>
    /// <returns></returns>
    public static bool IsCmiInteger(string dataValue)
    {
        if (!double.TryParse(dataValue, NumberStyles.Integer, null, out var result)) return false;
        var number = Convert.ToInt32(result);
        return number is >= 0 and <= 65536;
    }
    /// <summary>
    /// CMISInteger
    /// </summary>
    /// <param name="dataValue"></param>
    /// <returns></returns>
    public static bool IsCmisInteger(string dataValue)
    {
        if (!double.TryParse(dataValue, NumberStyles.Integer, null, out double result)) return false;
        var number = Convert.ToInt32(result);
        return number is >= -32768 and <= 32768;
    }
    /// <summary>
    /// IsCmiIdentifier:
    ///     the length of Identifier must be between 1 and 255.
    ///     Then the regex expression \W will match on non-alphanumeric including spaces (whereas \w matches on alphanumeric)
    /// </summary>
    /// <param name="dataValue"></param>
    /// <returns></returns>
    public static bool IsCmiIdentifier(string dataValue)
    {
        if (dataValue.Length is > 255 or 0) return false;
        return !IsMatch(dataValue, @"\W+");
    }
    /// <summary>
    /// IsRangeValid:
    ///     First, make sure value Is integer.
    ///     Then, make sure it Is between the low and high values
    /// </summary>
    /// <param name="dataValue"></param>
    /// <param name="low"></param>
    /// <param name="high"></param>
    /// <returns></returns>
    public static bool IsRangeValid(string dataValue, int low, int high)
    {
        if (!IsCmisInteger(dataValue)) return false;
        var i = Convert.ToInt32(dataValue);
        return i >= low && i <= high;
    }
    /// <summary>
    /// CMITime
    /// </summary>
    /// <param name="dataValue"></param>
    /// <returns></returns>
    public static bool IsCmiTime(string dataValue)
    {
        // CMITime Is like HH:MM:SS.SS
        Check.NotNull(dataValue, nameof(dataValue));

        if (dataValue.Length > 11) return false;

        var delimiter = TimeDelimiter.ToCharArray();

        var splitString = dataValue.Split(delimiter, 3);
        if (splitString.Length != 3) return false;

        var hours = splitString[0];
        var minutes = splitString[1];
        var secs = splitString[2];

        if (!IsCmiInteger(hours)) return false;
        if (!IsCmiInteger(minutes)) return false;
        if (!IsCmiDecimal(secs)) return false;

        var iHours = Convert.ToInt32(hours);
        if (iHours is < 0 or > 23) return false;

        var iMinutes = Convert.ToInt32(minutes);
        if (iMinutes is < 0 or > 59) return false;

        var dSecs = Convert.ToDecimal(secs);
        return dSecs >= 0 && dSecs <= Convert.ToDecimal("59.99");
    }

    /// <summary>
    /// CMITimespan
    /// </summary>
    /// <param name="dataValue"></param>
    /// <returns></returns>
    public static bool IsCmiTimespan(string dataValue)
    {
        // CMITimeSpan Is like "HHHH:MM:SS.SS";

        Check.NotNull(dataValue, nameof(dataValue));
        if (dataValue.Length > 13) return false;

        var delimiter = TimeDelimiter.ToCharArray();
        var splitsStrings = dataValue.Split(delimiter, 3);
        if (splitsStrings.Length != 3) return false;
        var hours = splitsStrings[0];
        var minutes = splitsStrings[1];
        var secs = splitsStrings[2];

        if (hours.Length is < 2 or > 4) return false;
        if (minutes.Length != 2) return false;

        if (secs.IndexOf(".", StringComparison.Ordinal) == 0 || secs.IndexOf(".", StringComparison.Ordinal) == 1) return false;

        if (!IsCmiInteger(hours)) return false;
        if (!IsCmiInteger(minutes)) return false;
        if (!IsCmiDecimal(secs)) return false;

        var iMinutes = Convert.ToInt32(minutes);
        if (iMinutes is < 0 or > 99) return false;
        var dSecs = Convert.ToDecimal(secs);
        return dSecs >= 0 && dSecs <= Convert.ToDecimal("59.99");
    }


    /// <summary>
    /// SCORM2004:
    ///     CMITimeInterval Is like "PT3H5M2S" which Is 3 hours, 5 minutes, 2 seconds;
    ///     it starts with P
    ///     if year/month/day duration Is present, it Is like P1Y3M2D which Is 1 Year, 3 months, 2 days
    ///     so altogether it might be P1Y3M2DT3H5M2S
    /// </summary>
    /// <param name="dataValue"></param>
    /// <returns></returns>
    public static bool IsCmiTimeInterval(string dataValue)
    {
        return dataValue.StartsWith("P");

    }

    /// <summary>
    /// Add two CMITimeSpan values together. Returns "false" if either argument Isn't a valid CMITimeSpan.
    ///     CMITimeSpan Is "HHHH:MM:SS.ss"
    /// </summary>
    /// <param name="cmiTimeSpan1"></param>
    /// <param name="cmiTimeSpan2"></param>
    /// <returns></returns>
    public static string AddCmiTime(string cmiTimeSpan1, string cmiTimeSpan2)
    {
        if (!IsCmiTimespan(cmiTimeSpan1))
        {
            return "false";
        }
        if (!IsCmiTimespan(cmiTimeSpan2))
        {
            return "false";
        }

        // both fields are valid. Now add them
        //HHHH:MM:SS.SS
        // split the first one into its parts
        var delimiter = TimeDelimiter.ToCharArray();
        var s1a = cmiTimeSpan1.Split(delimiter, 3);
        var hours1 = s1a[0];
        var mins1 = s1a[1];
        var secs1 = s1a[2];
        var iHours1 = Convert.ToInt32(hours1);
        var iMins1 = Convert.ToInt32(mins1);
        var dSecs1 = Convert.ToDecimal(secs1);
        // split the second one into its parts
        var s2a = cmiTimeSpan2.Split(delimiter, 3);
        var hours2 = s2a[0];
        var mins2 = s2a[1];
        var secs2 = s2a[2];
        var iHours2 = Convert.ToInt32(hours2);
        var iMins2 = Convert.ToInt32(mins2);
        var dSecs2 = Convert.ToDecimal(secs2);
        // add the seconds together
        var totsecs = dSecs1 + dSecs2;
        // figure the remainder (take mod 60)
        var SecsRemainder = (totsecs % 60);
        var iMinstoCarry = (Convert.ToInt32(totsecs - SecsRemainder)) / 60;
        //  add the minutes together
        var iMinsTot = iMins1 + iMins2 + iMinstoCarry;
        var iMinsRemainder = (iMinsTot % 60);
        var iMinsToCarry = (iMinsTot - iMinsRemainder) / 60;
        // add the hours together
        var iHoursTot = iHours1 + iHours2 + iMinsToCarry;
        // assemble the new value
        var sNew = new StringBuilder();
        sNew.AppendFormat("{0,4:0000}", iHoursTot);
        sNew.Append(":");
        sNew.AppendFormat("{0,2:00}", iMinsRemainder);
        sNew.Append(":");
        sNew.AppendFormat("{0,4:00.00}", SecsRemainder);
        return sNew.ToString();
    }

}
