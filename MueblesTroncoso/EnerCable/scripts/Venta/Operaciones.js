
document.addEventListener('DOMContentLoaded', function (event) {
    let view = new Vue({
        el: document.getElementById('view'),
        mounted: function () { },
        data: datosCaptura,

        methods:
        {
            eliminar: Eliminar
            , guardar: Guardar
            , load: Load
            //, cargar: loadGrid
            , actualizar: Actualizar
            , tooglePanel: mostrarCaptura
            , cancelar: cancelar
        }
    });

    //.cargar();
    view.load();
});

function Eliminar() { }
function Guardar() {


    var _idmes = $("#ddlMes").val();
    $.ajax({
        type: 'GET',
        url: _encabezado + "/Venta/obtenerCostos?idMes=" + _idmes,
        dataType: 'json',
        beforeSend: function () {
            $("#tablaMenu").html("");
            $("#divProgreso").show();
            $("#txtFiltro").off();
            $("ddlFiltroSerie").off();
            $(".divFiltro").hide();
        },
        data: '{idMes:' + _idmes + '}',
        contentType: 'application/json; charset=utf-8',
        success: function (msg) {
            $("#divProgreso").hide();

            $("#tablaMenu").html(msg.Data);

            if (msg.Success == "OK") {
                var _table = $('.js-exportable').DataTable({
                    dom: 'Bfrtip',
                    responsive: true,
                    paging: false,
                    buttons: [
                        'copy', 'csv', 'excel', 'print'
                    ]
                });
                $(".divFiltro").show();
                $('#txtFiltro').on('keyup', function () {
                    _table
                        .columns(5)
                        .search(this.value)
                        .draw();
                });
                $('#ddlFiltroSerie').on('change', function () {

                    _table
                        .columns(1)
                        .search(this.value)
                        .draw();
                });
                $('#ddlFiltroFactura').on('change', function () {

                    _table
                        .columns(2)
                        .search(this.value)
                        .draw();
                });


                $("#aPanel").click();
            }



        },
        error: function (x, status, error) {
            alert("Ocurrio un Error: " + status + "nError: " + x.responseText);
        }, async: true


    });


    return false;

}
function Load() {
    $('.combo').selectpicker();
    /*$('#ddlTipoBusqueda').on('change', function (e) {
        var _seccion = $(this).find("option:selected").val();
        $(".opciones").hide();

        $("#txtFechaInicio").val("");
        $("#txtFechaFin").val("");
        $("#txtFolioPedido").val("");



        switch (_seccion) {
            case "1": $("#rowFecha").show(); break;
            case "2": $("#rowFolio").show(); break;

        }

    



    });    */
    /* $('.datepicker').bootstrapMaterialDatePicker({
         format: 'DD-MM-YYYY',
         clearButton: true,
         weekStart: 1,
         time: false
     });*/

    $("#aPanel").click(function () {
        if ($("#aPanel").hasClass("collapsed")) {
            $('a#aCapturar').html('<i class="material-icons">screen_lock_rotation</i>Ocultar');
        }
        else {
            $('a#aCapturar').html('<i class="material-icons">screen_rotation</i>Capturar');


        }
    });
    for (var j = 0; j < datosInsercion.length; j++) {
        if (datosInsercion[j].tipoControl === "Texto") {


            $("#" + datosInsercion[j].control).val(datosInsercion[j].defaultVal);
        }
        else if (datosInsercion[j].tipoControl === "Combo") {
            $("#" + datosInsercion[j].control).selectpicker("val", datosInsercion[j].defaultVal);
        }
    }


}
function Actualizar() { }
function mostrarCaptura() {

    $("#aPanel").click();
    if ($("#aPanel").hasClass("collapsed")) {
        $('a#aCapturar').html('<i class="material-icons">screen_rotation</i>Capturar');
    }
    else {

        $('a#aCapturar').html('<i class="material-icons">screen_lock_rotation</i>Ocultar');

    }
}
function Validar() {


    var _html = "";
    for (var j = 0; j < datosInsercion.length; j++) {

        if (datosInsercion[j].requerido === 1) {

            if ($("#" + datosInsercion[j].control).val() == "") {

                _html += "  *" + datosInsercion[j].nombre + " ";
            }
        }
    }
    return _html;
}
function cancelar() {

    $nuevo = 1;

    for (var j = 0; j < datosInsercion.length; j++) {
        if (datosInsercion[j].tipoControl === "Texto") {

            $("#" + datosInsercion[j].control).val(datosInsercion[j].defaultVal);
        }
        else if (datosInsercion[j].tipoControl === "Combo") {
            $("#" + datosInsercion[j].control).selectpicker("val", datosInsercion[j].defaultVal);
        }
    }




    var grid = $("#miGrid").data("kendoGrid");
    grid.clearSelection();
    $("#aPanel").click();
    if ($("#aPanel").hasClass("collapsed")) {
        $('a#aCapturar').html('<i class="material-icons">screen_rotation</i>Capturar');
    }
    else {

        $('a#aCapturar').html('<i class="material-icons">screen_lock_rotation</i>Ocultar');

    }
}
function rowclick(e) {
    e.preventDefault();
    var grid = $("#miGrid").data("kendoGrid");
    grid.clearSelection();
    $(e.currentTarget).closest("tr").addClass('k-state-selected');
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    if (dataItem) {
        $nuevo = 0;
        llenarDatos(dataItem);
        if ($("#aPanel").hasClass("collapsed")) {
            $("#aPanel").click();

        }

        $('a#aCapturar').html('<i class="material-icons">screen_lock_rotation</i>Ocultar');


    }
    else {
        swal("Ocurrio un Error al Seleccionar el Registro!", "", "error");

    }
    return false;

}

//////////////////////////////NUEVO////////////////////////


function porSerie() {
    var _tabla = $("#ddlTabla").val();
    var _tipo = $("#ddlTipoPoliza").val() == null ? "" : $("#ddlTipoPoliza").val().toString();
    var _recuperarCostos = recuperarMenu();
    if (_tipo == "") {

        swal("Busqueda por Serie", "Por Favor Seleccione un Tipo de Poliza", "warning"); return;

    }
    if (_recuperarCostos == "") {
        swal("Busqueda Por Serie", "Por Favor Seleccione Al Menos Un Movimiento", "warning"); return;
    }

    $.ajax({
        type: 'GET',
        url: _encabezado + "/Venta/obtenerFacturasSerie?IdMovimientosCostos=" + _recuperarCostos + "&TipoDePolizas=" + _tipo + "&IdTabla=" + _tabla,
        dataType: 'json',
        beforeSend: function () {
            var window = myWindow.data("kendoWindow");
            window.element.prev().find(".k-window-title").html("<b class='custom'>Facturas Por Costo</b>");
            $("#window").data("kendoWindow").center().open();

        },
        contentType: 'application/json; charset=utf-8',
        success: function (msg) {

            $("#window").data("kendoWindow").close();
            if (msg.Success == "OK") {
                swal("Busqueda Por Serie", "Busqueda Realizada Exitosamente", "success");
                $("#aPanel").click();
                Guardar();
                

                return;
            }
            else {
                swal("Busqueda Por Serie", msg.Data, "error"); return;
            }



        },
        error: function (x, status, error) {
            alert("Ocurrio un Error: " + status + "nError: " + x.responseText);
        }, async: true


    });

    return false;
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
function porFactura() {
    var _tabla = $("#ddlTabla").val();
    var _tipo = $("#ddlTipoPoliza").val() == null ? "" : $("#ddlTipoPoliza").val().toString();
    var _recuperarCostos = recuperarMenu();
    if (_tipo == "") {

        swal("Busqueda por Factura", "Por Favor Seleccione un Tipo de Poliza", "warning"); return;

    }
    if (_recuperarCostos == "") {
        swal("Busqueda Por Factura", "Por Favor Seleccione Al Menos Un Movimiento", "warning"); return;
    }

    $.ajax({
        type: 'GET',
        url: _encabezado + "/Venta/obtenerFacturasFactura?IdMovimientosCostos=" + _recuperarCostos + "&TipoDePolizas=" + _tipo + "&IdTabla=" + _tabla,
        dataType: 'json',
        beforeSend: function () {
            var window = myWindow.data("kendoWindow");
            window.element.prev().find(".k-window-title").html("<b class='custom'>Facturas Por Costo</b>");
            $("#window").data("kendoWindow").center().open();

        },
        contentType: 'application/json; charset=utf-8',
        success: function (msg) {

            $("#window").data("kendoWindow").close();
            if (msg.Success == "OK") {
                $("#aPanel").click();
                Guardar();
                swal("Busqueda Por Factura", "Busqueda Realizada Exitosamente", "success");

                return;
            }
            else {
                swal("Busqueda Por Factura", msg.Data, "error"); return;
            }



        },
        error: function (x, status, error) {
            alert("Ocurrio un Error: " + status + "nError: " + x.responseText);
        }, async: true


    });
    return false;
}

//////////////////////////////NUEVO////////////////////////
/////////////////////////// eliminar ligue///////////////////////
function removerPorSerie(serie, factura) {
    var _tabla = $("#ddlTabla").val();
    var window = myWindow.data("kendoWindow");
    window.element.prev().find(".k-window-title").html("<b class='custom'>Facturas Por Costo</b>");
    $("#window").data("kendoWindow").center().open();
    var _logs = "";
    $('.chkPer').each(function (index, obj) {

        if (this.checked === true) {
            var _conspol = $("#" + this.id).attr("conspol");
            var _consmov = $("#" + this.id).attr("consmov");
            var _movmes = $("#" + this.id).attr("movmes");
            var _tipopol = $("#" + this.id).attr("tipopol");

            //var _resultado = remover(_tabla, _conspol.toString().trim(), _consmov.toString().trim(), _movmes.toString().trim(),_tipopol,0,1);
            $.ajax({
                type: 'GET',
                url: _encabezado + "/Venta/desligarCorrecion?idTabla=" + _tabla + "&movConsPol=" + _conspol.toString().trim() + "&movConsMov=" + _consmov.toString().trim() +
                     "&movMes=" + _movmes.toString().trim() + "&movTipoPol=" + _tipopol + "&eliminarFactura=" + factura + "&eliminarSerie=" + serie,
                dataType: 'json',
                beforeSend: function () {
                },
                contentType: 'application/json; charset=utf-8',
                success: function (msg) {
                    if (msg.Success != "OK") _logs += _resultado;
                },
                error: function (x, status, error) {
                    return ("Ocurrio un Error: " + status + "nError: " + x.responseText);
                }, async: false
            });
        }
    });
    $("#window").data("kendoWindow").close();
    if (_logs == "") {
        swal("Remover Correcciones por " + (serie == 1 ? "Serie" : "Factura"), "Correciones Removidas Exitosamente", "success");
        $("#aPanel").click();
        Guardar();
        $('#ddlFiltroSerie').selectpicker('val', "");
        $('#ddlFiltroSerie').selectpicker('refresh')
        $('#ddlFiltroFactura').selectpicker('val', "");
        $('#ddlFiltroFactura').selectpicker('refresh')


    }
    else {
        swal("Remover Correcciones por " + (serie == 1 ? "Serie" : "Factura"), _logs, "error");

    }
    return false;
}
function Corregir(aplicaFactura, aplicaSerie, tipopol, conspol, consmov, movmes, idmovimiento) {

    var window2 = myWindow2.data("kendoWindow");
    if (aplicaFactura == 1) {
        window2.element.prev().find(".k-window-title").html("<b class='custom'>Corregir Factura</b>");
        $("#lblCapturaCorregir").html("Factura");

    }
    else {
        window2.element.prev().find(".k-window-title").html("<b class='custom'>Corregir Serie</b>");
        $("#lblCapturaCorregir").html("Serie");
    }

    $("#progressGuardar").hide();

    $("#hdIdMovimiento").val(idmovimiento);
    $("#hdFactura").val(aplicaFactura);
    $("#txtCapturaCorregir").val("");
    $("#txtMovTipoPol").val(tipopol);
    $("#txtConsPol").val(conspol);
    $("#txtConsMov").val(consmov);
    $("#txtMovMes").val(movmes);
    if (window2.element.is(":hidden")) $("#window2").data("kendoWindow").center().open();
    return false;
}
function corregirGuardar() {
    var _tabla = $("#ddlTabla").val();
    if ($("#hdIdMovimiento").val().toString().trim() == "0") {
        swal("Corregir", "Por Favor Seleccione un Elemento a Corregir", "warning");
        return false;
    }
    if ($("#txtCapturaCorregir").val().toString().trim() == "") {
        swal("Corregir " + ($("#hdFactura").val() == "1" ? "Factura" : "Serie"), "Por Favor Ingrese un Valor Valido", "warning");
        return false;
    }
    else {

        var _conspol = $("#txtConsPol").val();
        var _consmov = $("#txtConsMov").val();
        var _movmes = $("#txtMovMes").val();
        var _tipopol = $("#txtMovTipoPol").val();


        var _aplicaFactura = 0;
        var _aplicaSerie = 0;
        var _serie = "";
        var _factura = ";"
        if ($("#hdFactura").val() == "1") {
            _aplicaFactura = 1;
            _aplicaSerie = 0;
            _factura = $("#txtCapturaCorregir").val().toString().trim();

        }
        else {
            _aplicaFactura = 0;
            _aplicaSerie = 1;
            _serie = $("#txtCapturaCorregir").val().toString().trim();
        }


        $.ajax({
            type: 'GET',
            url: _encabezado + "/Costo/correcion?idTabla=" + _tabla + "&movConsPol=" + _conspol.toString().trim() + "&movConsMov=" + _consmov.toString().trim() +
                 "&movMes=" + _movmes.toString().trim() + "&movTipoPol=" + _tipopol + "&aplicaFactura=" + _aplicaFactura + "&aplicaSerie=" + _aplicaSerie
            + "&Factura=" + _factura + "&Serie=" + _serie
            ,
            dataType: 'json',
            beforeSend: function () {
                $("#divprogressGuardarxx").show();
                $(".btnCorregir").attr("disabled", "disabled");
            },
            contentType: 'application/json; charset=utf-8',
            success: function (msg) {
                $("#divprogressGuardarxx").hide();
                $(".btnCorregir").removeAttr("disabled");
                if (msg.Success == "OK") {

                    var table = $('.js-exportable').DataTable();
                    if ($("#hdFactura").val() == "1") {

                        table.cell($("#tdFactura" + $("#hdIdMovimiento").val())).data(_factura).draw();
                        $("#aFactura" + $("#hdIdMovimiento").val()).hide();



                    }
                    else {
                        table.cell($("#tdSerie" + $("#hdIdMovimiento").val())).data(_serie).draw();
                        $("#aSerie" + $("#hdIdMovimiento").val()).hide();

                    }
                    swal("Corregir " + ($("#hdFactura").val() == "1" ? "Factura" : "Serie"), "Registro Corregido Exitosamente", "success");
                }
                else {
                    swal("Corregir " + ($("#hdFactura").val() == "1" ? "Factura" : "Serie"), "Ocurrio un Error al Intentar corregir " + msg.Data, "error");
                }
                $("#hdIdMovimiento").val("0");
                $("#hdFactura").val("0");
                $("#txtCapturaCorregir").val("");
                $("#txtMovTipoPol").val("");
                $("#txtConsPol").val("");
                $("#txtConsMov").val("");
                $("#txtMovMes").val("");

            },
            error: function (x, status, error) {
                return ("Ocurrio un Error: " + status + "nError: " + x.responseText);
            }, async: true
        });
    }

}


function cerrarVentanaCorregir() {
    $("#window2").data("kendoWindow").close();
    return false;
}
/////////////////////////// eliminar ligue///////////////////////