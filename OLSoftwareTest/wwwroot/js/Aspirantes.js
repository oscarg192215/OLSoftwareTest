$(document).ready(function () {
    var fechaInicio = $("#fecha_inicio").val();
   
    if (fechaInicio != "") {

        var fechaActual = new Date();
        fechaActual = new Intl.DateTimeFormat("fr-CA", { year: "numeric", month: "2-digit", day: "2-digit" }).format(new Date(fechaActual))
        $("#fecha_inicio").attr('min', fechaActual);

        var fechaFormateadaInicio = new Date(fechaInicio);
        fechaFormateadaInicio.setDate(fechaFormateadaInicio.getDate() + 1);
        fechaFormateadaInicio = new Intl.DateTimeFormat("fr-CA", { year: "numeric", month: "2-digit", day: "2-digit" }).format(new Date(fechaFormateadaInicio))
        
        $("#fecha_finalizacion").attr('min', fechaFormateadaInicio);
        console.log(fechaFormateadaInicio);
    }

    $("#fecha_inicio").change(function () {
        $("#fecha_finalizacion").attr('min', this.value).val(this.value);
    });

    $("#id_prueba").change(function () {       
        var texto = $(this).find('option:selected').text();
        $("#nombre_prueba").val(texto);
    });
});