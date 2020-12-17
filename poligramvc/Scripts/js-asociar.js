function editar(idh) {

    $('#IdHN').val(idh);

    $('#myModal').modal('show');
}

function asociar() {
    var idh = $("#IdHN").val();
    var poli = $("#idpol").val();

    //alert(idh + ' ' + poli);

    $.ajaxSetup({ async: true });

    $.post("js_asociar", { p_idHistorico: idh, p_idEvaluador: poli }, function (data) {
        if (data == "SESION_EXPIRED") { showReiniciarSesion(); }
        else if (data == "Ok") {
            $('#myModal').modal('hide');
            swal({
                title: "Evaluado Asociado / Actualizado",
                text: "Haga click para continuar",
                type: "success"
            });
        }
        else { alert("Ocurrio un error, intente de nuevo."); }
        //else { showMessage("Ocurrio un error, intente de nuevo.", "#msgBandeja", "danger", "warning"); }
    })
    .fail(function (result) {
        showMessage("Ocurrio un error:" + result.statusText + ", intente de nuevo. ", "#msgBandeja", "danger", "warning");
    });
}
