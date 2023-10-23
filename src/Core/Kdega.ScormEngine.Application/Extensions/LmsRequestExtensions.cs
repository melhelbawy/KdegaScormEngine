using Kdega.ScormEngine.Application.Common.Models;
using Kdega.ScormEngine.Domain.Constants.ScormLms;

namespace Kdega.ScormEngine.Application.Extensions;
public static class LmsRequestExtensions
{
    public static void InitCode0Success(this LmsRequest request, string returnValue = "true")
    {
        request.ReturnValue = returnValue;
        request.ErrorCode = ErrorCodes.NoError.ToString();
        request.ErrorString = "";
    }
    public static void InitCode201(this LmsRequest request)
    {
        request.ReturnValue = "false";
        request.ErrorCode = ErrorCodes.InvalidArgumentError.ToString();
        request.ErrorString = ErrorCodesMessages.InvalidArgumentError;
    }

    public static void InitCode202(this LmsRequest request)
    {
        request.ReturnValue = "";
        request.ErrorCode = ErrorCodes.ElementCannotHaveChildren.ToString();
        request.ErrorString = ErrorCodesMessages.ElementCannotHaveChildren;
    }

    public static void InitCode203(this LmsRequest request)
    {
        request.ReturnValue = "";
        request.ErrorCode = ErrorCodes.ElementNotAnArrayCannotHaveCount.ToString();
        request.ErrorString = ErrorCodesMessages.ElementNotAnArrayCannotHaveCount;
    }

    public static void InitCode301(this LmsRequest request)
    {
        request.ReturnValue = "false";
        request.ErrorCode = ErrorCodes.NotInitialized.ToString();
        request.ErrorString = ErrorCodesMessages.NotInitialized;
    }

    public static void InitCode402(this LmsRequest request)
    {
        request.ReturnValue = "false";
        request.ErrorCode = ErrorCodes.InvalidSetValueElementIsAKeyword.ToString();
        request.ErrorString = ErrorCodesMessages.InvalidSetValueElementIsAKeyword;
    }

    public static void InitCode403(this LmsRequest request)
    {
        request.ReturnValue = "false";
        request.ErrorCode = ErrorCodes.ElementIsReadOnly.ToString();
        request.ErrorString = ErrorCodesMessages.ElementIsReadOnly;
    }

    public static void InitCode404(this LmsRequest request)
    {
        request.ReturnValue = "";
        request.ErrorCode = ErrorCodes.ElementIsWriteOnly.ToString();
        request.ErrorString = ErrorCodesMessages.ElementIsWriteOnly;
    }

    public static void InitCode405(this LmsRequest request)
    {
        request.ReturnValue = "false";
        request.ErrorCode = ErrorCodes.IncorrectDataType.ToString();
        request.ErrorString = ErrorCodesMessages.IncorrectDataType;
    }
}
