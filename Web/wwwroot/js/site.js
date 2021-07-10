$(document).ready(function () {
    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
    });
});

function ajaxSuccess(xhr, returnUrl) {
    if (xhr.responseJSON.error === false)
        window.location.href = returnUrl;
    else {
        alertify.error("Kayit Basarisiz");
        console.log(xhr.responseJSON.message);
    }
}
function ajaxFail(err) {
    alertify.error("Baglanti Hatasi");
    console.log(err);
}