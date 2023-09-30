using Kdega.ScormEngine.Application.Behavior;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
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


    #region SCORM Api Keys Validations
    /// <summary>
    /// List of scorm Api vocabularies
    /// </summary>
    private static readonly string[,] _vocabulary = {
        {"mode","normal"},
        {"mode","review"},
        {"mode","browse"},
        {"status","passed"},
        {"status","completed"},
        {"status","failed"},
        {"status","incomplete"},
        {"status","browsed"},
        {"status","not attempted"},
        {"exit","time-out"},
        {"exit","suspend"},
        {"exit","logout"},
        {"exit",""},
        {"credit","no-credit"},
        {"credit","credit"},
        {"entry","ab-initio"},
        {"entry","resume"},
        {"entry",""},
        {"interaction","true-false"},
        {"interaction","choice"},
        {"interaction","fill-in"},
        {"interaction","matching"},
        {"interaction","performance"},
        {"interaction","likert"},
        {"interaction","sequencing"},
        {"interaction","numeric"},
        {"result","correct"},
        {"result","wrong"},
        {"result","unanticipated"},
        {"result","neutral"},
        {"time_limit_action","exit,message"},
        {"time_limit_action","exit,no message"},
        {"time_limit_action","continue,message"},
        {"time_limit_action","continue,no message"},
        {"success_status","passed"},
        {"success_status","failed"},
        {"success_status","unknown"},
        {"completion_status","completed"},
        {"completion_status","incomplete"},
        {"completion_status","unknown"}
    };

    /// <summary>
    /// Check to see if the vocabulary type Is valid (note, these are case-sensitive. The spec doesn't say thIs
    /// explicitly but the test suite fails if you give "Passed" instead of "passed".
    /// </summary>
    /// <param name="vocabularyType"></param>
    /// <param name="dataValue"></param>
    /// <returns></returns>
    public static bool IsCmiVocabulary(string vocabularyType, string dataValue)
    {
        for (var i = 0; i < _vocabulary.GetLength(0); i++)
        {
            if (_vocabulary[i, 0] != vocabularyType) continue;
            if (_vocabulary[i, 1] == dataValue) return true;
        }
        return false;
    }

    /// <summary>
    /// Validate if is interaction Pattern valid
    /// </summary>
    /// <param name="type"></param>
    /// <param name="dataValue"></param>
    /// <returns></returns>
    public static bool IsInteractionPatternValid(string type, string dataValue)
    {
        dataValue = dataValue.ToLower();
        switch (type)
        {
            case "true-false":
                var s = dataValue[..1].ToLower();
                return s is "0" or "1" or "t" or "f";
            case "choice":
                // TODO - I need a regular expression to validate thIs
                return true;
            case "fill-in":
                return dataValue.Length <= 255;
            case "numeric":
                return IsCmiDecimal(dataValue);
            case "likert":
                return dataValue.Trim() == "" || Regex.IsMatch(dataValue, @"\w"); // alphanumeric only
            case "matching":
                // TODO - I need a regular expression to validate thIs
                return true;
            case "performance":
                return dataValue.Length <= 255;
            case "sequencing":
                // TODO - I need a regular expression to validate thIs
                return true;
            default:
                return false;
        }
    }

    /// <summary>
    /// The "result" field in the cmi.interactions.n.result "SetValue" call
    /// Is either a valid vocabulary OR a valid Decimal
    /// </summary>
    /// <param name="dataValue"></param>
    /// <returns></returns>
    public static bool IsResultValid(string dataValue) =>
        IsCmiVocabulary("result", dataValue) || IsCmiDecimal(dataValue);

    private static readonly string[] ReadOnlyCalls = {    "cmi.core.student_id",
                                             "cmi.core.student_name",
                                             "cmi.core.credit",
                                             "cmi.core.entry",
                                             "cmi.core.total_time",
                                             "cmi.core.lesson_mode",
                                             "cmi.launch_data",
                                             "cmi.comments_from_lms",
                                             "cmi.student_data.mastery_score",
                                             "cmi.student_data.max_time_allowed",
                                             "cmi.student_data.time_limit_action",
                                             "cmi.interactions._count"
                                         };
    private static readonly string[] WriteOnlyCalls = {  "cmi.core.session_time",
                                              "cmi.core.exit",
                                            "pattern",
                                            "id",
                                            "time",
                                            "type",
                                            "weighting",
                                            "student_response",
                                            "result",
                                            "latency"

                                         };
    private static readonly string[] KeywordCalls = {"cmi.core._children",
                                             "cmi.core.score._children",
                                             "cmi.objectives._children",
                                             "cmi.objectives._count",
                                             "cmi.student_data._children",
                                             "cmi.student_preference._children",
                                             "cmi.interactions._children",
                                             "cmi.interactions._count,"

                                         };


    /// <summary>
    /// Provides a fast check to see if the dataValue they passed to LMSSetValue Is ReadOnly
    /// if so we are supposed to return an error
    /// </summary>
    /// <param name="dataValue"></param>
    /// <returns></returns>
    public static bool IsReadOnly(string dataValue) =>
        ReadOnlyCalls.Any(t => string.Equals(t, dataValue,
            StringComparison.CurrentCultureIgnoreCase));

    /// <summary>
    /// Provides a fast check to see if the dataValue they passed to LMSSetValue Is a keyword
    /// if so we are supposed to return an error
    /// </summary>
    /// <param name="dataValue"></param>
    /// <returns></returns>
    public static bool IsKeyword(string dataValue) =>
        KeywordCalls.Any(t => string.Equals(t.ToLower(), dataValue.ToLower(),
            StringComparison.CurrentCultureIgnoreCase));

    /// <summary>
    /// Provides a fast check to see if the dataValue they passed to LMSGetValue Is WriteOnly
    /// if so we are supposed to return an error
    /// </summary>
    /// <param name="dataValue"></param>
    /// <returns></returns>
    public static bool IsWriteOnly(string dataValue) =>
        WriteOnlyCalls.Any(t => string.Equals(t.ToLower(), dataValue.ToLower(),
            StringComparison.InvariantCultureIgnoreCase));

    #endregion
}
