var ReqRes_currFunc = null;

function ReqRes_CloseMessageBoxAndFun()
{
    if ( ReqRes_ShowMessageBox_Timer!=null )
	clearTimeout( common_Redirection2IndexHtml_Timer ); 

    $('#msgBoxWindow').modal('hide');
    ReqRes_currFunc();
}


var ReqRes_ShowMessageBox_Timer = null;
function ReqRes_ShowMessageBoxAndFunc( reqRes_Id, zResult, positiveFunc, negativeFunc )
{     
    var addClass = ( zResult>=0 ) ? "alert alert-success" : "alert alert-danger";
    var caption =  ( zResult>=0 ) ? "Оповещение:" : "Ошибка:";
    var message = ReqRes_Data[reqRes_Id][zResult];
    ReqRes_currFunc = ( zResult>=0 ) ? positiveFunc : negativeFunc;

    $('#msgBoxWindowHeader').addClass(addClass);
    $('#msgBoxWindowTitle').html(caption);
    $('#msgBoxWindowMessage').html(message);
    $('#msgBoxWindowCloseButton').on( "click", ReqRes_CloseMessageBoxAndFun );

    ReqRes_ShowMessageBox_Timer = setTimeout(ReqRes_CloseMessageBoxAndFun, 5000); // автоматическое закрывание через 5 секунд

    $('#msgBoxWindow').modal('show'); 
}