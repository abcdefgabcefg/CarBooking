document.forms[0].addEventListener('submit', function (event) {
    let result = true;

    const carTitle = document.querySelector('#CarTitle').value;
    const carTitleError = document.querySelector('#car-title-error');
    carTitleError.textContent = '';

    if (carTitle === '') {
        carTitleError.textContent = 'Required field';
        result = false;
    }
    else if (carTitle.length > 60) {
        carTitleError.textContent = 'Max lenght is 60 characters';
        result = false;
    }

    const price = document.querySelector('#Price').value;
    const priceError = document.querySelector('#price-error');
    priceError.textContent = '';

    if (price === '') {
        priceError.textContent = 'Required field';
        result = false;
    }
    else if (isNaN(price)) {
        priceError.textContent = 'Need to be number';
        result = false;
    }
    else if (price <= 0) {
        priceError.textContent = 'Need to be grater than zero';
        result = false;
    }

    const imagePath = document.querySelector('#ImagePath').value;
    const imagePathError = document.querySelector('#image-path-error');
    imagePathError.textContent = '';

    if (imagePath === '') {
        imagePathError.textContent = 'Required field';
        result = false;
    }
    else if (imagePath.length > 300) {
        imagePathError.textContent = 'Max lenght is 300 characters';
        result = false;
    }

    if (!result) {
        event.preventDefault();
    }
})