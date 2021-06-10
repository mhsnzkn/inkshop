$(document).ready(function () {
    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
    });
});

function ajaxCompleted (res) {
    if (res.responseJSON.error === false) {
        $('#datatable').DataTable().ajax.reload();
        $('.modal').modal("hide");
        toastr.success("Kayıt Başarılı");
    } else {
        toastr.error("Beklenmedik bir hata oldu!");
        console.log(res.responseJSON);
    }
}

function ajaxFailed (xhr) {
    toastr.error("Baglanti Hatasi");
    console.log(xhr);
}