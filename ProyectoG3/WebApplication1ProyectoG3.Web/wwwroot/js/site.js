$(document).ready(function () {

    // ENTER pasa al siguiente campo
    $("input").on("keydown", function (e) {
        if (e.key === "Enter") {
            e.preventDefault();
            var inputs = $("input:visible");
            var index = inputs.index(this);
            inputs.eq(index + 1).focus();
        }
    });

    // SPACE o ENTER en botones
    $(document).on("keydown", "button", function (e) {
        if (e.key === "Enter" || e.key === " ") {
            e.preventDefault();
            $(this).click();
        }
    });

});


//  Mensaje accesible 
function mostrarMensaje(mensaje) {
    $("#mensajeAccesible").text(mensaje);
}