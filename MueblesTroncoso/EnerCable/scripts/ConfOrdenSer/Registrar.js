//$(document).ready(function () {

//});
datosCaptura = {
    txtEstatus: "",


};
datosGrid = {
    
    Estatus: { type: "string" },
    IdEstatus: { type: "string" }
};
_accionGrid = "/ConfiguracionOrdenSer/ObtenerConf";
_accion = "/ConfiguracionOrdenSer/agregarConf/";

datosInsercion = [
        { control: "txtEstatus", requerido: 1, nombre: "Nombre del Estatus ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Estatus" },
        { control: "hdIdEstatus", requerido: 0, nombre: "Id del Proveedor ", defaultVal: "0", tipoControl: "Texto", columnaGrid: "IdPersona" }
];
var _estatus = new Object();
datosAccion = {
    "con": _estatus
};
columnasGrid = [{
    command: { iconClass: "k-icon k-edit", name: "c", text: "", click: rowclick }, title: " ", width: "100px"
},

                 {
                     field: "LongitudSerieEnSer_Orden",
                     title: "Longitud"

                 },
                 {
                     field: "Fecha",
                     title: "Fecha Modificacion"
                 }

];

function recuperarDatos() {
    _estatus.LongitudSerieEnSer_Orden = $("#txtEstatus").val();


}
function llenarDatos(dataItem) {
    $("#txtEstatus").val(dataItem.LongitudSerieEnSer_Orden);

}