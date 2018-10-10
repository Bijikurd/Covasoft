// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {

    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
    });

});


$('.vertical .progress-fill span').each(function () {
    var percent = $(this).html();
    var pTop = 100 - (percent.slice(0, percent.length - 1)) + "%";
    $(this).parent().css({
        'height': percent,
        'top': pTop
    });
});