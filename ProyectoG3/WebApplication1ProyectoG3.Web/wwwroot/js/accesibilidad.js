$(document).ready(function () {

    let fontSize = localStorage.getItem("fontSize") || 100;
    $("body").css("font-size", fontSize + "%");

    $("#btnAumentar").click(function () {
        fontSize = parseInt(fontSize) + 10;
        $("body").css("font-size", fontSize + "%");
        localStorage.setItem("fontSize", fontSize);
    });

    $("#btnDisminuir").click(function () {
        fontSize = parseInt(fontSize) - 10;

        if (fontSize < 70) {
            fontSize = 70;
        }

        $("body").css("font-size", fontSize + "%");
        localStorage.setItem("fontSize", fontSize);
    });

});