
var workshopsServerData = null;
function plant_LoadWorkshopDataFromODataServer()
{
    workshopsServerData = oDataServiceClient_GetWorkshops();
}

var maxWorkshopNumber = 50;
function plant_CalculateMaxWorkshopNumber()
{
    for (var key in workshopsServerData) {
        var value = workshopsServerData[key];
        if (value.Number > maxWorkshopNumber) {
            maxWorkshopNumber = value.Number;
        }
    }
}

var usefulStatusesStorage = new Array();
var usefulNamesStorage = new Array();
function plant_fillUsefulStatusesStorage()
{
    for (var i = 1; i <= maxWorkshopNumber; i++) {
        usefulStatusesStorage[i] = 5; // Не показывать
        usefulNamesStorage[i] = "";
    }

    for (var key in workshopsServerData) {
        var value = workshopsServerData[key];
        usefulStatusesStorage[value.Number] = value.StatusId; // для существующих цехов
        usefulNamesStorage[value.Number] = value.Name;
    }
}

function plant_BrowserWidth2PartOfBtnStyles(buttonElement)
{   
    //if (common_IsWidthMoreThen2000 == false) {
        buttonElement.className = 'btn workshop_button_1000'; // в любом случае !!! иначе проблемы с чертежом
    //} else {
    //    buttonElement.className = 'btn workshop_button_2000';
    //}
}

function plant_Status2PartOfBtnStyles(buttonElement, currStatus)
{
    switch (currStatus) {
        case 1:
            buttonElement.className += ' btn-default active'; // для любых существующих цехов
            break;
        //    buttonElement.className += ' btn-success active';
        //    break;
        //case 2:
        //    buttonElement.className += ' btn-warning active';
        //    break;
        //case 3:
        //    buttonElement.className += ' btn-danger active';
        //    break;
        case 4:
            buttonElement.className += ' btn-default disabled'; // На консервации/Не реализовано 
                                                                // видно в списке цехов, но страница цеха не реализована, перехода нет
            break;
        default: // остальное не отображается
            break;
    }
}

function plant_Status2BtnOnClick(buttonElement, currNumber, currName, currStatus)
{
    if (currStatus == 1 || currStatus == 2 || currStatus == 3) // только для поддерживаемых статусов и не 4 (Консервация/Не реализовано)
    {
        buttonElement.setAttribute(
            'onclick',
            'plant_ClickOnWorkshop(' + currNumber + ', \'' + currName + '\')'
        );
    } 
}

function plant_DrawWorkshops()
{
    var buttonsContainer = $('#plantContainer')[0];

    for (var currWrkNumber = 1; currWrkNumber <= maxWorkshopNumber; currWrkNumber++)
    {        
        var currWrkName = usefulNamesStorage[currWrkNumber];
        var currWrkStatus = usefulStatusesStorage[currWrkNumber];

        if (currWrkStatus == 1 || currWrkStatus == 4)
        {
            var currWrkName = usefulNamesStorage[currWrkNumber];

            var currButtonContainer = document.createElement("DIV");
            currButtonContainer.className += " workshop_button_cont_1000";

            var currButton = document.createElement("BUTTON");
            currButton.type = "button";
            currButton.innerText = currWrkName;
            plant_BrowserWidth2PartOfBtnStyles(currButton);
            plant_Status2PartOfBtnStyles(currButton, currWrkStatus);
            plant_Status2BtnOnClick(currButton, currWrkNumber, currWrkName, currWrkStatus);

            currButtonContainer.appendChild(currButton);
            buttonsContainer.appendChild(currButtonContainer);
        }
    }    
}

function plant_ClickOnWorkshop(number, name) {
    var uriName = encodeURI(name);
    window.location.href = "Workshop.html?Number=" + number + "&Name=" + uriName;
}

