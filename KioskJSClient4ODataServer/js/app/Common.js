
function common_GetBrowserWidth() {
    if (self.innerWidth) {
        return self.innerWidth;
    }
    else if (document.documentElement && document.documentElement.clientHeight) {
        return document.documentElement.clientWidth;
    }
    else if (document.body) {
        return document.body.clientWidth;
    }
    return 0;
}

var common_BrowserWidth = null;
var common_IsWidthMoreThen2000 = false;
// calculate global variables
common_BrowserWidth = common_GetBrowserWidth();
if (common_BrowserWidth > 1800) {
    common_IsWidthMoreThen2000 = true;
}

function common_LoadJsonFromWebServer(jsonFileName_1stPart, jsonFileName_2stPart, jsonFileName_3stPart, error_message)
{
    var result = null;
    var filename = jsonFileName_1stPart + jsonFileName_2stPart + jsonFileName_3stPart;

    $.ajax({ // $.getJSON
        dataType: "json",
        url: filename,
        async: false
    })
    .done(function (json_data) {
        result = json_data;
    })
    .fail(function (jqxhr, textStatus, error) {
        console.log(
            "Ошибка загрузки данных с веб сервера: " +
            error_message +
            " Error status/text: " + textStatus + ": " + error
        );
    });

    return result;
}

function common_Status2AddClass(status) {
    switch (status) {
        case 1: return 'status_normal_work';
        case 2: return 'status_breakage';
        case 3: return 'status_crash';
        default: return 'status_conservation';
    }
}

function common_CreateCircle(x_pcnt, y_pcnt, classes)
{
    var circle = document.createElement("div");

    circle.className = classes;
    circle.style.left = x_pcnt + '%';
    circle.style.top = y_pcnt + '%';

    return circle;
}

function common_DrawCircleOnContainer(container_element, x_pcnt, y_pcnt, classes)
{   
    var circle = common_CreateCircle(x_pcnt, y_pcnt, classes);
    container_element.appendChild(circle);
}

function common_Status2ColorInText(status) {
    //switch (status) {
    //    case 1: return '#10ff10';
    //    case 2: return '#ffff00';
    //    case 3: return '#ff1010';
    //    default: return '#aaaaaa';
    //}
    return '#f000f0'; // сейчас для любых статусов участков
}

var common_SVG_NameSpace = "http://www.w3.org/2000/svg";
function common_CreatePolylineInSVG(coords_xy, status, is_border)
{
    var color_text = common_Status2ColorInText(status);
    var polygon_element = document.createElementNS(common_SVG_NameSpace, 'polygon');

    if (is_border == true)
    {
        polygon_element.setAttributeNS(null, "fill", "none");
        polygon_element.setAttributeNS(null, "stroke", color_text);
        polygon_element.setAttributeNS(null, "stroke-width", "0.2");
    }
    else
    {
        polygon_element.setAttributeNS(null, "stroke", "none");
        polygon_element.setAttributeNS(null, "opacity", "0.0");
    }

    var coord_str = "";
    for (var key in coords_xy)
    {
        var curr_coord = coords_xy[key];
        coord_str += curr_coord.X + ',' + curr_coord.Y + ' ';
    }

    polygon_element.setAttributeNS(null, "points", coord_str);

    return polygon_element;
}

function common_Redirect2IndexHtml()
{
    window.location.href = "Index.html";
}

var common_Redirection2IndexHtml_Timer = null;
function common_ResetRedirection2IndexHtml()
{
    common_Redirection2IndexHtml_Timer = setTimeout( common_Redirect2IndexHtml, redirection2IndexHtmlTimout ); 
}
function common_DisableRedirection2IndexHtml()
{
    if( common_Redirection2IndexHtml_Timer!=null )
	clearTimeout( common_Redirection2IndexHtml_Timer ); 
}


function getFullMinutes(time) 
{
   var value = time.getMinutes();
   if (value < 10) {
       return '0' + value;
   }
   return '' + value;
};
