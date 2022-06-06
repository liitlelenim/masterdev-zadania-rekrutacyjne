var peselInputField = $("#pesel-input");
var birthYearInput = $("#birth-year-input");
var plecInput = $("#plec-input");
var plecPlaceholder = $("#plec-placeholder")
var peselValue = peselInputField.val();

peselToFields();
plecInput.hide();
peselInputField.keyup((event) => {
    peselValue = peselInputField.val();
    peselToFields();
});


function peselToFields() {
    if (peselValue.length >= 11) {
        peselInputField.blur();
        birthYearInput.val(calculateBirthYearBasedOnPesel(peselValue));
        plecInput.val(getPlecFromPesel(peselValue));
        plecPlaceholder.val(getPlecFromPesel(peselValue) == 0 ? "Mężczyzna" : "Kobieta");
    }
}

function calculateBirthYearBasedOnPesel(pesel) {
    var firstTwoDigits;
    var lastTwoDigits = pesel.substring(0, 2);
    if (Number(pesel.substring(2, 4)) <= 12) {
        firstTwoDigits = "19";
    } else {
        firstTwoDigits = "20";
    }
    return (firstTwoDigits + lastTwoDigits);
}

//0 -> man 1 -> woman
function getPlecFromPesel(pesel) {
    return (Number(pesel[9]) + 1) % 2;
}
