var myWindow;
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

});
function onClose(e) {
    $("#hdIdPedido").val("0");
    limpiarPop();
}
datosCaptura = {
    hdIdPedido: "0",
    txtFechaInicio: "",
    txtFechaFin: "",
    txtFolioPedido: "",
    txtCliente: ""




};
datosGrid = {
    Cliente: { type: "string" },
    FechaPedido: { type: "string" },
    Folio: { type: "string" },
    Total: { type: "string" },
    IdPedido: { type: "string" },
    IdNotaDeCredito: { type: "string" }

};
_accionGrid = '/Pedido/obtenerPedidosFacturados';
_accion = "/Perfil/agregarPerfiles/";

datosInsercion = [
        { control: "txtPerfil", requerido: 1, nombre: "Nombre del Perfil ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Perfil" },

        { control: "hdIdPedido", requerido: 0, nombre: "Id del Pedido ", defaultVal: "0", tipoControl: "Texto", columnaGrid: "IdPedido" },
        { control: "ddlEstatus", requerido: 0, nombre: "Estatus del Perfil ", defaultVal: "1", tipoControl: "Combo", columnaGrid: "IdEstatus" }



];
var _perfil = new Object();
datosAccion = {
    "perfil": _perfil
};
columnasGrid = [
    {
        command: [{ iconClass: "k-icon k-edit", name: "c", text: "", click: mostrarVentana, title: "Editar", width: "30px" }
        , { iconClass: "k-icon k-i-connector", name: "d", text: "", click: desligar, title: "Desligar", width: "30px" }

        ]
    },

{
    field: "Cliente",
    title: "Cliente"

},

{
    field: "FechaPedido",
    title: "Fecha"
},
{
    field: "Folio",
    title: "Folio"
},
{
    field: "Total",
    title: "Importe"
},
{
    field: "IdPedido",
    title: "IdPedido", hidden: true
},
{
    field: "IdNotaDeCredito",
    title: "IdNotaDeCredito", hidden: true
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
function loadGridMenu(idperfil, perfil) {


    var parametros = {};
    parametros["IdPedido"] = 0;
    $.ajax({
        type: 'GET',
        url: _encabezado + "/Pedido/obtenerDetallePedido?IdPedido=" + idperfil,
        dataType: 'json',
        beforeSend: function () { $("#tablaMenu").html(""); },
        data: '{IdPedido:' + 0 + '}',
        contentType: 'application/json; charset=utf-8',
        success: function (msg) {
            $("#tablaMenu").html(msg.Detalle);
            $("#tablaDatos").html(msg.Datos);
            $("#tablaFecha").html(msg.Fecha);

            changeNumerico();

            if (parseInt(msg.Cliente.IdNotaCredito) > 0) {
                $('select[name=txtUso]').val(msg.Cliente.UsoCfdi);
                $('select[name=txtTipoDocumento]').val(msg.Cliente.TipoDocumento);
                $('select[name=txtUso]').selectpicker('refresh');
                $('select[name=txtTipoDocumento]').selectpicker('refresh');

                $("#txtRelacionado").val(msg.Cliente.CfdiRelacionado);

                $("#txtSubTotal").val(msg.Cliente.SubTotal);
                $("#txtIva").val(msg.Cliente.Iva);
                $("#txtTotal").val(msg.Cliente.Total);
                $(".chkPer").hide();
                $(".chkMain").hide();
                $(".ccMainLabel").hide();



            }

            var window = myWindow.data("kendoWindow");

            window.element.prev().find(".k-window-title").html("<b class='custom'>Detalle para El Folio " + perfil + "</b>");

            $("#window").data("kendoWindow").center().open();

        },
        error: function (x, status, error) {
            alert("Ocurrio un Error: " + status + "nError: " + x.responseText);
        }, async: false


    });






}
function changeNumerico() {
    $(".numerico").bind('keyup change click', function (e) {
        if (!$(this).data("previousValue") ||
               $(this).data("previousValue") != $(this).val()
           ) {

            $(this).data("previousValue", $(this).val());
            /*alert($(this).val());
            alert($(this).attr("posicion"));*/
            cambiarValores($(this).attr("posicion"));
        }

    });
}
function cambiarValores(_posicion) {

    var _pu = parseFloat($("#tdPuArticulo_" + _posicion).html());
    var _cantidad = parseFloat($("#txtCantidad" + _posicion).val());
    var _descpza = parseFloat($("#tdDescPzaArticulo_" + _posicion).html());
    var _subtotal = _pu * _cantidad;
    var _totaldescuento = _descpza * _cantidad;
    var _total = _subtotal - _totaldescuento;

    $("#tdSubTotalArticulo_" + _posicion).html(_subtotal);
    $("#tdTotalArticulo_" + _posicion).html(_total);
    $("#tdDescTotalArticulo_" + _posicion).html(_totaldescuento);

    recalculaTotal();

}
function recalculaTotal() {
    var _totalTotal = 0.00;
    var _tasa = parseFloat($("#tasaIva").val());
    $('.chkPer').each(function (index, obj) {


        if (this.checked === true) {
            var _X = (this.id);
            var _posicion = $("#" + _X).attr("posicion");
            _totalTotal += parseFloat($("#tdTotalArticulo_" + _posicion).html());

        }

    });
    $("#txtSubTotal").val(_totalTotal);
    $("#txtIva").val(parseFloat(_totalTotal * _tasa));
    $("#txtTotal").val(_totalTotal + parseFloat((_totalTotal * _tasa)));
}
function mostrarVentana(e) {
    e.preventDefault();
    var grid = $("#miGrid").data("kendoGrid");
    grid.clearSelection();
    $(e.currentTarget).closest("tr").addClass('k-state-selected');
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    if (dataItem) {

        if (parseInt(dataItem.IdNotaDeCredito) == 0) $("#btnGuardarPermisos").removeAttr("disabled");
        else $("#btnGuardarPermisos").attr("disabled", "disabled");


        $("#hdIdPedido").val(dataItem.IdPedido);
        loadGridMenu(dataItem.IdPedido, dataItem.Folio);



    }

}
function desligar(e) {
    e.preventDefault();
    var grid = $("#miGrid").data("kendoGrid");
    grid.clearSelection();
    $(e.currentTarget).closest("tr").addClass('k-state-selected');
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    if (dataItem) {
        var _idnotaCredito = dataItem.IdNotaDeCredito;
        if (parseInt(_idnotaCredito) == 0) {
            swal("Desligar Nota de Crédito", "No es Posible Desligar un Pedido Que no Cuenta Con Nota de Credito", "warning"); return;
        }
        else {
            swal({
                title: "Esta Seguro de Desligar?",
                text: "Se desligara la Nota de Credito y No será Posible Recuperarla",
                type: "warning",
                showCancelButton: true,
                confirmButtonClass: "btn-danger",
                confirmButtonText: "Si, Deseo Desligar!",
                closeOnConfirm: false
            },
                function () {

                    var url = _encabezado + "/Pedido/desligarNota";
                    var datosAccion = {
                        "idNota": _idnotaCredito
                    };
                    var self = this;
                    axios.post(url, datosAccion)
                            .then(function (response) {
                                if (response.data.Result == "OK") {
                                    swal("Nota Desligada!", "Se ha desligado exitosamente la Nota de Credito", "success");
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
        //   $("#hdIdPedido").val(dataItem.IdPedido);
        // loadGridMenu(dataItem.IdPedido, dataItem.Folio);
    }
}

function checkTodo(ch) {
    var c = ch.checked;
    $('.chkPer').prop('checked', c);
    recalculaTotal();
}
function checkaEsta(ch) {

    recalculaTotal();
}
function recuperarMenu() {

    var ListaPedidosFacturadosDetalle = new Array();
    var _perfil = new Object();
    var _resultado = "";
    $('.chkPer').each(function (index, obj) {

        if (this.checked === true) {
            var _X = (this.id);


            var _posicion = $("#" + _X).attr("posicion");
            var PedidosFacturadosDetalle = new Object();
            var _pu = parseFloat($("#tdPuArticulo_" + _posicion).html());
            var _cantidad = parseFloat($("#txtCantidad" + _posicion).val());
            var _descpza = parseFloat($("#tdDescPzaArticulo_" + _posicion).html());
            var _subtotal = _pu * _cantidad;
            var _totaldescuento = _descpza * _cantidad;
            var _total = _subtotal - _totaldescuento;


            PedidosFacturadosDetalle.IdPedido = $("#" + _X).attr("valor");
            PedidosFacturadosDetalle.Consecutivo = $("#" + _X).attr("consecutivo");
            PedidosFacturadosDetalle.ClaveArticulo = $("#" + _X).attr("claveArticulo");
            PedidosFacturadosDetalle.Descripcion = $("#" + _X).attr("descripcion");
            PedidosFacturadosDetalle.Cantidad = $("#txtCantidad" + _posicion).val();
            PedidosFacturadosDetalle.Unidad = $("#tdUnidad_" + _posicion).html();
            PedidosFacturadosDetalle.PrecioUnitario = $("#tdPuArticulo_" + _posicion).html();
            PedidosFacturadosDetalle.SubTotal = _subtotal;
            PedidosFacturadosDetalle.Total = _total;
            PedidosFacturadosDetalle.IdTienda = $("#" + _X).attr("idTienda");
            PedidosFacturadosDetalle.TotalDescuento = _totaldescuento;
            PedidosFacturadosDetalle.DescuentoPorPieza = $("#tdDescPzaArticulo_" + _posicion).html();
            PedidosFacturadosDetalle.IdArticulo = $("#" + _X).attr("IdArticulo");
            PedidosFacturadosDetalle.IdNotaCredito = 0;

            ListaPedidosFacturadosDetalle.push(PedidosFacturadosDetalle);




        }
    });

    return ListaPedidosFacturadosDetalle;
}

function guardarPermiso() {

    var _menus = recuperarMenu();
    var _header = new Object();
    if (_menus.length > 0) {
        _header.IdPedido = _menus[0].IdPedido;
        _header.IdClienteFactura = $("#clienteFactura").val();
        _header.UsoCfdi = $("#txtUso").val();
        _header.TipoDeDocumento = $("#txtTipoDocumento").val();
        _header.SubTotal = $("#txtSubTotal").val();
        _header.Iva = $("#txtIva").val();
        _header.Total = $("#txtTotal").val();
        _header.TasaIva = $("#tasaIva").val();
        _header.CfdiRelacionado = $("#txtRelacionado").val();



        var url = _encabezado + "/Pedido/guardarNota";
        var datosAccion = {
            "nota": _header,
            "detalle": _menus
        };
        $("#btnGuardarPermisos").attr("disabled", "disabled");
        var self = this;
        axios.post(url, datosAccion)
                .then(function (response) {


                    //$("#btnGuardarPermisos").removeAttr("disabled");
                    $("#window").data("kendoWindow").close();
                    $("#miGrid").data("kendoGrid").dataSource.read();
                    $("#hdIdPedido").val("0");
                    swal("Generar Nota de Crédito", "Nota de Crédito #" + response.data.Result + " Generada Exitosamente", "success"); return;

                })
                .catch(function (error) {
                    swal("Ocurrio un Error!", error, "error");
                    $("#btnGuardarPermisos").removeAttr("disabled");
                });

    }
    else {
        swal("Generar Nota de Crédito", "Por Favor Seleccione al Menos un Partida", "warning"); return;
    }

    return false;
}



function limpiarPop() {
    $("#tablaDatos").html("");
    $("#tablaFecha").html("");
    $("#tablaMenu").html("");
    $("#txtRelacionado").val("");


    $('select[name=txtUso]').val("P01");
    $('select[name=txtTipoDocumento]').val("04");
    $('select[name=txtUso]').selectpicker('refresh');
    $('select[name=txtTipoDocumento]').selectpicker('refresh');



    $("#txtSubTotal").val("0.00");
    $("#txtIva").val("0.00");
    $("#txtTotal").val("0.00");
}