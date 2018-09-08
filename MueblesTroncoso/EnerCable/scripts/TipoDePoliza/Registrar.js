//$(document).ready(function () {

//});
datosCaptura = {
    hdIdTipoDePoliza: "0",
    txtTipoDePoliza: "",
    txtPosicion: "",
    txtLongitud: "",
    txtTipoExtraccion: "",
    txtDatoValidar: "",
    txtCate: "",

};
datosGrid = {
    IdTipoDePoliza: { type: "string" },
    TipoDePoliza: { type: "string" },
    AplicaNombreCuenta: { type: "string" },
    AplicaCondicionNombreCuenta: { type: "string" },
    Posicion: { type: "string" },
    Longitud: { type: "string" },
    TipoExtraccion: { type: "string" },
    DatoValidar: { type: "string" },
    Cate: { type: "string" },
    AplicaFacturaEnSerie: { type: "string" }

};
_accionGrid = "/TipoDePoliza/obtenerTipos";
_accion = "/TipoDePoliza/agregarTipos/";

datosInsercion = [
        { control: "txtTipoDePoliza", requerido: 1, nombre: "Tipo de Poliza ", defaultVal: "", tipoControl: "Texto", columnaGrid: "TipoDePoliza" },
        { control: "txtPosicion", requerido: 1, nombre: "Posicion ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Posicion" },
        { control: "txtLongitud", requerido: 1, nombre: "Longitud ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Longitud" },
        { control: "txtTipoExtraccion", requerido: 0, nombre: "Tipo Extraccion ", defaultVal: "", tipoControl: "Texto", columnaGrid: "TipoExtraccion" },
        { control: "txtDatoValidar", requerido: 0, nombre: "Dato Validar ", defaultVal: "", tipoControl: "Texto", columnaGrid: "DatoValidar" },
        { control: "txtCate", requerido: 0, nombre: "Cate ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Cate" },

        { control: "hdIdTipoDePoliza", requerido: 0, nombre: "Id de la Tabla ", defaultVal: "0", tipoControl: "Texto", columnaGrid: "IdTipoDePoliza" },
        { control: "ddlAplicaCuenta", requerido: 0, nombre: "Aplica Cuenta ", defaultVal: "SI", tipoControl: "Combo", columnaGrid: "AplicaNombreCuenta" },
        { control: "ddlAplicaCondicion", requerido: 0, nombre: "Aplica Condicion ", defaultVal: "SI", tipoControl: "Combo", columnaGrid: "AplicaCondicionNombreCuenta" },
        { control: "ddlAplicaFactura", requerido: 0, nombre: "Aplica Factura ", defaultVal: "SI", tipoControl: "Combo", columnaGrid: "AplicaFacturaEnSerie" }



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
                    field: "TipoDePoliza",
                    title: "Tipo Poliza"
                },
                {
                    field: "Posicion",
                    title: "Posicion"
                },
                {
                    field: "Longitud",
                    title: "Longitud"
                },

                 {
                     field: "TipoExtraccion",
                     title: "Tipo Extraccion"

                 },

                 {
                     field: "DatoValidar",
                     title: "Dato Validar"

                 },

                 {
                     field: "Cate",
                     title: "Cate"

                 },
                 {
                     field: "AplicaNombreCuenta",
                     title: "A.NombreCuenta"

                 },
                 {
                     field: "AplicaCondicionNombreCuenta",
                     title: "A.CondicionNombreCuenta"

                 },
                 {
                     field: "AplicaFacturaEnSerie",
                     title: "A.FacturaEnSerie"

                 },

                 {
                     field: "IdTipoDePoliza",
                     title: "IdTipoDePoliza", hidden: true
                 }

];

function recuperarDatos() {
    _cuenta.IdTipoDePoliza = $("#hdIdTipoDePoliza").val();
    _cuenta.TipoDePoliza = $("#txtTipoDePoliza").val();
    _cuenta.AplicaNombreCuenta = $("#ddlAplicaCuenta").val();
    _cuenta.AplicaCondicionNombreCuenta = $("#ddlAplicaCondicion").val();
    _cuenta.Posicion = $("#txtPosicion").val();
    _cuenta.Longitud = $("#txtLongitud").val();
    _cuenta.TipoExtraccion = $("#txtTipoExtraccion").val();
    _cuenta.DatoValidar = $("#txtDatoValidar").val();
    _cuenta.Cate = $("#txtCate").val();
    _cuenta.AplicaFacturaEnSerie = $("#ddlAplicaFactura").val();

}
function llenarDatos(dataItem) {
    
    $("#hdIdTipoDePoliza").val(dataItem.IdTipoDePoliza);
    $("#txtTipoDePoliza").val(dataItem.TipoDePoliza);
    $("#txtPosicion").val(dataItem.Posicion);
    $("#txtLongitud").val(dataItem.Longitud);
    $("#txtTipoExtraccion").val(dataItem.TipoExtraccion);
    $("#txtDatoValidar").val(dataItem.DatoValidar);
    $("#txtCate").val(dataItem.Cate);


    $("#ddlAplicaFactura").selectpicker("val", dataItem.AplicaFacturaEnSerie);
    $("#ddlAplicaCuenta").selectpicker("val", dataItem.AplicaNombreCuenta);
    $("#ddlAplicaCondicion").selectpicker("val", dataItem.AplicaCondicionNombreCuenta);


}
function borrar(e) {
    e.preventDefault();
    var grid = $("#miGrid").data("kendoGrid");
    grid.clearSelection();
    $(e.currentTarget).closest("tr").addClass('k-state-selected');
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    if (dataItem) {





        swal({
            title: "Esta Seguro de Eliminar el Tipo de Poliza " + dataItem.TipoDePoliza + "?",
            text: "Una Vez Eliminado el Movimiento No Se Podrá Revertir",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Si, Deseo Eliminar!",
            closeOnConfirm: false
        },
              function () {


                  var url = _encabezado + "/TipoDePoliza/eliminarTabla";
                  var datosAccion = {
                      "idTabla": dataItem.IdTipoDePoliza
                  };
                  var self = this;
                  axios.post(url, datosAccion)
                          .then(function (response) {
                              if (response.data.Result == "OK") {
                                  swal("Tipo De Poliza Eliminada!", "Se ha Removido exitosamente el Tipo de Poliza", "success");
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