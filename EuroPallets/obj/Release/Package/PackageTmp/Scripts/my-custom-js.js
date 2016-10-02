$(document).scroll(function (e) {
    var scrollTop = $(document).scrollTop();
    if (scrollTop > 0) {
        console.log(scrollTop);
        $('.header-v5 header-static').addClass('header-fixed-shrink');
    } else {
        $('.header-v5 header-static').addClass('header-fixed-shrink');
    }
});