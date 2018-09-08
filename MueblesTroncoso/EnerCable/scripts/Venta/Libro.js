
document.addEventListener('DOMContentLoaded', function (event) {
    let view = new Vue({
        el: document.getElementById('view'),
        mounted: function () { },
       // data: datosCaptura,

        methods:
        {
            eliminar: Eliminar
            , guardar: Guardar
            , load: Load
            //, cargar: loadGrid
            , actualizar: Actualizar
            , tooglePanel: mostrarCaptura
            , cancelar: cancelar
        }
    });

    //.cargar();
    view.load();
});

function Eliminar() { }
function Guardar() {

    
    var date = new Date();
    var mili = date.getMilliseconds();
    var second = date.getSeconds();
    var minute = date.getMinutes();
    var hour = date.getHours();
    var day = date.getDate();
    var month = date.getMonth();
    var year = date.getFullYear();
    


    var _idmes = $("#ddlMes").val();
    var _idusuario = $("#hdIdUsuario").val();
    var _adx = ([mili, second, minute, hour, day, month, year].join(""));
    $("#Contenido").attr("src", "http://172.10.1.1/ReporteLibro/reporte.aspx?asasdasda="+_idmes+"&asdasdaasfv="+_idusuario+"&idx="+_adx);
    $("#aPanel").click();


    return false;

}
function Load() {
    $('.combo').selectpicker();
  

    $("#aPanel").click(function () {
        if ($("#aPanel").hasClass("collapsed")) {
            $('a#aCapturar').html('<i class="material-icons">screen_lock_rotation</i>Ocultar');
        }
        else {
            $('a#aCapturar').html('<i class="material-icons">screen_rotation</i>Capturar');


        }
    });
    for (var j = 0; j < datosInsercion.length; j++) {
        if (datosInsercion[j].tipoControl === "Texto") {


            $("#" + datosInsercion[j].control).val(datosInsercion[j].defaultVal);
        }
        else if (datosInsercion[j].tipoControl === "Combo") {
            $("#" + datosInsercion[j].control).selectpicker("val", datosInsercion[j].defaultVal);
        }
    }


}
function Actualizar() { }
function mostrarCaptura() {

    $("#aPanel").click();
    if ($("#aPanel").hasClass("collapsed")) {
        $('a#aCapturar').html('<i class="material-icons">screen_rotation</i>Capturar');
    }
    else {

        $('a#aCapturar').html('<i class="material-icons">screen_lock_rotation</i>Ocultar');

    }
}
function Validar() {


    var _html = "";
    for (var j = 0; j < datosInsercion.length; j++) {

        if (datosInsercion[j].requerido === 1) {

            if ($("#" + datosInsercion[j].control).val() == "") {

                _html += "  *" + datosInsercion[j].nombre + " ";
            }
        }
    }
    return _html;
}
function cancelar() {

    $nuevo = 1;

    for (var j = 0; j < datosInsercion.length; j++) {
        if (datosInsercion[j].tipoControl === "Texto") {

            $("#" + datosInsercion[j].control).val(datosInsercion[j].defaultVal);
        }
        else if (datosInsercion[j].tipoControl === "Combo") {
            $("#" + datosInsercion[j].control).selectpicker("val", datosInsercion[j].defaultVal);
        }
    }




    var grid = $("#miGrid").data("kendoGrid");
    grid.clearSelection();
    $("#aPanel").click();
    if ($("#aPanel").hasClass("collapsed")) {
        $('a#aCapturar').html('<i class="material-icons">screen_rotation</i>Capturar');
    }
    else {

        $('a#aCapturar').html('<i class="material-icons">screen_lock_rotation</i>Ocultar');

    }
}



