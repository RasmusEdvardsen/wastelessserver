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

    $('#querytosearch').keyup(function () {
        if (event.keyCode === 13) {
            $('#searchbutton').click();
        }
    });

    $('#searchbutton').on('click', function () {
        //pre ajax
        document.body.style.cursor = 'wait';
        var $this = $(this);
        $this.prop('disabled', true);

        //data for get
        var $query = $('#querytosearch').val();
        var $options = $('#queryoptions').val();

        $.ajax({
            url: "/api/foodtypes/get/?query=" + $query + "&options=" + $options,
            success: function (data) {
                console.log(data);
                $this.removeProp('disabled');
                document.body.style.cursor = 'default';
                if (data.length > 0) {
                    $('#foodtypetablebody').empty();
                    var counter = 1;
                    data.forEach(function (foodtype) {
                        var row = $('<tr>');
                        row.append($('<td>').html(counter));
                        row.append($('<td>').html(foodtype.FoodTypeID));
                        row.append($('<td>').html(foodtype.FoodType));
                        row.append($('<td>').html(foodtype.Code));
                        row.append($('<td>').html(foodtype.Created));
                        row.append($('<td>').html(foodtype.GUID));
                        row.append($('<td align=center>').append($('<button type="button" class="btn btn-danger">').html('Delete')));
                        $('#foodtypetablebody').append(row);
                        counter++;
                    });
                    $('#foodtypecountrendered').html(data.length)
                }
            }
        }); 
    });
});