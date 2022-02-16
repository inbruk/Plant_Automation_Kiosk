
var CommonServicesUrl = 'https://localhost:44375/odata/';
var X_XSRF_Token = '';

function toolGetSuccessData(jqXHRData) {
    return jqXHRData.value;
}

function toolGetErrorData(jqXHRData) {

    var err_str = jqXHRData.url + " -> " + jqXHRData.statusText + " " + jqXHRData.responseText;
    console.log(err_str);

    return null;
}

function GetWorkshops()
{
    var result = null;

    $.ajax
    ({
        "async": false,
        "crossDomain": true,
        "url": CommonServicesUrl + "Workshop",
        "method": "GET",
        "dataType": "json",
        "headers": {
            "Content-Type": "application/json",
            "X-XSRF-Token": X_XSRF_Token
        }
    })
        .done(function (jqXHRData) { result = toolGetSuccessData(jqXHRData); })
        .fail(function (jqXHRData) { result = toolGetErrorData(jqXHRData); });

    return result;
}

function GetSites(workshopNumber)
{
    var result = null;

    $.ajax
        ({
            "async": false,
            "crossDomain": true,
            "url": CommonServicesUrl + "Site?$filter=WorkshopNumber eq " + workshopNumber,
            "method": "GET",
            "dataType": "json",
            "headers": {
                "Content-Type": "application/json",
                "X-XSRF-Token": X_XSRF_Token
            }
        })
    .done(function (jqXHRData) { result = toolGetSuccessData(jqXHRData); })
    .fail(function (jqXHRData) { result = toolGetErrorData(jqXHRData); });

    return result;
}

function GetMachineStatuses(workshopNumber, siteNumber) {
    var result = null;

    $.ajax
        ({
            "async": false,
            "crossDomain": true,
            "url": CommonServicesUrl + "MachineStatusValue?$filter=WorkshopNumber eq " + workshopNumber + " and SiteNumber eq " + siteNumber,
            "method": "GET",
            "dataType": "json",
            "headers": {
                "Content-Type": "application/json",
                "X-XSRF-Token": X_XSRF_Token
            }
        })
        .done(function (jqXHRData) { result = toolGetSuccessData(jqXHRData); })
        .fail(function (jqXHRData) { result = toolGetErrorData(jqXHRData); });

    return result;
}

function GetMachinePlace( workshopNumber, siteNumber, placeNumber)
{
    var result = null;

    $.ajax
        ({
            "async": false,
            "crossDomain": true,
            "url": CommonServicesUrl + "vwMachinePlace?$filter=WorkshopNumber eq " + workshopNumber + " and SiteNumber eq " + siteNumber + " and Number eq " + placeNumber,
            "method": "GET",
            "dataType": "json",
            "headers": {
                "Content-Type": "application/json",
                "X-XSRF-Token": X_XSRF_Token
            }
        })
        .done(function (jqXHRData) { result = toolGetSuccessData(jqXHRData); })
        .fail(function (jqXHRData) { result = toolGetErrorData(jqXHRData); });

    return result;
}


function PostCrashRequestLog(crashRequestData)
{
    var oDataStr = JSON.stringify(crashRequestData);
    var result = null;

    $.ajax
        ({
            "async": false,
            "crossDomain": true,
            "url": CommonServicesUrl + "CrashRequestLog",
            "method": "POST",
            "headers": {
                "Content-Type": "application/json",
                "cache-control": "no-cache",
                "X-XSRF-Token": X_XSRF_Token
            }
            , "data": oDataStr
        })
        .done(function (jqXHRData) { result = toolGetSuccessData(jqXHRData); })
        .fail(function (jqXHRData) { result = toolGetErrorData(jqXHRData); });

    return result;
}

function PostPassLeanLog(passLeanData)
{
    var oDataStr = JSON.stringify(passLeanData);
    var result = null;

    $.ajax
    ({
        "async": false,
        "crossDomain": true,
        "url": CommonServicesUrl + "PassLeanLog",
        "method": "POST",
        "headers": {
            "Content-Type": "application/json",
            "cache-control": "no-cache",
            "X-XSRF-Token": X_XSRF_Token
        }
        , "data": oDataStr
    })
        .done(function (jqXHRData) { result = toolGetSuccessData(jqXHRData); })
        .fail(function (jqXHRData) { result = toolGetErrorData(jqXHRData); });

    return result;
}