document.forms[0].addEventListener('submit', function (event) {
    let result = true;
    const login = document.querySelector('#Login').value;
    const loginError = document.querySelector('#login-error');
    loginError.innerHTML = '';
    if (login == '') {
        loginError.innerHTML = 'Required field<br>';
        result = false;
    }
    if (login.length > 50) {
        loginError.innerHTML += 'Max length is 50 characters';
        result = false;
    }

    const password = document.querySelector('#Password').value;
    const passwordError = document.querySelector('#password-error');
    passwordError.innerHTML = '';
    if (password == '') {
        passwordError.innerHTML = 'Required field<br>';
        result = false;
    }
    if (password.length < 4) {
        passwordError.innerHTML += 'Min length is 4 characters<br>';
        result = false;
    }
    if (password.length > 50) {
        passwordError.innerHTML += 'Max length is 50 characters';
        result = false;
    }

    if (!result) {
        event.preventDefault();
        const summaryErrors = document.querySelector('.validation-summary-errors ul');
        if (summaryErrors !== null)
            summaryErrors.innerHTML = '';
    }
});