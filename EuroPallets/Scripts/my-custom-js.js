$(document).scroll(function (e) {
    var scrollTop = $(document).scrollTop();
    if (scrollTop > 0) {
        console.log(scrollTop);
        $('.header-v5 header-static').addClass('header-fixed-shrink');
    } else {
        $('.header-v5 header-static').addClass('header-fixed-shrink');
    }
});

function notification(type, message) {
    if (type == 'Success') {
        toastr.success(message, 'Success', { positionClass: "toast-bottom-right", });
    } else if (type == 'Error') {
        toastr.error(message, 'Error', 'Error', { positionClass: "toast-bottom-right", });
    } else if (type == 'Email') {
        toastr.warning(message, 'Email', 'Success', { positionClass: "toast-bottom-right", });
    }
}