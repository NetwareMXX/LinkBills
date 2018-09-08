//$(document).ready(function () {

//});
datosCaptura = {
    hdIdEstatus: "0",
    txtEstatus: "",


};
datosGrid = {
    
    Estatus: { type: "string" },
    IdEstatus: { type: "string" }
};
_accionGrid = "/Estatus/obtenerEstatus";
_accion = "/Estatus/agregarServicios/";

datosInsercion = [
        { control: "txtEstatus", requerido: 1, nombre: "Nombre del Estatus ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Estatus" },
        { control: "hdIdEstatus", requerido: 0, nombre: "Id del Proveedor ", defaultVal: "0", tipoControl: "Texto", columnaGrid: "IdPersona" }
];
var _estatus = new Object();
datosAccion = {
    "estatus": _estatus
};
columnasGrid = [{
    command: { iconClass: "k-icon k-edit", name: "c", text: "", click: rowclick }, title: " ", width: "100px"
},

                 {
                     field: "Estatus1",
                     title: "Estatus"

                 },
                 {
                     field: "IdEstatus",
                     title: "IdEstatus", hidden: true
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