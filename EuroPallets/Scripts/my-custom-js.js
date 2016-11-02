$(document).scroll(function (e) {
    var scrollTop = $(document).scrollTop();
    if (scrollTop > 40) {
        $("#top-nav-bar").addClass('header-fixed-shrink');
    } else {
        $("#top-nav-bar").removeClass('header-fixed-shrink');
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

function WebApiResolveUrl(url) {
    var host = window.location.origin;

    return host + url;
}

function ResolveUrl(url) {
    var baseUrl = window.location.origin;
    var pathName = window.location.pathname.substr(0, window.location.pathname.lastIndexOf('/'))

    url = baseUrl + pathName + url;

    return url;
}