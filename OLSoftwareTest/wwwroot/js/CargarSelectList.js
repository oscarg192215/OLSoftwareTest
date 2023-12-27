$(document).ready(function () {
    $('#TiposPruebas').attr('disabled', true);
    $('#LenguajesProgramacion').attr('disabled', true);
    $('#NivelesConocimiento').attr('disabled', true);
    LoadTiposPruebas();

    $("#TiposPruebas").change(function () {
        $('#NivelesConocimiento').empty();
        $('#LenguajesProgramacion').empty();
        $("#Preguntas").empty();
        $('#NivelesConocimiento').attr('disabled', true);
        LoadNivelesConocimiento();
    });

    $("#NivelesConocimiento").change(function () {
        $('#LenguajesProgramacion').empty();
        $("#Preguntas").empty();
        LoadLenguajesProgramacion();
    });

    $("#LenguajesProgramacion").change(function () {
        $("#Preguntas").empty();
        LoadPreguntas();
        SetPregunta();
    });
});

function SetPregunta() {    

    var preguntaSeleccionada = []
    $('input[type=checkbox]:checked').each(function () {
        preguntaSeleccionada.push($(this).prop('id'));
    });

    var obj = { IdPregunta: preguntaSeleccionada.toString() }; 

    $.ajax({
        type: "GET",
        url: "/Pruebas/SetPreguntas",
        data: obj,
        success: function (data) {            
        },
        error: function (error) {
            alert(error);
        }
    });
}


function LoadTiposPruebas() {
    $('#TiposPruebas').empty();
    $.ajax({
        type: "GET",
        url: "/Pruebas/GetTiposPruebas",
        data: "{}",
        success: function (data) {
            if (data != null && data != undefined && data.length > 0) {
                $('#TiposPruebas').attr('disabled', false);
                $('#TiposPruebas').append('<option>Seleccione...</option>');
                $('#LenguajesProgramacion').append('<option>Seleccione...</option>');
                $('#NivelesConocimiento').append('<option>Seleccione...</option>');
                var s = '<option value="-1">Seleccione...</option>';
                for (var i = 0; i < data.length; i++) {
                    console.log(data[i])
                    s += '<option value="' + data[i].id_tipo_prueba + '">' + data[i].nombre_tipo_prueba + '</option>';
                }
                $("#TiposPruebas").html(s);
            }
            else {
                $('#TiposPruebas').attr('disabled', true);
                $('#LenguajesProgramacion').attr('disabled', true);
                $('#NivelesConocimiento').attr('disabled', true);
                $('#TiposPruebas').append('<option>Sin registros...</option>');
                $('#LenguajesProgramacion').append('<option>Sin registros...</option>');
                $('#NivelesConocimiento').append('<option>Sin registros...</option>');
            }
        },
        error: function (error) {
            alert(error);
        }
    });    
}

function LoadNivelesConocimiento() {
    $('#NivelesConocimiento').empty();
    $('#LenguajesProgramacion').empty();
    $("#Preguntas").empty();
    $('#LenguajesProgramacion').attr('disabled', true);

    $.ajax({
        type: "GET",
        url: "/Pruebas/GetNivelesConocimiento",
        data: "{}",
        success: function (data) {
            if (data != null && data != undefined && data.length > 0) {
                $('#NivelesConocimiento').attr('disabled', false);
                $('#NivelesConocimiento').append('<option>Seleccione...</option>');
                $('#LenguajesProgramacion').append('<option>Seleccione...</option>');

                var s = '<option value="-1">Seleccione...</option>';
                for (var i = 0; i < data.length; i++) {
                    console.log(data[i])
                    s += '<option value="' + data[i].id_nivel + '">' + data[i].nombre_nivel + '</option>';
                }
                $("#NivelesConocimiento").html(s);
            }
            else {
                $('#TiposPruebas').attr('disabled', true);
                $('#LenguajesProgramacion').attr('disabled', true);
                $('#NivelesConocimiento').attr('disabled', true);
                $('#TiposPruebas').append('<option>Sin registros...</option>');
                $('#LenguajesProgramacion').append('<option>Sin registros...</option>');
                $('#NivelesConocimiento').append('<option>Sin registros...</option>');
            }
        },
        error: function (error) {
            alert(error);
        }
    });
};

function LoadLenguajesProgramacion() {
    $('#LenguajesProgramacion').empty();

    $.ajax({
        type: "GET",
        url: "/Pruebas/GetLenguajesProgramacion",
        data: "{}",
        success: function (data) {
            if (data != null && data != undefined && data.length > 0) {
                $('#LenguajesProgramacion').attr('disabled', false);
                $('#LenguajesProgramacion').append('<option>Seleccione...</option>');

                var s = '<option value="-1">Seleccione...</option>';
                for (var i = 0; i < data.length; i++) {
                    console.log(data[i])
                    s += '<option value="' + data[i].id_lenguaje + '">' + data[i].nombre_lenguaje + '</option>';
                }
                $("#LenguajesProgramacion").html(s);
            }
            else {
                $('#TiposPruebas').attr('disabled', true);
                $('#LenguajesProgramacion').attr('disabled', true);
                $('#NivelesConocimiento').attr('disabled', true);
                $('#TiposPruebas').append('<option>Sin registros...</option>');
                $('#LenguajesProgramacion').append('<option>Sin registros...</option>');
                $('#NivelesConocimiento').append('<option>Sin registros...</option>');
            }
        },
        error: function (error) {
            alert(error);
        }
    });
};

function LoadPreguntas() {
    $("#Preguntas").empty();

    var IdLenguaje = $('#LenguajesProgramacion').val();    
    var obj = { IdLenguaje: $('#LenguajesProgramacion').val(), IdNivel: $('#NivelesConocimiento').val() };  

    $.ajax({
        type: "GET",
        url: "/Pruebas/GetPreguntas",
        data: obj,
        success: function (data) {
            if (data != null && data != undefined && data.length > 0) {
                $("#Preguntas").html("");
                for (var i = 0; i < data.length; i++) {
                    console.log(data[i])
                    $("#Preguntas").append("<table width='100%'><tr><td><input type='checkbox' onclick='SetPregunta()' id='" + data[i].id_pregunta + "' name='" + data[i].contenido_pregunta + "' /> </td><td> " + data[i].contenido_pregunta + "</td></tr ></table > ");                    
                }
                SetPregunta();
            }
            else {
                $('#Preguntas').empty();
                $('#Preguntas').append('');
            }
        },
        error: function (error) {
            alert(error);
        }
    });
};