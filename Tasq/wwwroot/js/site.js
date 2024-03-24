

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



// Funcion para mostrar contraseña cambiando el tipo del input field y la clase del Ojito
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






// AJAX de Actualizar Tarea, se toma la información del from y su acción para hacer una solicitud AJAX para ejecutar la función en el back, y en caso de que sea exitosa se actualizan los elementos del front end
function updateTarea(button, email) {
    var form = button.closest('form'); // encuentra el formulario más cercano al botón que llamó a a función
    var actionUrl = form.dataset.url; // obtiene el url a donde mandar la solicitud AJAX, éste fue especificado con el tag data-url en el botón
    var idTarea = form.dataset.idTarea; // obtiene el id de la tarea desde el tag data del formulario
    var returnUrl = form.querySelector('input[name="returnUrl"]').value; // obtiene el url de retorno desde un hidden input del form


    //  solicitud AJAX
    var formData = new FormData(); // objeto al que se le appendea el id de la tarea y el url de retorno de manera tipo de diccionario
    formData.append('idTarea', idTarea);
    formData.append('returnUrl', returnUrl); // los mismos nombres que los argumentos de la función de TareaController
    var xhr = new XMLHttpRequest(); // se crea un objeto de éste tipo, el cual se confgura para mandar una solicitud POST a actionUrl
    xhr.open('POST', actionUrl, true); // El primer argumento indica el método HTTP (POST), el segundo es la URL a la que se enviará la solicitud, y el tercer argumento (true) indica que la solicitud será asíncrona.
    xhr.setRequestHeader('RequestVerificationToken', document.querySelector('input[name="__RequestVerificationToken"]').value); // Se establece un encabezado en la solicitud para incluir el token de verificación de la solicitud (importante para la seguridad).


    // Se cambian los datos de visualización solo en el front para que no se tenga que volver a cargar la página y aplicar la lógica del .cshtml
    xhr.onload = function () { // Éste es el manejador de eventos del objeto tipo XMLHttpRequest xml, el cual hará que se ejecute la función juna vez que se reciba una respuesta
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







// AJAX Actualizar Tarea para Detalles de Usuario, en este caso si la solicitud es exitosa, se elimina el elemento de la interfaz, ya que se dejará de mostrar como las tareas en las que el usuario es responsable
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



