function initFoodType() {
    //TODO: REWRITE ALL OF THIS!



    $('#tofoodtypeform').on('click', function () {
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
        //if (!$('#querytosearch').val()) return;

        //pre ajax
        document.body.style.cursor = 'wait';
        var $this = $(this);
        $this.prop('disabled', true);

        //data for get
        var $query = $('#querytosearch').val();
        var $options = $('#queryoptions').val();

        $.ajax({
            url: "/api/foodtypes/?query=" + $query + "&options=" + $options,
            type: 'GET',
            success: function (data) {
                $this.removeProp('disabled');
                document.body.style.cursor = 'default';
                if (data.length > 0) {
                    $('#foodtypetablebody').empty();
                    var counter = 1;
                    data.forEach(function (foodtype) {
                        var row = $('<tr>');
                        row.append($('<td>').html(counter));
                        row.append($('<td>').html(foodtype.FoodTypeID));
                        row.append($('<td>').html(foodtype.FoodTypeName));
                        row.append($('<td>').html(foodtype.Code));
                        row.append($('<td>').html(foodtype.Created));
                        row.append($('<td>').html(foodtype.GUID));
                        row.append($('<td align=center>').append($('<button type="button" class="btn btn-danger deletefoodtype">').html('Delete')));
                        $('#foodtypetablebody').append(row);
                        counter++;
                    });
                    $('#foodtypecountrendered').html(data.length);
                }
            }
        });
    });

    $('.deletefoodtype').on('click', function () {
        var result = confirm("Want to delete?");
        if (result) {
            $this = $(this);
            $parent = $this.parent();
            $foodtypeid = $parent.siblings('td.foodtypeid');
            $row = $this.closest('tr');
            //$guid = $parent.siblings('td.guid');
            $.ajax({
                url: "/api/foodtypes/?id=" + $foodtypeid[0].innerHTML,
                type: 'DELETE',
                success: function () {
                    console.log($row);
                    $row.fadeOut(function () {
                        $row.remove();
                    });
                }
            });
        }
    });

    $('#createfoodtypename').keyup(function () {
        if (event.keyCode === 13) {
            $('#createfoodtype').click();
        }
    });

    $('#createfoodtypecode').keyup(function () {
        if (event.keyCode === 13) {
            $('#createfoodtype').click();
        }
    });

    $('#createfoodtype').on('click', function () {
        if (!$('#createfoodtypename').val()) return;
        //pre ajax
        document.body.style.cursor = 'wait';
        var $this = $(this);
        $this.prop('disabled', true);

        //date for create
        var $createfoodtypename = $('#createfoodtypename').val();
        var $createfoodtypecode = $('#createfoodtypecode').val();

        $.ajax({
            url: "/api/foodtypes/",
            data: { createfoodtypename: $('#createfoodtypename').val(), createfoodtypecode: $('#createfoodtypecode').val() },
            type: "POST",
            success: function () {
                $this.removeProp('disabled');
                document.body.style.cursor = 'default';

                var row = $('<tr>');
                row.append($('<td>').html('NEW'));
                row.append($('<td>').html('NEW'));
                row.append($('<td>').html($createfoodtypename));
                row.append($('<td>').html($createfoodtypecode));
                row.append($('<td>').html('Just Now'));
                row.append($('<td>').html('NEW'));
                row.append($('<td align=center>'));
                row.hide().prependTo($('#foodtypetablebody')).fadeIn('slow');
            }
        });
    });
}