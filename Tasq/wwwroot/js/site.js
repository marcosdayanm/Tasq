
document.addEventListener("DOMContentLoaded", function () {


});



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





