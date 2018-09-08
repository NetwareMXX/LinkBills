
datosCaptura = {
    txtFechaInicio: "",
    txtFechaFin: "",
    txtFolioPedido: ""

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