function validateTitle(inputElement, alertId) {
    var pattern = /^[a-zA-Z0-9\s.]*$/;
    var inputValue = inputElement.value;

    if (!pattern.test(inputValue)) {
        showValidationAlert(alertId);
    } else {
        hideValidationAlert(alertId);
    }
}

function validateQuantity(inputElement, alertId) {
    var pattern = /^[0-9]*$/;
    var inputValue = inputElement.value;
    if (!pattern.test(inputValue)) {
        showValidationAlert(alertId);
        inputElement.value = inputValue.replace(/[^0-9]/g, '');
    } else {
        hideValidationAlert(alertId);
    }
    var quantity = parseFloat(inputElement.value);

    if (isNaN(quantity) || quantity <= 0) {
        displayValidationAlert(alertId);
        input.value = ''; // Clear the invalid value
    } else {
        hideValidationAlert(alertId);
    }
}



function validatePrice(inputElement, alertId) {

    var pattern = /^[0-9]*$/;
    var inputValue = inputElement.value;
    if (!pattern.test(inputValue)) {
        showValidationAlert(alertId);
        inputElement.value = inputValue.replace(/[^0-9]/g, '');
    } else {
        hideValidationAlert(alertId);
    }
    var quantity = parseFloat(inputElement.value);

    if (isNaN(quantity) || quantity <= 0) {
        displayValidationAlert(alertId);
        input.value = ''; // Clear the invalid value
    } else {
        hideValidationAlert(alertId);
    }

}



function showValidationAlert(alertId) {
    var alertElement = document.getElementById(alertId);
    console.log(alertElement);
    alertElement.style.display = 'block';
}

function hideValidationAlert(alertId) {
    var alertElement = document.getElementById(alertId);
    alertElement.style.display = 'none';
}

document.addEventListener('focusin', function (e) {
    hideValidationAlert('titleAlert');
    hideValidationAlert('quantityAlert');
    hideValidationAlert('priceAlert');
    hideValidationAlert('titleUpdateAlert');
    hideValidationAlert('quantityUpdateAlert');
    hideValidationAlert('priceUpdateAlert');
});
