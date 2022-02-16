
var workshopNumber = null;
var workshopName = null;
function workshop_GetWorkshopNumberFromRequestParameter()
{
    var urlParams = new URLSearchParams(window.location.search);
    workshopNumber = urlParams.get('Number');
    workshopName = decodeURI(urlParams.get('Name'));
}

function workshop_DrawCaption()
{
    var elem = $('#workshopSelectionText')[0];
    elem.innerText = workshopName;
}

function workshop_DrawBackgroundDrawing()
{
    var bkgDrawing = $('#bkgDrawing')[0];
    var brdSites = $('#brdSites')[0];
    var placeContainer = $('#placeContainer')[0];

    bkgDrawing.setAttribute('src', 'data/drawings/wn' + workshopNumber + '/workshop.svg');

    //if (common_IsWidthMoreThen2000 == false) {
        bkgDrawing.className += ' item_dims_1000'; // в любом случае !!! иначе проблемы с чертежом
        placeContainer.className += ' item_dims_1000';
        brdSites.width = '1170px';
        brdSites.height = '900px';
    //} else {
    //    ; //add there style for > 2000 width
    //}
}

var machineStatusesArrayBySiteNumber = new Array();
function workshop_LoadMachineStatusesFromODataService()
{
    tempData = oDataServiceClient_GetMachineStatuses1(workshopNumber);

    var maxSiteNumber = 0;
    for (var key in tempData)
    {
        var item = tempData[key];
        if (item.SiteNumber > maxSiteNumber)
            maxSiteNumber = item.SiteNumber;
    }

    for (var i = 1; i <= maxSiteNumber; i++) {
        machineStatusesArrayBySiteNumber[i] = new Array();
        for (var key2 in tempData) {
            var value = tempData[key2];
            if (value.SiteNumber == i) {
                machineStatusesArrayBySiteNumber[i].push(value);
            }            
        }
    }  
}

var siteStatusesBySiteNumber = new Array();
var siteNameBySiteNumber = new Array();
function workshop_LoadSiteStatusesFromODataService()
{
    var tempData = oDataServiceClient_GetSites(workshopNumber);

    var maxSiteNumber = 0;
    for (var key in tempData)
    {
        var item = tempData[key];
        if (item.Number > maxSiteNumber)
            maxSiteNumber = item.Number;
    }

    for (var i = 1; i <= maxSiteNumber; i++) {
        siteStatusesBySiteNumber[i] = 4;  // не задан/на консервации
        siteNameBySiteNumber[i] = '';
    }

    for (var key2 in tempData)
    {
        var value = tempData[key2];
        siteStatusesBySiteNumber[value.Number] = 1; // value.StatusId; для любых существующих цехов
        siteNameBySiteNumber[value.Number] = value.Name;
    }
}

var placeCoordsData = null;
function workshop_LoadPlaceCoordsFromJSONFile()
{
    var fileData = common_LoadJsonFromWebServer(
        'data/metadata/wn',
        workshopNumber,
        '/place_coords.json',
        'о местах станков по номеру цеха.'
    );

    placeCoordsData = fileData.Places;
}

var siteBordersData = null;
function workshop_LoadSiteBordersFromJSONFile()
{
    var fileData = common_LoadJsonFromWebServer(
        'data/metadata/wn',
        workshopNumber,
        '/site_borders.json',
        'о границах участков по номеру цеха.'
    );

    siteBordersData = fileData.SiteBorders;
}

function workshop_GetPlaceStatus(siteNumber, number)
{ 
    var arrOfStatusesByNumber = machineStatusesArrayBySiteNumber[siteNumber];    

    for (var key in arrOfStatusesByNumber) {
        var item = arrOfStatusesByNumber[key];
        if (item.Number == number)
            return item;
    }

    return null;
}

function workshop_DrawPlaceStatusesMarkers()
{
    var container_element = $('#placeContainer')[0];

    for (var key in placeCoordsData)
    {
        var placeCoord = placeCoordsData[key];

        var siteNumber = placeCoord.SiteNumber;
        var number = placeCoord.Number;

        var item = workshop_GetPlaceStatus(siteNumber, number);
        var status = 4;  // не задан/на консервации
        var isCritical = false;
        if (item != null) {
            status = item.StatusId;
            isCritical = item.IsCritical;
        }
    // nikitushkin 07.02.2019 круг подложка для критического оборудования
    /*    if (isCritical == true) {
            var classes2 = 'circ_critical_place_on_workshop';
            var x_pcnt2 = placeCoord.XCoord - 0.2;
            var y_pcnt2 = placeCoord.YCoord - 0.2;
            common_DrawCircleOnContainer(container_element, x_pcnt2, y_pcnt2, classes2);
        } */
         // 08.02.2019 непрозрачность - стили с постфиксом _ws
        // var classes = 'circ_place_on_workshop ' + common_Status2AddClass(status); 
		var classes = 'circ_place_on_workshop ' + common_Status2AddClass(status) + '_ws';
		// nikitushkin 07.02.2019 для критического добавим стиль с каёмкой
        if (isCritical == true) { classes += ' critical_machine'; classes += ' critical_machine_border'	}	
        var x_pcnt = placeCoord.XCoord;
        var y_pcnt = placeCoord.YCoord;      
        common_DrawCircleOnContainer(container_element, x_pcnt, y_pcnt, classes);
    }              
}

function workshop_DrawSiteBordersMarkers()
{
    var svg_container = $('#brdSites')[0];

    for (var key in siteBordersData)
    {
        var item = siteBordersData[key];
        var status = siteStatusesBySiteNumber[item.SiteNumber];
        var siteName = siteNameBySiteNumber[item.SiteNumber];

        var polyElementBorder = common_CreatePolylineInSVG(item.XYCoords, status, true);
        svg_container.appendChild(polyElementBorder);

        var polyElementFill = common_CreatePolylineInSVG(item.XYCoords, status, false);
        polyElementFill.setAttribute(
            'onclick',
            'workshop_ClickOnSitePolygon(' + workshopNumber + ',\'' + workshopName + '\',' + item.SiteNumber + ',\'' + siteName + '\')'
        );
        svg_container.appendChild(polyElementFill);
    }
}

function workshop_ClickOnSitePolygon(workshopNumber, workshopName, siteNumber, siteName)
{
    var uriWorkshopName = encodeURI(workshopName);
    var uriSiteName = encodeURI(siteName);
    window.location.href = "Site.html?WorkshopNumber=" + workshopNumber +
        "&WorkshopName=" + uriWorkshopName + "&SiteNumber=" + siteNumber + "&SiteName=" + uriSiteName;
}
