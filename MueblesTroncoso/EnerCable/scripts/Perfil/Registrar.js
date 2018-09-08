var myWindow;
$(document).ready(function () {
    myWindow = $("#window");
    myWindow.kendoWindow({
        width: "600px",
        title: "Permisos",
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

});
function onClose(e) {
    $("#hdIdPerfil").val("0");
}
datosCaptura = {
    hdIdPerfil: "0",
    txtPerfil: "",


};
datosGrid = {
    Perfil: { type: "string" },
    Estatus: { type: "string" },
    IdPerfil: { type: "string" }
};
_accionGrid = "/Perfil/ObtenerPerfiles";
_accion = "/Perfil/agregarPerfiles/";

datosInsercion = [
        { control: "txtPerfil", requerido: 1, nombre: "Nombre del Perfil ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Perfil" },

        { control: "hdIdPerfil", requerido: 0, nombre: "Id del Perfil ", defaultVal: "0", tipoControl: "Texto", columnaGrid: "IdPerfil" },
        { control: "ddlEstatus", requerido: 0, nombre: "Estatus del Perfil ", defaultVal: "1", tipoControl: "Combo", columnaGrid: "IdEstatus" }



];
var _perfil = new Object();
datosAccion = {
    "perfil": _perfil
};
columnasGrid = [
    {
        command: [{ iconClass: "k-icon k-edit", name: "c", text: "", click: rowclick, title: "Editar", width: "30px" },
            { iconClass: "k-icon k-i-lock", name: "d", text: "", click: mostrarVentana, title: "Permisos", width: "30px" }
        ]
    },

{
    field: "Perfil",
    title: "Perfil"

},

{
    field: "Estatus",
    title: "Estatus"
},
{
    field: "IdPerfil",
    title: "IdPerfil", hidden: true
},
{
    field: "IdEstatus",
    title: "IdEstatus", hidden: true
}

];

function recuperarDatos() {
    _perfil.IdPerfil = $("#hdIdPerfil").val();
    _perfil.Perfil = $("#txtPerfil").val();
    _perfil.IdEstatus = $("#ddlEstatus").val();


}
function llenarDatos(dataItem) {
    $("#hdIdPerfil").val(dataItem.IdPerfil);
    $("#txtPerfil").val(dataItem.Perfil);
    $("#ddlEstatus").selectpicker("val", dataItem.IdEstatus);
    //loadGridMenu(dataItem.IdPerfil);
}
function loadGridMenu(idperfil,perfil) {


    var parametros = {};
    parametros["IdPerfil"] = 0;
    $.ajax({
        type: 'GET',
        url: _encabezado + "/Perfil/obtenerPermisosPorPerfil?IdPerfil=" + idperfil,
        dataType: 'json',
        beforeSend: function () { $("#tablaMenu").html(""); },
        data: '{IdPerfil:' + 0 + '}',
        contentType: 'application/json; charset=utf-8',
        success: function (msg) {
            $("#tablaMenu").html(msg);

            var window = myWindow.data("kendoWindow");

            window.element.prev().find(".k-window-title").html("<b class='custom'>Permisos para el Perfil de "+perfil+"</b>");

            $("#window").data("kendoWindow").center().open();

        },
        error: function (x, status, error) {
            alert("Ocurrio un Error: " + status + "nError: " + x.responseText);
        }, async: false


    });






}
function mostrarVentana(e) {
    e.preventDefault();
    var grid = $("#miGrid").data("kendoGrid");
    grid.clearSelection();
    $(e.currentTarget).closest("tr").addClass('k-state-selected');
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    if (dataItem) {
        $("#hdIdPerfil").val(dataItem.IdPerfil);
        loadGridMenu(dataItem.IdPerfil,dataItem.Perfil);
    }

}
function checkTodo(ch) {
    var c = ch.checked;
    $('.chkPer').prop('checked', c);
}
function recuperarMenu() {

    var _resultado = "";
    $('.chkPer').each(function (index, obj) {

        if (this.checked === true) {
            var _X = (this.id);

            _resultado += $("#" + _X).attr("valor") + ",";
        }
    });
    _resultado += "x";
    _resultado = _resultado.replace(",x", "");
    if (_resultado == "x") _resultado = "";
    return _resultado;
}
function guardarPermiso() {

    var _menus = recuperarMenu();

    $.ajax({
        type: 'POST',
        url: _encabezado + "/Perfil/agregarPerfilesPermisos",
        dataType: 'json',
        beforeSend: function () { $("#btnGuardarPermisos").attr("disabled", "disabled"); },
        data: '{permisos:"' + _menus + '",idperfil:' + $("#hdIdPerfil").val() + '}',
        contentType: 'application/json; charset=utf-8',
        success: function (msg) {
            $("#btnGuardarPermisos").removeAttr("disabled");
            $("#window").data("kendoWindow").close();
            $("#miGrid").data("kendoGrid").dataSource.read();
            $("#hdIdPerfil").val("0");
        },
        error: function (x, status, error) {
            $("#hdIdPerfil").val("0");
            alert("Ocurrio un Error: " + status + "nError: " + x.responseText);
        }, async: false
    });

    return false;
}