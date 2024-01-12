
document.addEventListener("DOMContentLoaded", function () {


});


// Validación dinámica de contraseña en el cliente, para que el usuario pueda ir viendo de forma dinámica
function validatePassword(password) {
    //console.log("La función validatePassword se está ejecutando.");

    const indicator = document.getElementById('password-indicator');
    const requirements = indicator.querySelectorAll('li');
    let validations = 0;

    //6 caracteres
    if (password.length >= 6) {
        validations++;
        requirements[0].classList.add('complete');
    } else {
        validations--;
        requirements[0].classList.remove('complete');
    }

    // 1 mayúscula
    if (/[A-Z]/.test(password)) {
        validations++;
        requirements[1].classList.add('complete');
    } else {
        validations--;
        requirements[1].classList.remove('complete');
    }

    // 1 minúscula
    if (/[a-z]/.test(password)) {
        validations++;
        requirements[2].classList.add('complete');
    } else {
        validations--;
        requirements[2].classList.remove('complete');
    }

    //  1 número
    if (/\d/.test(password)) {
        validations++;
        requirements[3].classList.add('complete');
    } else {
        validations--;
        requirements[3].classList.remove('complete');
    }

    // 1 no alfanumerico
    if (/[^a-zA-Z0-9]/.test(password)) {
        validations++;
        requirements[4].classList.add('complete');
    } else {
        validations--;
        requirements[4].classList.remove('complete');
    }

    // validación
    if (validations >= 5) { 
        indicator.style.display = 'none';
        document.getElementById('submit').disabled = false;
    } else {
        indicator.style.display = 'block';
        document.getElementById('submit').disabled = true;
    }

}



// Funcionalidad de mostrar contraseña
function pwOjito(inputId) {
    const passwordInput = document.getElementById(inputId);
    const passwordIcon = document.querySelector(`#${inputId} + .pw-ojito`);

    if (passwordInput.type === 'password') {
        passwordInput.type = 'text';
        passwordIcon.classList.remove('fa-eye');
        passwordIcon.classList.add('fa-eye-slash');
    } else {
        passwordInput.type = 'password';
        passwordIcon.classList.remove('fa-eye-slash');
        passwordIcon.classList.add('fa-eye');
    }
}






// AJAX de Tarea
function updateTarea(button, email) {
    var form = button.closest('form');
    var actionUrl = form.dataset.url;
    var idTarea = form.dataset.idTarea;
    var returnUrl = form.querySelector('input[name="returnUrl"]').value;


    //  solicitud AJAX
    var formData = new FormData();
    formData.append('idTarea', idTarea);
    formData.append('returnUrl', returnUrl);
    var xhr = new XMLHttpRequest();
    xhr.open('POST', actionUrl, true);
    xhr.setRequestHeader('RequestVerificationToken', document.querySelector('input[name="__RequestVerificationToken"]').value);


    // Se cambian los datos de visualización solo en el front para que no se tenga que volver a cargar la página y aplicar la lógica del .cshtml
    xhr.onload = function () {
        if (xhr.status === 200) {


            // Texto y clase del botón
            var buttonText = button.textContent.trim() === "Anotarme" ? "Desanotarme" : "Anotarme";
            button.textContent = buttonText;

            if (buttonText === "Desanotarme") {
                button.classList.remove("btn-primary");
                button.classList.add("btn-danger");
            } else {
                button.classList.remove("btn-danger");
                button.classList.add("btn-primary");
            }

            var responsableElement = document.getElementById("responsable-" + idTarea);

            if (buttonText === "Desanotarme") {
                responsableElement.innerHTML = "Responsable: <strong>" + email + "</strong>";
            } else {
                responsableElement.innerHTML = "Responsable: <strong>No asignado</strong>";
            }

        } else {
            console.error('este es el error:', xhr.responseText);
        }
    };

    xhr.send(formData);
}







// AJAX Tarea para Detalles de Usuario
function Desanotar(button) {
    var form = button.closest('form');
    var idTarea = form.dataset.idTarea;
    var actionUrl = form.dataset.url;
    var returnUrl = form.querySelector('input[name="returnUrl"]').value;

    var formData = new FormData();
    formData.append('idTarea', idTarea);
    formData.append('returnUrl', returnUrl);

    var xhr = new XMLHttpRequest();
    xhr.open('POST', actionUrl, true);
    xhr.setRequestHeader('RequestVerificationToken', document.querySelector('input[name="__RequestVerificationToken"]').value);

    xhr.onload = function () {
        if (xhr.status === 200) {
            var tareaContenedor = document.getElementById("tarea-container-" + idTarea);
            if (tareaContenedor) {
                tareaContenedor.remove();
            }
        } else {
            console.error('este es el error:', xhr.responseText);
        }
    };

    xhr.send(formData);
}



