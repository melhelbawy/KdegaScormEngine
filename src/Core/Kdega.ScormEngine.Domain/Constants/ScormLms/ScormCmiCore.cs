using Newtonsoft.Json;

namespace Kdega.ScormEngine.Domain.Constants.ScormLms;
public class ScormCmiCore
{

    [JsonProperty("cmi.core._children")]
    public const string CmiChildren = "student_id,student_name,lesson_status,lesson_location,lesson_mode,score,credit,entry,exit,session_time,total_time";

    [JsonProperty("cmi.core.score_children")]
    public const string CmiScoreChildren = "raw,min,max";

    [JsonProperty("cmi.objectives.n.score._children")]
    public const string CmiObjectivesScoreChildren = "raw,min,max";

    [JsonProperty("cmi.objectives._children")]
    public const string CmiObjectivesChildren = "id,score,status";

    [JsonProperty("cmi.student_data._children")]
    public const string CmiDataChildren = "mastery_score, max_time_allowed, time_limit_action";

    [JsonProperty("cmi.student_preference._children")]
    public const string CmiPreferenceChildren = "audio,language,speed,text";

    [JsonProperty("cmi.interactions._children")]
    public const string CmiInteractionsChildren = "id,objectives,time,type,correct_responses,weighting,student_response,result,latency";

    [JsonProperty("cmi.comments_from_learner._children")]
    public const string CmiICommentsFromLearnerChildren = "comment,location,timestamp";

    [JsonProperty("cmi.comments_from_lms._children")]
    public const string CmiICommentsFromLmsChildren = "comment,location,timestamp";

    public class CmiCoreCredits
    {
        public const string Credit = "credit";
        public const string NoCredit = "no-credit";

    }

    public class CmiCoreLessonStatus
    {
        public const string Passed = "passed";
        public const string Completed = "completed";
        public const string Failed = "failed";
        public const string Incomplete = "incomplete";
        public const string Browsed = "browsed";
        public const string NotAttempted = "not attempted";
    }
    public class CmiCoreSuccessStatus
    {
        public const string Passed = "passed";
        public const string Failed = "failed";
        public const string Unknown = "unknown";
    }

    public class CmiCoreEntry
    {
        public const string AbInitio = "ab-initio";
        public const string Resume = "resume";
    }

    public class CmiCoreLessonMode
    {
        public const string Browse = "browse";
        public const string Normal = "normal";
        public const string Review = "review";
    }
    public class CmiCoreCompletionStatus
    {
        public const string Completed = "completed";
        public const string Incomplete = "incomplete";
        public const string NotAttempted = "not attempted";
        public const string Unknown = "unknown";
    }
    public class CmiCoreLessonExit
    {
        public const string TimeOut = "time-out";
        public const string Suspend = "suspend";
        public const string Logout = "logout";
        public const string Empty = "";
    }
    public class CmiDataTimeLimitActions
    {
        public const string ExitMessage = "exit,message";
        public const string ExitNoMessage = "exit,no message";
        public const string ContinueMessage = "continue,message";
        public const string ContinueNoMessage = "continue, no message";
    }
    public class CmiInteractionsNType
    {
        public const string TrueFalse = "true-false";
        public const string Choice = "choice";
        public const string FillIn = "fill-in";
        public const string Matching = "matching";
        public const string Performance = "performance";
        public const string Sequencing = "sequencing";
        public const string Likert = "likert";
        public const string Numeric = "numeric";
    }
    public class CmiInteractionsNResult
    {
        public const string correct = "correct";
        public const string wrong = "wrong";
        public const string unanticipated = "unanticipated";
        public const string neutral = "matcneutralhing";
        public const string XX = "x.x [CMIDecimal]";
    }
}
