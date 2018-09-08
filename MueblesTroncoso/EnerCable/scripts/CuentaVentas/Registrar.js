//$(document).ready(function () {

//});
datosCaptura = {
    hdIdCuenta: "0",
    txtCuenta: "",
    txtNombre: ""

};
datosGrid = {
    Nombre: { type: "string" },
    Cuenta: { type: "string" },
    IdTipoCuenta: { type: "string" },
    IdCuentaVenta: { type: "string" }
};
_accionGrid = "/CuentaVentas/obtenerCuentas";
_accion = "/CuentaVentas/agregarCuentas/";

datosInsercion = [
        { control: "txtCuenta", requerido: 1, nombre: "Cuenta ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Cuenta" },
        { control: "txtNombre", requerido: 1, nombre: "Nombre de la Cuenta ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Nombre" },
        { control: "hdIdCuenta", requerido: 0, nombre: "Id de la Cuenta ", defaultVal: "0", tipoControl: "Texto", columnaGrid: "IdCuentaCosto" },
        { control: "ddlTipoCuenta", requerido: 0, nombre: "Tipo de Cuenta ", defaultVal: "1", tipoControl: "Combo", columnaGrid: "IdTipoCuenta" }



];
var _cuenta = new Object();
datosAccion = {
    "con": _cuenta
};
columnasGrid = [{
    command: [{ iconClass: "k-icon k-edit", name: "c", text: "", click: rowclick, title: "Editar", width: "30px" }
  , { iconClass: "k-icon k-delete", name: "d", text: "", click: borrar, title: "Desligar", width: "30px" }

    ]
},

                 {
                     field: "Cuenta",
                     title: "Cuenta"

                 },
                 {
                     field: "IdCuentaVenta",
                     title: "IdCuentaVenta", hidden: true
                 },
                 {
                     field: "Nombre",
                     title: "Nombre"
                 },

                 {
                     field: "TipoCuenta",
                     title: "Tipo Cuenta"
                 },
                 {
                     field: "IdTipoCuenta",
                     title: "IdTipoCuenta",hidden:true
                 }
                 
];

function recuperarDatos() {
    _cuenta.IdCuentaVenta = $("#hdIdCuenta").val();
    _cuenta.Cuenta = $("#txtCuenta").val();
    _cuenta.Nombre = $("#txtNombre").val();
    _cuenta.IdTipoCuenta = $("#ddlTipoCuenta").val();

}
function llenarDatos(dataItem) {
    $("#hdIdCuenta").val(dataItem.IdCuentaVenta);
    $("#txtCuenta").val(dataItem.Cuenta);
    $("#txtNombre").val(dataItem.Nombre);

    
    $("#ddlTipoCuenta").selectpicker("val", dataItem.IdTipoCuenta);

}
function borrar(e) {
    e.preventDefault();
    var grid = $("#miGrid").data("kendoGrid");
    grid.clearSelection();
    $(e.currentTarget).closest("tr").addClass('k-state-selected');
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    if (dataItem) {





        swal({
            title: "Esta Seguro de Eliminar la Cuenta Venta " + dataItem.Cuenta+" "+dataItem.Nombre + "?",
            text: "Una Vez Eliminado el Movimiento No Se Podrá Revertir",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Si, Deseo Eliminar!",
            closeOnConfirm: false
        },
              function () {


                  var url = _encabezado + "/CuentaVentas/eliminarTabla";
                  var datosAccion = {
                      "idTabla": dataItem.IdCuentaVenta
                  };
                  var self = this;
                  axios.post(url, datosAccion)
                          .then(function (response) {
                              if (response.data.Result == "OK") {
                                  swal("Cuenta Venta Eliminada!", "Se ha Removido exitosamente la Cuenta Venta", "success");
                                  $("#miGrid").data("kendoGrid").dataSource.read();
                              }
                              else {
                                  swal("Ocurrio un Error!", response.data.Result, "error");
                              }

                          })
                          .catch(function (error) {
                              swal("Ocurrio un Error!", error, "error");
                          });

              });
    }
    else {
        swal("Ocurrio un Error al Eliminar el Registro!", "", "error");

    }
    return false;
}