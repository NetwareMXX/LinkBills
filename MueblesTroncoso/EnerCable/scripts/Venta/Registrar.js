var myWindow;
var myWindow2;
$(document).ready(function () {
    myWindow = $("#window");
    myWindow.kendoWindow({
        width: $(".container-fluid").width(),
        title: "Facturas",
        visible: false,
        actions: [
            "Pin",
            "Minimize",
            "Maximize",
            "Close"
        ],
        close: onClose
        //close: onClose
    });//.data("kendoWindow").center().open();
    myWindow2 = $("#window2");
    myWindow2.kendoWindow({
        width: 500,
        title: "Correcciones",
        visible: false,
        actions: [
            "Pin",
            "Minimize",
            "Maximize",
            "Close"
        ],
        close: onClose2
        //close: onClose
    });//.data("kendoWindow").center().open();
});
function onClose(e) {

//    limpiarPop();
}
function onClose2(e) {

    //    limpiarPop();
}
datosCaptura = {
    /*txtFechaInicio: "",
    txtFechaFin: "",
    txtFolioPedido: ""
    */
};
datosGrid = {

    Usuario: { type: "string" },
    Ip: { type: "string" },
    Accion: { type: "string" },
    FechaSistema: { type: "string" }
};
_accionGrid = "/Bitacora/obtenerBitacora";
_accion = "";

datosInsercion = [
        
];
var _estatus = new Object();
datosAccion = {
    "estatus": _estatus
};
columnasGrid = [
                 {
                     field: "Usuario",
                     title: "Usuario"

                 },
                 {
                     field: "Ip",
                     title: "Ip"
                 },
                 {
                     field: "Accion",
                     title: "Accion"
                 },
                 {
                     field: "FechaSistema",
                     title: "FechaSistema"
                 }

];

function recuperarDatos() {
    _persona.IdEstatus = $("#hdIdEstatus").val();
    _persona.Estatus1 = $("#txtEstatus").val();


}
function llenarDatos(dataItem) {
    $("#hdIdEstatus").val(dataItem.IdEstatus);
    $("#txtEstatus").val(dataItem.Estatus1);

}
function checkTodo(ch) {
    var c = ch.checked;
    $('.chkPer').prop('checked', c);
}