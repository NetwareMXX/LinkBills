//$(document).ready(function () {

//});
datosCaptura = {
    hdIdTabla: "0",
    txtTabla: "",
    txtEjercicio: "",
    txtTablaC: ""

};
datosGrid = {
    IdTabla: { type: "string" },
    Tabla: { type: "string" },
    Ejercicio: { type: "string" },
    Activo: { type: "string" },
    TablaNCRBON: { type: "string" }
};
_accionGrid = "/Tabla/obtenerTablas";
_accion = "/Tabla/agregarTablas/";

datosInsercion = [
        { control: "txtTabla", requerido: 1, nombre: "Tabla ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Tabla" },
        { control: "txtEjercicio", requerido: 1, nombre: "Ejercicio ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Ejercicio" },
        { control: "txtTablaC", requerido: 1, nombre: "Tabla NCRBON ", defaultVal: "", tipoControl: "Texto", columnaGrid: "TablaNCRBON" },
        { control: "hdIdTabla", requerido: 0, nombre: "Id de la Tabla ", defaultVal: "0", tipoControl: "Texto", columnaGrid: "IdTabla" },
        { control: "ddlActivo", requerido: 0, nombre: "Activo ", defaultVal: "True", tipoControl: "Combo", columnaGrid: "Activo" }



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
                    field: "Tabla",
                    title: "Tabla"
                },
                {
                    field: "Ejercicio",
                    title: "Ejercicio"
                },
                {
                    field: "TablaNCRBON",
                    title: "TablaNCRBON"
                },

                 {
                     field: "Activo",
                     title: "Activo"

                 },
                 {
                     field: "IdTabla",
                     title: "IdTabla", hidden: true
                 }

];

function recuperarDatos() {
    _cuenta.IdTabla = $("#hdIdTabla").val();
    _cuenta.Tabla = $("#txtTabla").val();
    _cuenta.Ejercicio = $("#txtEjercicio").val();
    _cuenta.Activo = $("#ddlActivo").val().replace("true","True").replace("false","False");
    _cuenta.TablaNCRBON = $("#txtTablaC").val();

}
function llenarDatos(dataItem) {
    
    $("#hdIdTabla").val(dataItem.IdTabla);
    $("#txtTabla").val(dataItem.Tabla);
    $("#txtEjercicio").val(dataItem.Ejercicio);
    $("#txtTablaC").val(dataItem.TablaNCRBON);
    $("#ddlActivo").selectpicker("val", dataItem.Activo.toString().replace("true","True").replace("false","False"));

}
function borrar(e) {
    e.preventDefault();
    var grid = $("#miGrid").data("kendoGrid");
    grid.clearSelection();
    $(e.currentTarget).closest("tr").addClass('k-state-selected');
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    if (dataItem) {


        if (dataItem.Activo == "true")
        {
            swal("Ocurrio un Error al Eliminar el Registro!", "No Es Posible Remover Una Tabla Que Se Encuentra Activa", "error");
            return false;
        }



        swal({
            title: "Esta Seguro de Eliminar la Tabla "+dataItem.Tabla+"?",
            text: "Una Vez Eliminado el Movimiento No Se Podrá Revertir",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Si, Deseo Eliminar!",
            closeOnConfirm: false
        },
              function () {
                  
                  
                  var url = _encabezado + "/Tabla/eliminarTabla";
                  var datosAccion = {
                      "idTabla": dataItem.IdTabla
                  };
                  var self = this;
                  axios.post(url, datosAccion)
                          .then(function (response) {
                              if (response.data.Result == "OK") {
                                  swal("Tabla Eliminada!", "Se ha Removido exitosamente la Tabla", "success");
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