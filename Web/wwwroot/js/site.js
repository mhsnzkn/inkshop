$(document).ready(function () {
    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
    });

    $('select').select2();
});

const ajaxSuccess = (xhr, returnUrl) => {
    if (xhr.responseJSON.error === false)
        window.location.href = returnUrl;
    else {
        alertify.error(xhr.responseJSON.userMessage);
        console.log(xhr.responseJSON.message);
    }
}
const ajaxFail = (err) => {
    alertify.error("Bağlantı Hatası");
    console.log(err);
}

const toUpper = (a) => {
    a.value = a.value.toLocaleUpperCase('tr-TR');
}