// отключает CORS в хроме
// --disable-web-security --user-data-dir="c:/PlantKioskJSClient"

var X_XSRF_Token = '';

function toolGetSuccessData(jqXHRData) {
    return jqXHRData;
}

function toolGetErrorData(jqXHRData) {

    var err_str = jqXHRData.url + " -> " + jqXHRData.statusText + " " + jqXHRData.responseText;
    console.log(err_str);

    return null;
}

var oDataServiceClient_IsInitialized = false;
function oDataServiceClient_CheckAndInit()
{
    if (oDataServiceClient_IsInitialized == false)
    {
        $.ajaxSetup({
            headers: {
                'Authorization': "Basic " + btoa(oDataServiceClient_USERNAME + ":" + oDataServiceClient_PASSWORD)
            }
        });

        oDataServiceClient_IsInitialized = true;
    }
}

// ---------------------------------------------------------------------------------------------------------------

function oDataServiceClient_GetWorkshops()
{

    var reqResult = null;

    $.ajax
    ({
        "async": false,
        "url": CommonServicesUrl + "ZWORKSHOPALL_SRV_01/zworkshopallSet?$format=json",  
        "method": "GET",
        "dataType": "json",
        "headers": {
            "Content-Type": "application/json"
        }
    })
        .done(function (jqXHRData) { reqResult = toolGetSuccessData(jqXHRData); })
        .fail(function (jqXHRData) { reqResult = toolGetErrorData(jqXHRData); });


    // конвертируем ответ
    var result = Array();
    var reqData = reqResult.d.results; 
    for(var key in reqData)
    {
	item = reqData[key];

	var resItem = 
	{	
	    Number: item.Numb,
	    Name: item.Name,
            StatusId: item.Statusid
        }

	result.push(resItem);
    }

    return result;
}

function oDataServiceClient_GetDirections()
{

    var reqResult = null;

    $.ajax
    ({
        "async": false,
        "url": CommonServicesUrl + "ZDIRECTION_SRV/zdirectionSet?$format=json",  
        "method": "GET",
        "dataType": "json",
        "headers": {
            "Content-Type": "application/json"
        }
    })
        .done(function (jqXHRData) { reqResult = toolGetSuccessData(jqXHRData); })
        .fail(function (jqXHRData) { reqResult = toolGetErrorData(jqXHRData); });


    // конвертируем ответ
    var result = Array();
    var reqData = reqResult.d.results; 
    for(var key in reqData)
    {
	item = reqData[key];

	var resItem = 
	{	
	    Id: item.Direction,
	    Name: item.Name
        }

	result.push(resItem);
    }

    return result;
}

function oDataServiceClient_GetSites(workshopNumber)
{

    var reqResult = null;

    $.ajax
        ({
            "async": false,
            "url": CommonServicesUrl + "ZSITEALL_SRV_01/zsiteallSet?$filter=Workshopnumber eq " 
                       + workshopNumber +"&$format=json",
            "method": "GET",
            "dataType": "json",
            "headers": {
                "Content-Type": "application/json"
            }
        })
    .done(function (jqXHRData) { reqResult = toolGetSuccessData(jqXHRData); })
    .fail(function (jqXHRData) { reqResult = toolGetErrorData(jqXHRData); });


    // конвертируем ответ
    var result = Array();
    var reqData = reqResult.d.results; 
    for(var key in reqData)
    {
	item = reqData[key];

	var resItem = 
	{	
            WorkshopNumber: item.Workshopnumber,
	    Number: item.Numb,
	    Name: item.Name,
            StatusId: item.Statusid
        }

	result.push(resItem);
    }


    return result;
}

function oDataServiceClient_GetMachineStatuses1(workshopNumber)
{

    var reqResult = null;

    $.ajax
        ({
            "async": false,

            "url": CommonServicesUrl + "ZMACHSTATZ_SRV_02/zmachstatzSet?$filter=Workshopnumber eq "  
                + workshopNumber + "&$format=json",
            "method": "GET",
            "dataType": "json",
            "headers": {
                "Content-Type": "application/json"
            }
        })
        .done(function (jqXHRData) { reqResult = toolGetSuccessData(jqXHRData); })
        .fail(function (jqXHRData) { reqResult = toolGetErrorData(jqXHRData); });

    // конвертируем ответ
    var result = Array();
    var reqData = reqResult.d.results; 
    for(var key in reqData)
    {
	item = reqData[key];

	if( item.Iscritical==1 ) item.Iscritical = true;
                            else item.Iscritical = false; 

	var resItem = 
	{	
            WorkshopNumber: item.Workshopnumber,
	    SiteNumber: item.Sitenumber,
	    Number: item.Numb,
	    IsCritical: item.Iscritical,
            StatusId: item.Statusid
        }

	result.push(resItem);
    }

    return result;
}

function oDataServiceClient_GetMachineStatuses2(workshopNumber, siteNumber)
{

    var reqResult = null;

    $.ajax
        ({
            "async": false,

            "url": CommonServicesUrl + "ZMACHSTAT_SRV_01/zmachstatSet?$filter=Workshopnumber eq " + workshopNumber  
               + " and Sitenumber eq " + siteNumber + "&$format=json",
            "method": "GET",
            "dataType": "json",
            "headers": {
                "Content-Type": "application/json"
            }
        })
        .done(function (jqXHRData) { reqResult = toolGetSuccessData(jqXHRData); })
        .fail(function (jqXHRData) { reqResult = toolGetErrorData(jqXHRData); });

    // конвертируем ответ
    var result = Array();
    var reqData = reqResult.d.results; 
    for(var key in reqData)
    {
	item = reqData[key];

	if( item.Iscritical==1 ) item.Iscritical = true;
                            else item.Iscritical = false; 

	var resItem = 
	{	
            WorkshopNumber: item.Workshopnumber,
	    SiteNumber: item.Sitenumber,
	    Number: item.Numb,
	    IsCritical: item.Iscritical,
            StatusId: item.Statusid
        }

	result.push(resItem);
    }

    return result;
}


function oDataServiceClient_GetMachinePlace( workshopNumber, siteNumber, placeNumber, kioskNumber)
{
    var reqResult = null;

    $.ajax
        ({
            "async": false,

            "url": CommonServicesUrl + "ZMACHINE_SRV_07/ZMachineSet(Numb=" + placeNumber + ",Sitenumber="  
                 + siteNumber + ",Workshopnumber=" + workshopNumber + ",Kiosknumber=" + kioskNumber + ")?$format=json",

            "method": "GET",
            "dataType": "json",
            "headers": {
                "Content-Type": "application/json"
            }
        })
        .done(function (jqXHRData) { reqResult = toolGetSuccessData(jqXHRData); })
        .fail(function (jqXHRData) { reqResult = toolGetErrorData(jqXHRData); });

    // конвертируем ответ
    var item = reqResult.d; 

    if( item.Iscritical==1 ) item.Iscritical = true;
                        else item.Iscritical = false; 

    var resItem = 
    {	
	WorkshopNumber: item.Workshopnumber,
	SiteNumber: item.Sitenumber,
	Number: item.Numb,
	CurrMachineInvNum: item.Inventorynumber,
	TypeName: item.Name,
	Model: item.Model,
	IsCritical: item.Iscritical,
	ProductionDate: item.Productiondate,
	StatusId: item.Statusid
    }
    var result = Array();
    result.push(resItem);

    return result;
}
// -----------

function oDataServiceClient_PostCrashRequestLog(crashRequestData, workshopNumber, siteNumber, placeNumber)
{
// for post request --------
//    var requestData = 
//    {
//        d : 
//        {
//            Numb : placeNumber,
//	    SiteNumber : siteNumber, 
//            WorkshopNumber : workshopNumber, 
//            Kiosknumber : crashRequestData.KioskNumber,
//            Newstatusid : crashRequestData.NewStatusId,
//            Message : crashRequestData.Message,
//            Direction: crashRequestData.Direction
//            Error : 0
//        } 
//    };
//    var oDataStr = JSON.stringify(requestData);
// for post request --------

    var result = null;

    $.ajax
        ({
            "async": false,

// for get request --------
            "url": CommonServicesUrl + "ZCRASHREQ_SRV_02/zcrashreqSet(Numb=" + placeNumber + ",Sitenumber=" + siteNumber      
            + ",Workshopnumber=" + workshopNumber + ",Kiosknumber=" + crashRequestData.KioskNumber  
            + ",Newstatusid=" + crashRequestData.NewStatusId + ",Message='" + encodeURIComponent(crashRequestData.Message) 
                                                                      + "',Direction=" + crashRequestData.Direction
            + ")?$format=json",
            "method": "GET",
// for get request --------

// for post request --------
//            "url": CommonServicesUrl + "ZCRASHREQPOST_SRV/zcrashreqpostSet",
//            "method": "POST",
//            "data": oDataStr,
// for post request --------

	    "headers": {
	        "Content-Type": "application/json",
	        "cache-control": "no-cache"
	    }

        })
        .done(function (jqXHRData) { result = toolGetSuccessData(jqXHRData); })
        .fail(function (jqXHRData) { result = toolGetErrorData(jqXHRData); });

    return result;
}

function oDataServiceClient_PostPassLeanLog(passLeanData)
{
    var result = null;

// for post request --------
//    var requestData =   
//    {
//	d : {
//	    Kiosknumber : passLeanData.KioskNumber,
//	    Passnumber : passLeanData.PassNumber,
//            Idpernr : 0
//	}
//    };
//    var requestDataStr = JSON.stringify(requestData);    
// for post request --------

    $.ajax
    ({
        "async": false,
        "crossDomain": true,

// for post request --------
//        "url": CommonServicesUrl + "ZPASSLEANLOGPOST_SRV/zpassleanlogpostSet",
//        "method": "POST",
//        "dataType": 'json',
//	  "data": requestDataStr
// for post request --------

// for get request --------
        "url": CommonServicesUrl + "ZPASSLEANLOG_SRV_02/zpassleanlogSet"              
            + "(Kiosknumber=" + passLeanData.KioskNumber 
            + ",Passnumber=\'" + encodeURIComponent(passLeanData.PassNumber)
	    +"\')?$format=json",
        "method": "GET",
// for get request --------

        "headers": {
            "Content-Type": "application/json",
            "cache-control": "no-cache"
        },
    })
    .done(function (jqXHRData) { result = toolGetSuccessData(jqXHRData); })
    .fail(function (jqXHRData) { result = toolGetErrorData(jqXHRData); });

    return result;
}


function oDataServiceClient_GetPassLeanLog(kioskNumber, workshopNumber)
{
    var reqResult = null;

    $.ajax
        ({
            "async": false,
            "crossDomain": true,
            "url": CommonServicesUrl + "ZCHECKLOG_SRV_01/ZCHECKLOGSet(Kiosknumber=" + kioskNumber + ",Workshopnumber=" + workshopNumber + ")?$format=json",   
            "method": "GET",
            "dataType": "json",
            "headers": {
                "Content-Type": "application/json"
            }
        })
    .done(function (jqXHRData) { reqResult = toolGetSuccessData(jqXHRData); })
    .fail(function (jqXHRData) { reqResult = toolGetErrorData(jqXHRData); });

    // конвертируем ответ
    var item = reqResult.d; 
    var resItem = 
    {	
        KioskNumber: item.Kiosknumber,
        IDPERNR: item.Idpernr,
        IsConfirmed: item.Isconfirmed,
	ZResult: item.Zresult
    }

    var result = Array();
    result.push(resItem);

    return result;
}