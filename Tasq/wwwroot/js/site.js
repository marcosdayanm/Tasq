
document.addEventListener("DOMContentLoaded", function () {



    // Esto es para añadir a todos los inputs el autocomplete=off
    var inputs = document.getElementsByTagName('input');
    for (var i = 0; i < inputs.length; i++) {
        inputs[i].setAttribute('autocomplete', 'off');
    }
});