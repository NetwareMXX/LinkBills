//$(document).ready(function () {

//});
datosCaptura = {
    hdIdUsuario: "0",
    txtNombre: "",
    txtApPaterno: "",
    txtApMaterno: "",
    txtNombreUsuario: "",
    txtPassword: "",
    txtCorreo: "",
    txtTelefono: ""


};
datosGrid = {
    Nombre: { type: "string" },
    Usuario: { type: "string" },
    Paterno: { type: "string" },
    Materno: { type: "string" },
    Password: { type: "string" },
    Correo: { type: "string" },
    Perfil: { type: "string" },
    Estatus: { type: "string" },
    IdUsuario: { type: "string" }
};
_accionGrid = "/Usuario/ObtenerUsuarios";
_accion = "/Usuario/agregarUsuarios/";

datosInsercion = [
        { control: "txtNombre", requerido: 1, nombre: "Nombre del Usuario ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Nombre" },
        { control: "txtApPaterno", requerido: 1, nombre: "Apellido Paterno del Usuario ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Paterno" },
        { control: "txtApMaterno", requerido: 0, nombre: "Apellido Materno del Usuario ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Materno" },
        { control: "txtNombreUsuario", requerido: 1, nombre: "Usuario ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Usuario" },
        { control: "txtPassword", requerido: 1, nombre: "Password del Usuario ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Password" },
        { control: "txtCorreo", requerido: 1, nombre: "Correo del Usuario ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Correo" },
        { control: "txtTelefono", requerido: 0, nombre: "Telefono del Usuario ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Telefono" },
        { control: "hdIdUsuario", requerido: 0, nombre: "Id del Usuario ", defaultVal: "0", tipoControl: "Texto", columnaGrid: "IdUsuario" },
        { control: "ddlEstatus", requerido: 0, nombre: "Estatus del Usuario ", defaultVal: "1", tipoControl: "Combo", columnaGrid: "IdEstatus" },
        { control: "ddlPerfil", requerido: 0, nombre: "Perfil del Usuario ", defaultVal: "1", tipoControl: "Combo", columnaGrid: "IdPerfil" }



];
var _usuario = new Object();
datosAccion = {
    "usuario": _usuario
};
columnasGrid = [{
    command: { iconClass: "k-icon k-edit", name: "c", text: "", click: rowclick }, title: " ", width: "100px"
},

                 {
                     field: "Usuario",
                     title: "Usuario"

                 },

                 {
                     field: "Password",
                     title: "Password", hidden: true
                 },
                 {
                     field: "IdUsuario",
                     title: "IdUsuario", hidden: true
                 },
                 {
                     field: "Nombre",
                     title: "Nombre"
                 },

                 {
                     field: "Paterno",
                     title: "Paterno"
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
                     field: "IdEstatus",
                     title: "IdEstatus", hidden: true
                 },
                 {
                     field: "IdPerfil",
                     title: "IdPerfil", hidden: true
                 },
                 {
                     field: "Telefono",
                     title: "Telefono", hidden: true
                 },
                 {
                     field: "Materno",
                     title: "Materno", hidden: true
                 },

                 {
                     field: "Correo",
                     title: "Correo", hidden: true
                 }

];

function recuperarDatos() {
    _usuario.IdUsuario = $("#hdIdUsuario").val();
    _usuario.Usuario = $("#txtNombreUsuario").val();
    _usuario.Password = $("#txtPassword").val();

    _usuario.Nombre = $("#txtNombre").val();
    _usuario.Paterno = $("#txtApPaterno").val();
    _usuario.Materno = $("#txtApMaterno").val();
    _usuario.Correo = $("#txtCorreo").val();
    _usuario.Telefono = $("#txtTelefono").val();
    _usuario.IdEstatus = $("#ddlEstatus").val();
    _usuario.IdPerfil = $("#ddlPerfil").val();

}
function llenarDatos(dataItem) {
    $("#hdIdUsuario").val(dataItem.IdUsuario);
    $("#txtNombreUsuario").val(dataItem.Usuario);
    $("#txtPassword").val(dataItem.Password);

    $("#txtNombre").val(dataItem.Nombre);
    $("#txtApPaterno").val(dataItem.Paterno);
    $("#txtApMaterno").val(dataItem.Materno);
    $("#txtCorreo").val(dataItem.Correo);
    $("#txtTelefono").val(dataItem.Telefono);

    $("#ddlEstatus").selectpicker("val", dataItem.IdEstatus);
    $("#ddlPerfil").selectpicker("val", dataItem.IdPerfil);

}