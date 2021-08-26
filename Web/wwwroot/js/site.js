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

const setSelect = (url, elementId, defaultValue, placeHolder = "--Seçiniz--") => {
    $.ajax({
        url: url,
        success: function (res) {
            var s = '<option value="">' + placeHolder + '</option>';
            for (var i = 0; i < res.length; i++) {
                s += '<option value="' + res[i].value + '">' + res[i].text + '</option>';
            }
            $("#" + elementId).html(s);
            if (defaultValue) {
                $("#" + elementId).val(defaultValue).change();
            }
        }
    })
}