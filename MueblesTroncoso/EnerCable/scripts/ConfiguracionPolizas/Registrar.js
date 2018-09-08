//$(document).ready(function () {

//});
datosCaptura = {
    hdIdConfiguracion: "0",
    txtPosicion: "",
    txtLongitud: "",
    txtTipoConfiguracion: "",
    txtTipoExtraccion: ""

};
datosGrid = {
    IdConfiguracionPoliza: { type: "string" },
    IdTipoDePoliza: { type: "string" },
    Posicion: { type: "string" },
    Longitud: { type: "string" },
    TipoConfiguracion: { type: "string" },
    TipoExtraccion: { type: "string" },
    TipoDePoliza: { type: "string" }
};
_accionGrid = "/ConfiguracionPolizas/obtenerConfiguraciones";
_accion = "/ConfiguracionPolizas/agregarConfiguracion/";

datosInsercion = [
        { control: "txtPosicion", requerido: 1, nombre: "Posicion ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Posicion" },
        { control: "txtLongitud", requerido: 1, nombre: "Longitud ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Longitud" },
        { control: "txtTipoConfiguracion", requerido: 1, nombre: "Tipo de Configuracion ", defaultVal: "", tipoControl: "Texto", columnaGrid: "TipoConfiguracion" },
        { control: "txtTipoExtraccion", requerido: 1, nombre: "Tipo de Extraccion ", defaultVal: "", tipoControl: "Texto", columnaGrid: "TipoExtraccion" },
        { control: "hdIdConfiguracion", requerido: 0, nombre: "Id de la Configuracion ", defaultVal: "0", tipoControl: "Texto", columnaGrid: "IdConfiguracionPoliza" },
        { control: "ddlTipoPoliza", requerido: 0, nombre: "Tipo de Poliza ", defaultVal: "1", tipoControl: "Combo", columnaGrid: "IdTipoDePoliza" }



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
                    field: "TipoConfiguracion",
                    title: "Tipo Configuracion"
                },
                {
                    field: "TipoExtraccion",
                    title: "Tipo Extraccion"
                },
                {
                    field: "TipoDePoliza",
                    title: "Tipo de Poliza"
                },

                 {
                     field: "Posicion",
                     title: "Posicion"

                 },
                 {
                     field: "IdConfiguracionPoliza",
                     title: "IdConfiguracionPoliza", hidden: true
                 },
                 {
                     field: "IdTipoDePoliza",
                     title: "IdTipoDePoliza", hidden: true
                 },
                 {
                     field: "Longitud",
                     title: "Longitud"
                 }

];

function recuperarDatos() {
    _cuenta.IdConfiguracionPoliza = $("#hdIdConfiguracion").val();
    _cuenta.Posicion = $("#txtPosicion").val();
    _cuenta.Longitud = $("#txtLongitud").val();
    _cuenta.TipoConfiguracion = $("#txtTipoConfiguracion").val();
    _cuenta.TipoExtraccion = $("#txtTipoExtraccion").val();
    _cuenta.IdTipoDePoliza = $("#ddlTipoPoliza").val();

}
function llenarDatos(dataItem) {
    $("#hdIdConfiguracion").val(dataItem.IdConfiguracionPoliza);
    $("#txtPosicion").val(dataItem.Posicion);
    $("#txtLongitud").val(dataItem.Longitud);
    $("#txtTipoConfiguracion").val(dataItem.TipoConfiguracion);
    $("#txtTipoExtraccion").val(dataItem.TipoExtraccion);
    $("#ddlTipoPoliza").selectpicker("val", dataItem.IdTipoDePoliza);

}




function borrar(e) {
    e.preventDefault();
    var grid = $("#miGrid").data("kendoGrid");
    grid.clearSelection();
    $(e.currentTarget).closest("tr").addClass('k-state-selected');
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    if (dataItem) {





        swal({
            title: "Esta Seguro de Eliminar la Configuracion de Poliza " + dataItem.TipoConfiguracion + "?",
            text: "Una Vez Eliminado el Movimiento No Se Podrá Revertir",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Si, Deseo Eliminar!",
            closeOnConfirm: false
        },
              function () {


                  var url = _encabezado + "/ConfiguracionPolizas/eliminarTabla";
                  var datosAccion = {
                      "idTabla": dataItem.IdConfiguracionPoliza
                  };
                  var self = this;
                  axios.post(url, datosAccion)
                          .then(function (response) {
                              if (response.data.Result == "OK") {
                                  swal("Configuracion de Poliza Eliminada!", "Se ha Removido exitosamente la Configuracion de Poliza", "success");
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