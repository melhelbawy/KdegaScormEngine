using Kdega.ScormEngine.Application.Common.Models;

namespace Kdega.ScormEngine.Application.Extensions;
public static class LmsRequestExtensions
{
    public static void InitCode201(this LmsRequest request)
    {
        request.ErrorCode = "201";
        request.ReturnValue = "false";
        request.ErrorString = "Invalid argument error";
    }

    public static void InitCode202(this LmsRequest request)
    {
        request.ReturnValue = "";
        request.ErrorCode = "202";
        request.ErrorString = "Element cannot have children";
    }

    public static void InitCode203(this LmsRequest request)
    {
        request.ReturnValue = "";
        request.ErrorCode = "203";
        request.ErrorString = "Element not an array - Cannot have count";
    }

    public static void InitCode301(this LmsRequest request)
    {
        request.ErrorCode = "301";
        request.ErrorString = "No cmi_core record for this session";
        request.ReturnValue = "false";
    }

    public static void InitCode402(this LmsRequest request)
    {
        request.ErrorCode = "402";
        request.ReturnValue = "false";
        request.ErrorString = "Invalid set value, element is a keyword";
    }

    public static void InitCode403(this LmsRequest request)
    {
        request.ErrorCode = "403";
        request.ReturnValue = "false";
        request.ErrorString = "Element is readonly";
    }

    public static void InitCode404(this LmsRequest request)
    {
        request.ErrorCode = "404";
        request.ErrorString = "Element is write only";
        request.ReturnValue = "";
    }

    public static void InitCode405(this LmsRequest request)
    {
        request.ErrorCode = "405";
        request.ReturnValue = "false";
        request.ErrorString = "Incorrect DataType";
    }
}
