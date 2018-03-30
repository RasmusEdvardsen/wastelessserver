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
    $('#searchbutton').on('click', function () {

        //pre ajax
        document.body.style.cursor = 'wait';
        $(this).prop('disabled', true);

        //data for get
        var $query = $('#querytosearch').val();
        var $options = $('#queryoptions').val();

        $.ajax({
            url: "/admin/get/?query=" + $query + "&options=" + $options + "/"
        }).done(function (data) {
            console.log(data)
            //document.body.style.cursor = 'wait';
        });


    });
});