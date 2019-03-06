document.forms[0].addEventListener('submit', function (event) {
    let result = true;

    const startDate = Date.parse(document.querySelector('#StartDate').value);
    const startDateError = document.querySelector('#start-date-error');
    startDateError.innerHTML = '';
    if (isNaN(startDate)) {
        startDateError.innerHTML = 'Required format: mm-dd-yy (hh-mm)';
        result = false;
    }
    else if (startDate < Date.now()) {
        startDateError.innerHTML = 'Need to be bigger now';
        result = false;
    }

    const finishDate = Date.parse(document.querySelector('#FinishDate').value);
    const finishDateError = document.querySelector('#finish-date-error');
    finishDateError.innerHTML = '';
    if (isNaN(finishDate)) {
        finishDateError.innerHTML = 'Required format: mm-dd-yy (hh-mm)';
        result = false;
    }

    if (result) {
        const time = (finishDate - startDate) / 1000 / 60 / 60;
        if (time < 1) {
            finishDateError.innerHTML = 'Min time is 1 h';
            result = false;
        }
        else if (time > 168) {
            finishDateError.innerHTML = 'Max time is 1 week';
            result = false;
        }
    }

    const passportNumberError = document.querySelector('#passport-number-error');
    passportNumberError.innerHTML = '';
    if (!/\d{9}/.test(document.querySelector('#PassportNumber').value)) {
        passportNumberError.innerHTML = 'Need 9 numbers';
        result = false;
    }

    if (!result) {
        event.preventDefault();
    }
});