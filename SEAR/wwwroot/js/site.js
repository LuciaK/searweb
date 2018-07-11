// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {

    var idMensajeModal = "mensaje-modal";
    var idFormConfig = "form-config";

    $("#save").submit(function (e) {
        e.preventDefault();
    });
    

    $('#save').click(function () {

        var validTime=validPorcion = false;
        var hora1 = document.getElementById("hora1").value;
        var hora2 = document.getElementById("hora2").value;
        var hora3 = document.getElementById("hora3").value;
        var hora4 = document.getElementById("hora4").value;

        var porcion = document.getElementById("cantidadPorcion").value;


        if (validateTime(hora1) && validateTime(hora2) && validateTime(hora3) && validateTime(hora4)) {

            validTime = true;

        } else {

            getErrorMessage(idMensajeModal, "El formato permitido de los horarios es HH:MM");

        }

        if (validateNumber(porcion)) {

            validPorcion = true;

        } else {
            getErrorMessage(idMensajeModal, "La porción debe ser como mínimo 1 y el valor debe numérico");
        }

        if (validTime && validPorcion ) {
            submitConfiguration(idFormConfig, idMensajeModal);
        }
        
        
    });

    $("#saveConf").submit(function (e) {
        e.preventDefault();
    });


    $('#saveConf').click(function () {

        var url = document.getElementById("ip").value.trim();

        if (url.substr(-1) == '/') {
            url = url.substr(0, url.length - 1);
        }

        var modo = $("input:radio[name=modo]:checked").val()
        var urlModo = url + "/" + modo;


    /*   $.get(urlModo, function (data, status) {
           document.getElementById("mensaje").innerHTML = data + " Status: " + status;
           });
    */
        try {
            window.open(urlModo);
        }
        catch (e) {

            document.getElementById("mensaje").innerHTML = e.message;

        }
       



    });

});

function validateTime(strTime) {

    var regex = new RegExp("^([0-1][0-9]|2[0-3]):([0-5][0-9])$");

    if (regex.test(strTime) || strTime === "") {
        return true;
    } else {
        return false;
    }

}

function validateNumber(value) {

    var regex = new RegExp("^([1-9])$");

    if (regex.test(value)) {
        return true;
    } else {
        return false;
    }

}

function submitConfiguration(idFormConfig,idMensajeModal) {

    if ($('#' + idMensajeModal).hasClass("alert-danger"))
        document.getElementById(idMensajeModal).classList.remove("alert-danger");

    document.getElementById(idMensajeModal).classList.add("alert-success");
    document.getElementById(idMensajeModal).style.display = "block";
    document.getElementById(idMensajeModal).innerHTML = "Se ha guardado correctamente";
    document.getElementById(idFormConfig).submit();

}

function getErrorMessage(idMensajeModal, errorMensaje) {

    if ($('#' + idMensajeModal).hasClass("alert-success"))
        document.getElementById(idMensajeModal).classList.remove("alert-success");

    document.getElementById(idMensajeModal).classList.add("alert-danger");
    document.getElementById(idMensajeModal).innerHTML = errorMensaje;
    document.getElementById(idMensajeModal).style.display = "block";

}