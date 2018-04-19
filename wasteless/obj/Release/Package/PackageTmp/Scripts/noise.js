function initNoise() {
    $('#tonoiseform').on('click', function () {
        $('#actionsbar').fadeOut(function () {
            $('#createnoiseform').fadeIn();
        });
    });
    $('#returntoactionsbar').on('click', function () {
        $('#createnoiseform').fadeOut(function () {
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
            url: "/api/noises/?query=" + $query + "&options=" + $options,
            type: 'GET',
            success: function (data) {
                $this.removeProp('disabled');
                document.body.style.cursor = 'default';
                if (data.length > 0) {
                    $('#noisetablebody').empty();
                    var counter = 1;
                    data.forEach(function (foodtype) {
                        var row = $('<tr>');
                        row.append($('<td>').html(counter));
                        row.append($('<td>').html(foodtype.NoiseID));
                        row.append($('<td>').html(foodtype.NoiseWord));
                        row.append($('<td align=center>').append($('<button type="button" class="btn btn-danger deletenoise">').html('Delete')));
                        $('#noisetablebody').append(row);
                        counter++;
                    });
                    $('#noisecountrendered').html(data.length);
                }
            }
        });
    });

    $('.deletenoise').on('click', function () {
        var result = confirm("Want to delete?");
        if (result) {
            $this = $(this);
            $parent = $this.parent();
            $noiseid = $parent.siblings('td.noiseid');
            $row = $this.closest('tr');
            $.ajax({
                url: "/api/noises/?id=" + $noiseid[0].innerHTML,
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

    $('#noiseword').keyup(function () {
        if (event.keyCode === 13) {
            $('#createnoise').click();
        }
    });
    $('#createnoise').on('click', function () {
        if (!$('#noiseword').val()) return;
        //pre ajax
        document.body.style.cursor = 'wait';
        var $this = $(this);
        $this.prop('disabled', true);

        //date for create
        var $createnoisename = $('#noiseword').val();

        $.ajax({
            url: "/api/noises/",
            data: { createnoisename: $('#noiseword').val() },
            type: "POST",
            success: function () {
                $this.removeProp('disabled');
                document.body.style.cursor = 'default';

                var row = $('<tr>');
                row.append($('<td>').html('NEW'));
                row.append($('<td>').html('NEW'));
                row.append($('<td>').html($createnoisename));
                row.append($('<td align=center>'));
                row.hide().prependTo($('#noisetablebody')).fadeIn('slow');
            }
        });
    });
}