namespace Kdega.ScormEngine.Application.Constants;
public class ErrorCodes
{
    public const int NoError = 0;
    public const int GeneralException = 101;
    public const int GeneralInitializationFailure = 102;
    public const int AlreadyInitializedException = 103;
    public const int ContentInstanceTerminated = 104;
    public const int GeneralTerminationFailure = 111;
    public const int TerminationBeforeInitialization = 112;
    public const int TerminationAfterTermination = 113;
    public const int RetrieveDataBeforeInitialization = 122;
    public const int RetrieveDataAfterTermination = 123;
    public const int StoreDataBeforeInitialization = 132;
    public const int StoreDataAfterTermination = 133;
    public const int CommitBeforeInitialization = 142;
    public const int CommitAfterTermination = 143;
    public const int InvalidArgumentError = 201;
    public const int GeneralArgumentError = 201;
    public const int ElementCannotHaveChildren = 202;
    public const int ElementNotAnArrayCannotHaveCount = 203;
    public const int NotInitialized = 301;
    public const int GeneralGetFailure = 301;
    public const int GeneralSetFailure = 351;
    public const int GeneralCommitFailure = 391;
    public const int NotImplementedError = 401;
    public const int UndefinedDataModelElement = 401;
    public const int InvalidSetValueElementIsAKeyword = 402;
    public const int UnimplementedDataModelElement = 402;
    public const int ElementIsReadOnly = 403;
    public const int DataModelElementValueNotInitialized = 403;
    public const int ElementIsWriteOnly = 404;
    public const int DataModelElementIsReadOnly = 404;
    public const int IncorrectDataType = 405;
    public const int DataModelElementIsWriteOnly = 405;
    public const int DataModelElementTypeMismatch = 406;
    public const int DataModelElementValueOutOfRange = 407;
    public const int DataModelDependencyNotEstablished = 408;


}