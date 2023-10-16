//debug info
var blnDebug = false;
var aryDebug = new Array();
var strDebug = "";
var winDebug;
// establish a pointer to this api so the SCO can find it (HAS to be called "API" for 1.2 and 
// for SCORM 2004 it has to be API_1484_11
var API = new ApiClass();
API.version = '1.2';
var API_1484_11 = new ApiClass();
API_1484_11.version = '1.3';
var sessionId;
var learnerId;
var coreId;
var learnerPackageId;
var scorm_package_id;
var sco_identifier;
var exit_status;
var scormApis = "api/v1/scormplayer";

/****************************************************************************************
****************************************************************************************/
function ApiClass() {
    this._Debug = true;  // set this to false to turn debugging off

    this.version = '1.3'; // new value for SCORM2004

    // Define exception/error codes
    this._NoError = 0;
    this._GeneralException = 101;
    this._ServerBusy = 102;
    this._InvalidArgumentError = 201;
    this._ElementCannotHaveChildren = 202;
    this._ElementIsNotAnArray = 203;
    this._NotInitialized = 301;
    this._NotImplementedError = 401;
    this._InvalidSetValue = 402;
    this._ElementIsReadOnly = 403;
    this._ElementIsWriteOnly = 404;
    this._IncorrectDataType = 405;
    // define properties
    this.serviceAvailable = false; // WebService initialization state
    this.initialized = false; // SCO LMSInitialize state
    this.LastError = this._NotInitialized;
    this.LastErrorString = "";
    this.LastErrorDiagnostic = "";

    this._sessionid = "";
    this._userid = "";
    this._coreid = "";
    this._learnerPackageId = "";
    this._scorm_package_id = "";
    this._sco_identifier = "";
    this._exit_status = ""; // this will stay as is in browse or review mode, will change if they are actually taking the sco for credit to completed. Will only request the next page if this is "completed"

    // initialize the member function references
    // for the class prototype
    if (typeof (window._api_prototype_called) == 'undefined') {
        window._api_prototype_called = true;
        ApiClass.prototype.LMSInitialize = _LMSInitialize;
        ApiClass.prototype.LMSFinish = _LMSFinish;
        ApiClass.prototype.LMSGetValue = _LMSGetValue;
        ApiClass.prototype.LMSSetValue = _LMSSetValue;
        ApiClass.prototype.LMSCommit = _LMSCommit;
        ApiClass.prototype.LMSGetLastError = _LMSGetLastError;
        ApiClass.prototype.LMSGetErrorString = _LMSGetErrorString;
        ApiClass.prototype.LMSGetDiagnostic = _LMSGetDiagnostic;
        // for SCORM 2004
        ApiClass.prototype.Initialize = _LMSInitialize;
        ApiClass.prototype.Terminate = _LMSFinish;
        ApiClass.prototype.GetValue = _LMSGetValue;
        ApiClass.prototype.SetValue = _LMSSetValue;
        ApiClass.prototype.Commit = _LMSCommit;
        ApiClass.prototype.GetLastError = _LMSGetLastError;
        ApiClass.prototype.GetErrorString = _LMSGetErrorString;
        ApiClass.prototype.GetDiagnostic = _LMSGetDiagnostic;
    }

    /*******************************************************************************
    **
    ** Function: LMSInitialize()
    ** Inputs:  None
    ** Return:  CMIBoolean true if the initialization was successful, or
    **          CMIBoolean false if the initialization failed.
    **
    ** Description:
    ** Initialize communication with LMS by calling the LMSInitialize
    ** Web Service
    **
    *******************************************************************************/

    function _LMSInitialize(val) {

        WriteToDebug("----------------------------------------");
        WriteToDebug("----------------------------------------");
        WriteToDebug("In LMS Initialize");
        WriteToDebug("Browser Info (" + navigator.appName + " " + navigator.appVersion + ")");
        WriteToDebug("URL: " + window.document.location.href);
        WriteToDebug("----------------------------------------");
        WriteToDebug("----------------------------------------");

        if (val != '') {
            this.LastErrorString = "Value passed to LMSInitialize, should be blank";
            this.LastError = "201";
            this.LastErrorDiagnostic = "Error from API";
            return "false";
        }

        if (this.initialized) {
            this.LastErrorString = "LMS is already initialized, call to LMSInitialize ignored.";
            this.LastError = "101";
            this.LastErrorDiagnostic = "Error from API";
            WriteToDebug(this.LastErrorString);
            return "false";
        }
        // the calling application leaves a session id and other variables 
        this._sessionid = lmsClient.sessionId;
        this._learnerPackageId = lmsClient.learnerPackageId;
        this._userid = lmsClient.learnerId;
        this._coreid = lmsClient.coreId;
        this._scorm_package_id = lmsClient.scorm_package_id;
        this._sco_identifier = lmsClient.sco_identifier;
        // LMSInfo object carries arguments to the server and back
        var lmsRequest = JSON.stringify(createLMSInfo(this._sessionid, this._userid, this._coreid, this._learnerPackageId, this._scorm_package_id, this._sco_identifier));
        WriteToDebug("LMSInitialize: " + lmsRequest);
        var that = this; // get reference to current API instance
        $.ajax({
            type: "POST",
            url: `${scormApis}/lms-initialize`,
            data: lmsRequest,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            success: function (response) {
                lmsRequest = response.value;
                that.LastError = lmsRequest.errorCode;
                that.LastErrorString = lmsRequest.errorString
                // check error code from server
                if (lmsRequest.errorCode != "0") {
                    that.initialized = false;
                }
                else {
                    that.initialized = true;
                }
                //    return (API.initialized) ? "true" : "false";
            },
            error: function (request, error) {
                // Ajax call failed
                that.LastError = "101"; //general exception
                that.LastErrorString = error.Message;
                that.LastErrorDiagnostic = "AJAX error";
                WriteToDebug("Ajax error");
                WriteToDebug(error.Message);
                return "false";
            }
        });
        return (this.initialized) ? "true" : "false";
    }
    /*******************************************************************************
    **
    ** Function LMSFinish()
    ** Inputs:  None
    ** Return:  CMIBoolean true if successful
    **          CMIBoolean false if failed.
    **
    ** Description:
    ** Close communication with LMS by calling the LMSFinish
    ** function which will be implemented by the LMS
    **
    *******************************************************************************/
    function _LMSFinish(val) {
        WriteToDebug("LMSFinish");
        if (val != '') {
            this.LastErrorString = "Value passed to LMSFinish, should be blank";
            this.LastError = "201";
            this.LastErrorDiagnostic = "Error from API";
            return "false";
        }
        if (!this.initialized) {
            this.LastErrorString = "LMS is not initialized, call to LMSFinish ignored.";
            this.LastError = "301";
            this.LastErrorDiagnostic = "Error from API";
            return "false";
        }
        // LMSInfo object carries arguments to the server and back
        var lmsRequest = JSON.stringify(createLMSInfo(this._sessionid, this._userid, this._coreid, this._learnerPackageId, this._scorm_package_id, this._sco_identifier));
        WriteToDebug("LMSFinish: " + JSON.stringify({ 'lmsRequest': lmsRequest }));
        var that = this; // get reference to current instance
        $.ajax({
            type: "POST",
            url: `${scormApis}/lms-finish`,
            data: lmsRequest,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: true,
            success: LMSFinish_callback,
            error: function (request, error) {
                // Ajax call failed
                that.LastError = "101"; //general exception
                that.LastErrorString = error;
                that.LastErrorDiagnostic = "AJAX error";
                WriteToDebug("Ajax error");
                WriteToDebug(error.Message);
                return "false";
            }
        });

        // AJAX callback for the LMSFinish call
        function LMSFinish_callback(response) {
            lmsRequest = response.value.d == null ? response.value : response.value.d; // asp.net 3.5 adds the 'd' attribute to the response object
            that.LastError = lmsRequest.errorCode;
            that.LastErrorString = lmsRequest.errorString
            that.LastError = "101";
            that.LastErrorString = "";
            that.initialized = false;
        }
        return (this.initialized) ? "true" : "false";
    }

    /*******************************************************************************
    **
    ** Function LMSGetValue(name)
    ** Inputs:  name - string representing the cmi data model defined category or
    **             element (e.g. cmi.core.student_id)
    ** Return:  The value presently assigned by the LMS to the cmi data model
    **       element defined by the element or category identified by the name
    **       input value.
    **
    ** Description:
    ** Wraps the call to the LMS LMSGetValue method
    **
    *******************************************************************************/
    function _LMSGetValue(name) {
        WriteToDebug("LMSGetValue");
        if (!this.initialized) {
            this.LastErrorString = "LMS is not initialized, call to LMSGetValue ignored.";
            this.LastError = "301";
            this.LastErrorDiagnostic = "Error from API";
            return "";
        }
        var lmsRequest = JSON.stringify(createLMSInfo(this._sessionid, this._userid, this._coreid, this._learnerPackageId, this._scorm_package_id, this._sco_identifier, name));
        WriteToDebug("GETVALUE: " + JSON.stringify({ 'lmsRequest': lmsRequest }));

        var returnValue = '';
        var that = this; // get reference to current API instance
        $.ajax({
            type: "GET",
            url: `${scormApis}/lms-get-value`,
            data: lmsRequest,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            success: function (response) {
                lmsRequest = response.value;
                that.LastError = lmsRequest.errorCode;
                that.LastErrorString = lmsRequest.errorString;
                // check error code from server
                if (lmsRequest.errorCode != "0") {
                    return "false";
                }
                else {
                    returnValue = lmsRequest.returnValue;
                    return lmsRequest.returnValue;

                }
            },
            error: function (request, error) {
                // Ajax call failed
                that.LastError = "101"; //general exception
                that.LastErrorString = error;
                that.LastErrorDiagnostic = "AJAX error";
                WriteToDebug("Ajax error");
                WriteToDebug(error.Message);
                return "false";

            }
        });

        return returnValue;

    }
    /*******************************************************************************
    **
    ** Function LMSSetValue(name, value)
    ** Inputs:  name -string representing the data model defined category or element
    **          value -the value that the named element or category will be assigned
    ** Return:  CMIBoolean true if successful
    **          CMIBoolean false if failed.
    **
    ** Description:
    ** Wraps the call to the LMS LMSSetValue function
    **
    *******************************************************************************/
    function _LMSSetValue(name, value) {
        if (name == "cmi.core.lesson_status" || name == "cmi.core.exit") {
            this._exit_status = value; // set this so LMSFinish knows what to do
        }
        if (!this.initialized) {
            this.LastErrorString = "LMS is not initialized, call to LMSSetValue ignored.";
            this.LastError = "301";
            this.LastErrorDiagnostic = "Error from API";
            return "false";
        }
        var lmsRequest = JSON.stringify(createLMSInfo(this._sessionid, this._userid, this._coreid, this._learnerPackageId, this._scorm_package_id, this._sco_identifier, name, value));
        WriteToDebug("SETVALUE: " + JSON.stringify({ 'lmsRequest': lmsRequest }));
        var returnValue = '';
        var that = this; // get reference to current API instance
        $.ajax({
            type: "PUT",
            url: `${scormApis}/lms-set-value`,
            data: lmsRequest,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            success: function (response) {
                lmsRequest = response.value;
                that.LastError = lmsRequest.errorCode;
                that.LastErrorString = lmsRequest.errorString
                // check error code from server
                if (lmsRequest.errorCode != "0") {
                    returnValue = "false;"
                    return "false";
                }
                else {
                    returnValue = lmsRequest.ReturnValue;
                    return lmsRequest.ReturnValue;
                }
            },
            error: function (request, error) {
                // Ajax call failed
                that.LastError = "101"; //general exception
                that.LastErrorString = error;
                that.LastErrorDiagnostic = "AJAX error";
                WriteToDebug("Ajax error");
                WriteToDebug(error.Message);
                return "false";
            }
        });
        return returnValue;
    }
    /*******************************************************************************
    **
    ** Function LMSCommit()
    ** Inputs:  None
    ** Return:  None
    **
    ** Description:
    ** Call the LMSCommit function 
    **
    *******************************************************************************/
    function _LMSCommit(val) {
        // LMSCommit is a no-op since we commit every time.
        if (val != '') {
            this.LastErrorString = "Value passed to LMSCommit, should be blank";
            this.LastError = "201";
            this.LastErrorDiagnostic = "Error from API";
            return "false";
        }
        if (!this.initialized) {
            this.LastErrorString = "LMS is not initialized, call to LMSCommit ignored.";
            this.LastError = "301";
            this.LastErrorDiagnostic = "Error from API";
            return "false";
        }
        return "true";
    }
    /*******************************************************************************
    **
    ** Function LMSGetLastError()
    ** Inputs:  None
    ** Return:  The error code that was set by the last LMS function call
    **
    ** Description:
    ** Call the LMSGetLastError function 
    **
    *******************************************************************************/
    function _LMSGetLastError() {
        return this.LastError;
    }

    /*******************************************************************************
    **
    ** Function LMSGetErrorString(errorCode)
    ** Inputs:  errorCode - Error Code
    ** Return:  The textual description that corresponds to the input error code
    **
    ** Description:
    ** Call the LMSGetErrorString function 
    **
    ********************************************************************************/
    function _LMSGetErrorString() {
        return this.LastErrorString;
    }

    /*******************************************************************************
    **
    ** Function MSGetDiagnostic()
    ** Return:  The vendor specific textual description that corresponds to the 
    **          input error code
    **
    ** Description:
    ** Call the LMSGetDiagnostic function
    **
    *******************************************************************************/
    function _LMSGetDiagnostic() {
        return this.LastErrorDiagnostic;
    }
} // end API
/*********************************************************************************
**
** Function createLMSInfo(...)
** returns an LMSInfo object for data transfer to and from the server
 * 
*********************************************************************************/
function createLMSInfo(sessionId, learnerId, coreId, learnerPackageId, scormPackageId, scoIdentifier, dataItem, dataValue, errorCode, errorString, errorDiagnostic, returnValue) {
    const lmsRequest = new Object();
    lmsRequest.sessionId = sessionId;
    lmsRequest.LearnerId = learnerId;
    lmsRequest.coreId = coreId;
    lmsRequest.scoIdentifier = scoIdentifier;
    lmsRequest.scorm_package_id = scormPackageId;
    lmsRequest.LearnerScormPackageId = learnerPackageId;
    lmsRequest.dataItem = dataItem;
    lmsRequest.dataValue = dataValue;
    lmsRequest.errorCode = errorCode;
    lmsRequest.errorString = errorString;
    lmsRequest.errorDiagnostic = errorDiagnostic;
    lmsRequest.returnValue = returnValue;
    for (let prop in lmsRequest) {
        if (Object.prototype.hasOwnProperty.call(lmsRequest, prop)) {
            if (lmsRequest[prop] == null) {
                lmsRequest[prop] = "null";
            }
        }
    }
    return lmsRequest;
}
/*********************************************************************************
**
** Debug functions
**
*********************************************************************************/
function WriteToDebug(strInfo) {

    if (blnDebug) {

        var dtm = new Date();
        var strLine;

        strLine = aryDebug.length + ":" + dtm.toString() + " - " + strInfo;

        aryDebug[aryDebug.length] = strLine;

        if (winDebug && !winDebug.closed) {
            winDebug.document.body.appendChild(winDebug.document.createTextNode(strLine));
            winDebug.document.body.appendChild(winDebug.document.createElement("br"));
        }

    }
    return;
}

//public
function ShowDebugWindow() {
    var renderLog = function () {
        var i,
            len = aryDebug.length;

        winDebug.document.body.innerHTML = "";
        for (i = 0; i < len; i += 1) {
            winDebug.document.body.appendChild(winDebug.document.createTextNode(aryDebug[i]));
            winDebug.document.body.appendChild(winDebug.document.createElement("br"));
        }
    };

    if (winDebug && !winDebug.closed) {
        winDebug.close();
    }

    winDebug = window.open("/SCORM/ThinClient/blank.html", "Debug", "width=600,height=300,resizable,scrollbars");

    if (winDebug === null) {
        alert("Debug window could not be opened, popup blocker in place?");
    }
    else {
        if (winDebug.addEventListener || winDebug.attachEvent) {
            winDebug[winDebug.addEventListener ? 'addEventListener' : 'attachEvent'](
                (winDebug.attachEvent ? 'on' : '') + 'load',
                renderLog,
                false
            );
        }

        renderLog();

        winDebug.document.close();
        winDebug.focus();
    }

    return;
}

//public
function DisplayError(strMessage) {

    var blnShowDebug;

    WriteToDebug("In DisplayError, strMessage=" + strMessage);

    blnShowDebug = confirm("An error has occured:\n\n" + strMessage + "\n\nPress 'OK' to view debug information to send to technical support.");

    if (blnShowDebug) {
        ShowDebugWindow();
    }

}

