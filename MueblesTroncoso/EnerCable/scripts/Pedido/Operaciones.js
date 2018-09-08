

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
            , cargar: loadGrid
            , actualizar: Actualizar
            , tooglePanel: mostrarCaptura
            , cancelar: cancelar
        }
    });

    view.cargar();
    view.load();
});

function Eliminar() { }
function Guardar() {

    var _seccion = $("#ddlTipoBusqueda").val();
    switch (_seccion) {
        case "1":
            if (!($("#txtFechaInicio").val() != "" && $("#txtFechaFin").val() != "")) {
                swal("Busqueda por Fechas", "Por Favor Ingrese el Rango de Fechas", "warning"); return;
            }
            break;

        case "2":
            if ($("#txtFolioPedido").val() == "") {
                swal("Busqueda por Folio", "Por Favor Ingrese el Folio", "warning"); return;
            } break;
        case "3": if ($("#txtCliente").val() == "") {
            swal("Busqueda por Cliente", "Por Favor Ingrese el Cliente", "warning"); return;
        } break;
    }

    $("#miGrid").data("kendoGrid").dataSource.read();
    $("#aPanel").click();
    //var _html = Validar();

    //if (_html != "") {
    //    swal("Por Favor Rellene Los Campos Obligatorios!", _html, "warning");
    //    return;
    //}

    //try {
    //    var url = _encabezado + _accion;
    //    recuperarDatos();
    //    var self = this;
    //    axios.post(url, datosAccion)
    //            .then(function (response) {
    //                if (response.data.Result == "OK") {

    //                    if ($nuevo == 1) swal("Registro Guardado Exitosamente!", "", "success");
    //                    else swal("Registro Guardado Exitosamente!", "", "success");
    //                    cancelar();
    //                    $("#miGrid").data("kendoGrid").dataSource.read();
    //                }
    //                else {
    //                    swal("Ocurrio un Error!", response.data.Result, "error");
    //                }
    //            })
    //            .catch(function (error) {
    //                swal("Ocurrio un Error!", error, "error");
    //            });



    //} catch (ex) {
    //    alert(ex);
    //}

    return false;

}
function Load() {
    $('.combo').selectpicker();
    $('#ddlTipoBusqueda').on('change', function (e) {
        var _seccion = $(this).find("option:selected").val();
        $(".opciones").hide();

        $("#txtFechaInicio").val("");
        $("#txtFechaFin").val("");
        $("#txtFolioPedido").val("");
        $("#txtCliente").val("");


        switch (_seccion) {
            case "1": $("#rowFecha").show(); break;
            case "2": $("#rowFolio").show(); break;
            case "3": $("#rowCliente").show(); break;
        }





    });
    $('.datepicker').bootstrapMaterialDatePicker({
        format: 'DD-MM-YYYY',
        clearButton: true,
        weekStart: 1,
        time: false
    });

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
function loadGrid() {

    $("#miGrid").kendoGrid({
        dataSource: new kendo.data.DataSource({
            type: "get",
            schema: {
                model: {
                    fields:
                        datosGrid

                }
            },

            transport: {
                read: {
                    url: _encabezado + _accionGrid,
                    contentType: "application/json; charset=utf-8", // optional
                    dataType: "json",
                    data: function () {
                        return {

                            tipoBusqueda: $("#ddlTipoBusqueda").val()
                            , fechaInicial: $("#txtFechaInicio").val()
                            , fechaFinal: $("#txtFechaFin").val()
                            , folio: $("#txtFolioPedido").val()
                            , cliente: $("#txtCliente").val()
                        };
                    }
                }




            }, pageSize: 20
        }),
        height: 550,
        dataBound: function () {
            gridx = this;
            gridx.tbody.find('tr').each(function () {
                var item = gridx.dataItem(this);
                kendo.bind(this, item);
            });
        },
        filterable: {
            mode: "row"
        },
        sortable: true,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        //  editable: true,
        reorderable: true,
        resizable: true,
        //columnMenu: true,
        selectable: "single",
        scrollable: true,

        columns: columnasGrid

    });
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