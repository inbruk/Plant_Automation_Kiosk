
var parKioskNumber = null;

try {
    // предполагаем, что 1-й параметр KioskNumber
    var params = window.location.href.split('?');
    var param1 = params[1];
    var param1Arr = param1.split('=');
    var name = param1Arr[0];
    if (name == 'KioskNumber') {
        parKioskNumber = param1Arr[1]; // value
    }
} catch {}

if ( parKioskNumber != null ) {
    kioskNumber = parKioskNumber;
    $.cookie( 'KioskNumber', kioskNumber );
} else {
    if (kioskNumber == null) {
        alert("Ошибка: KioskNumber не задан, ни как параметр, ни как COOKIE. Для исправления всегда запускайте " +
            "стратовую страницу с первым параметром KioskNumber, например так: Index.html?KioskNumber=1");
    }
}
