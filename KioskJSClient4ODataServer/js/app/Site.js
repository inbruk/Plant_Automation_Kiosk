var workshopNumber = null;
var workshopName = null;
var siteNumber = null;
var siteName = null;
function site_GetWorkshopAndNumberFromRequestParameter()
{
    var urlParams = new URLSearchParams(window.location.search);
    workshopNumber = urlParams.get('WorkshopNumber');
    workshopName = urlParams.get('WorkshopName');
    siteNumber = urlParams.get('SiteNumber');
    siteName = decodeURI(urlParams.get('SiteName'));
}

var error_directions = null;
function site_LoadAvailableDirections()
{
    error_directions = oDataServiceClient_GetDirections();
}

function site_DrawCaption()
{
    var workshopCaptElem = $('#workshopSelectionText')[0];
    var siteCaptElem = $('#siteSelectionText')[0];
    workshopCaptElem.innerText = workshopName;
    siteCaptElem.innerText = siteName;
}

function site_DrawBackgroundDrawing()
{
    var bkgDrawing = $('#bkgDrawing')[0];
    var placeContainer = $('#placeContainer')[0];

    bkgDrawing.setAttribute('src', 'data/drawings/wn' + workshopNumber + '/sn' + siteNumber + '.svg');

    //if (common_IsWidthMoreThen2000 == false) {
        bkgDrawing.className += ' item_dims_1000';        // в любом случае !!! иначе проблемы с чертежом
        placeContainer.className += ' item_dims_1000';
    //} else {
    //    ; //add there style for > 2000 width
    //}
}

var machineStatusesByNumber = new Array();
var machineIsCriticalByNumber = new Array();
function site_LoadMachineStatusesFromODataService()
{
    tempData = oDataServiceClient_GetMachineStatuses2(workshopNumber, siteNumber);

    var maxMachineNumber = 0;
    for (var key in tempData) {
        var item = tempData[key];
        if (item.Number > maxMachineNumber)
            maxMachineNumber = item.Number;
    }

    for (var i = 1; i <= maxMachineNumber; i++) {
        machineStatusesByNumber[i] = 4;  // не задан/на консервации
        machineIsCriticalByNumber[i] = false;
    }

    for (var key2 in tempData) {
        var item2 = tempData[key2];
        machineStatusesByNumber[item2.Number] = item2.StatusId;
        machineIsCriticalByNumber[item2.Number] = item2.IsCritical;
    }
}

var placeCoordsData = null;
function site_LoadPlaceCoordsFromJSONFile()
{
    var fileData = common_LoadJsonFromWebServer(
        'data/metadata/wn',
        workshopNumber,
        '/place_coords_sn' + siteNumber + '.json',
        'о местах станков по номеру цеха и номеру участка.'
    );

    placeCoordsData = fileData.Places;
}

function site_DrawPlaceStatusesMarkers()
{
    var container_element = $('#placeContainer')[0];

    for (var key in placeCoordsData)
    {
        var placeCoord = placeCoordsData[key];
        var number = placeCoord.Number;
        var status = machineStatusesByNumber[number];
        var isCritical = machineIsCriticalByNumber[number];       
        var classes = 'circ_place_on_site ' + common_Status2AddClass(status);
        if (isCritical == true) { classes += ' critical_machine'}

	var x_pcnt = placeCoord.XCoord;
        var y_pcnt = placeCoord.YCoord;
        var circle = common_CreateCircle(x_pcnt, y_pcnt, classes);
        if( status==1 || status==2 )
	{
            circle.setAttribute(
	        'onclick',
	        'site_ClickOnMachineCircle(' + number + ')'
	    );
	}
        container_element.appendChild(circle);
    }
}

var placeNumber = null;
var machinePlaceData = null;
var tabelWorkerNumber = null;
var checkPassLeanCount = 0; // количество проверок, чтобы не проверять вечно
function site_ClickOnMachineCircle( machineNumber )
{
    placeNumber = machineNumber;

    // грузим данные по станку
    machinePlaceData = oDataServiceClient_GetMachinePlace(workshopNumber, siteNumber, machineNumber, kioskNumber)[0];

    // переключаем панели внизу окна
    var mdlLeanPassPanel = $('#mdlLeanPassPanel')[0];
    mdlLeanPassPanel.className = 'row central';

    var mdlMessagePanel = $('#mdlMessagePanel')[0];
    mdlMessagePanel.className = 'row hide-it';

    // заполняем поля верху окна
    var mdlModel = $('#mdlModel')[0];
    mdlModel.innerText = machinePlaceData.Model;

    var mdlName = $('#mdlName')[0];
    mdlName.innerText = machinePlaceData.TypeName;

    var mdlDate = $('#mdlDate')[0];
    var dateStr = machinePlaceData.ProductionDate + '';
//    var prodDate = new Date(prodDateStr);
//    var dateStr = prodDate.getDate() + '.' + prodDate.getMonth() + '.' + prodDate.getFullYear() + ' '
//        + prodDate.getHours() + ':' + prodDate.getMinutes() + ':' + prodDate.getSeconds();
    mdlDate.innerText = dateStr;
    
    var mdlInvNumber = $('#mdlInvNumber')[0];
    mdlInvNumber.innerText = machinePlaceData.CurrMachineInvNum;   

    // показываем окно
    $('#mwndMessage').modal('show');

    // отключаем таймаут автоматического перехода на стартовую страницу, чтобы не было паралельных таймаутов не нужных
    common_DisableRedirection2IndexHtml(); 
    
    // запустить таймаут с запросом приложен ли пропуск
    checkPassLeanCount = 0;
    site_RunTimeoutForPassLeanChecking();

    // нужно, не забыть отключить таймаут в случае если окно скрывается/закрывается пользователем
    $('#mwndMessage').unbind('hide.bs.modal');
    $('#mwndMessage').on('hide.bs.modal',
	function(event)
	{
	    clearTimeout( site_CheckForPassLean_Timer ); // перестраховка перед релоадом страницы
            site_RefreshPage(); // перезапустим страницу, чтобы все заново переустановить нормально
	}
    );  
}

var site_CheckForPassLean_Timer = null;
function site_RunTimeoutForPassLeanChecking()
{
    site_CheckForPassLean_Timer = setTimeout(site_CheckForPassLean, 5000); // проверка раз в 5 секунд  
}

function site_ShowMessageBoxAndFunc(zResult, positiveFunc, negativeFunc)
{
    // перестраховка, по идее сюда попадаем, только когда уже отключен таймаут
    if( site_CheckForPassLean_Timer!=null ) 
	clearTimeout( site_CheckForPassLean_Timer );

    ReqRes_ShowMessageBoxAndFunc( ReqRes_Id_Site, zResult, positiveFunc, negativeFunc );
}

function site_RefreshPage() {
    location.reload();
}

function site_GoHomePage() {
    window.location.href = "Index.html";
}

function site_CheckForPassLean()
{
    var response = oDataServiceClient_GetPassLeanLog(kioskNumber,workshopNumber);
    var responseLength = response.length;

    // если еще ничего не прикладывали (старый и новый варианты проверки)
    // если ничего не прикладывали продолжаем ждать, ничего не выводя
    if (responseLength == 0 || response[0].ZResult==-4 ) { 

        tabelWorkerNumber = null;
        checkPassLeanCount++;

        // если не приложили пропуск минуту, то 
        if (checkPassLeanCount > 11) {
            site_PassLeanNotReceivedForMinute();
        }

        // подождем еще
        site_RunTimeoutForPassLeanChecking();

    } else {

        var passLeanItem = response[0];
	var zResult = passLeanItem.ZResult;        

        if ( zResult>=0 ) // сообщать о неисправности разрешено, так как правильный пропуск был приложен
	{            
            // никаких сообщений не выводим, переходим к отсылке 
            tabelWorkerNumber = passLeanItem.IDPERNR;
            site_PassLeanReceived();
	}
        else // на любую ошибку, кроме -4, выводим мессадж бокс с ошибкой
        {
            site_ShowMessageBoxAndFunc(zResult, function() { ; } , site_RefreshPage );
        }
    }
}
	
function site_PassLeanNotReceivedForMinute() 
{
    // если долго ждали выводим ошибку с тем, что не прикладывали пропуск
    site_ShowMessageBoxAndFunc(-4, function() { ; } , site_RefreshPage );
}

function site_FillErrorDirectionsDropDownList()
{
    var sel = $('#selDirections');

    for(var i=0; i<error_directions.length; i++)
    {
       var item = error_directions[i];
		sel.append("<option value='"+ item.Id + "'>" + item.Name + "</option>");
    }
}

function site_PassLeanReceived()
{
    // переключаем панели внизу окна
    var mdlLeanPassPanel = $('#mdlLeanPassPanel')[0];
    mdlLeanPassPanel.className = 'row central hide-it';

    var mdlMessagePanel = $('#mdlMessagePanel')[0];
    mdlMessagePanel.className = 'row';

    // заполняем поле внизу окна
    var mdlTabNumber = $('#mdlTabNumber')[0];
    mdlTabNumber.innerText = tabelWorkerNumber;

    // заполняем выпадающий список с направлениями
    site_FillErrorDirectionsDropDownList();

    // и ждем заполнения поля сообщения и нажатия не кнопку
}

function site_SendRequestAndGoIndex(newStatusId) {
    var mdlMessageTArea = $('#mdlMessageTArea')[0];
    var message = mdlMessageTArea.value;
    var currDirValue = $('#selDirections').val();

    var oData =
    {
        MachineInventoryNumber: machinePlaceData.CurrMachineInvNum,
        NewStatusId: newStatusId,
        KioskNumber: kioskNumber, // берется из config.js
        Message: message,
	Direction: currDirValue,
        Time: new Date()
    };

    var reqResult = oDataServiceClient_PostCrashRequestLog(oData, workshopNumber, siteNumber, placeNumber );

    // сообщаем о результате запароса пользователю и переходим на основную страницу (в любом случае)
    site_ShowMessageBoxAndFunc(reqResult.d.Zresult, site_RefreshPage, site_RefreshPage);
}

function site_ClickBreakage()
{
    site_SendRequestAndGoIndex(2); // 2 - Неисправность
}

function site_ClickCrash()
{
    site_SendRequestAndGoIndex(3); // 3 - Авария
}


