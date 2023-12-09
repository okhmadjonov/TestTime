function validateTitle(inputElement, alertId) {
    var pattern = /^[a-zA-Z\s.]*$/;
    var inputValue = inputElement.value;

    if (!pattern.test(inputValue)) {
        showValidationAlert(alertId);
        inputElement.value = inputValue.replace(/[^a-zA-Z\s.]/g, '');
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
}




document.addEventListener('focusin', function (e) {
    hideValidationAlert('titleAlert');
    hideValidationAlert('priceAlert');
    hideValidationAlert('quantityAlert');
    hideValidationAlert('titleUpdateAlert');
    hideValidationAlert('priceUpdateAlert');
    hideValidationAlert('quantityUpdateAlert');
});