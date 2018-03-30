$(function () {
    $('#createfoodtype').on('click', function () {
        $('#actionsbar').fadeOut(function () {
            $('#createfoodtypeform').fadeIn();
        });
    });
    $('#returntoactionsbar').on('click', function () {
        $('#createfoodtypeform').fadeOut(function () {
            $('#actionsbar').fadeIn();
        });
    });
});