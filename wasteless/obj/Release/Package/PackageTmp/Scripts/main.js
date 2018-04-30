//TODO: CONSIDER MAKING CRUD-MODULE EVENTS STANDARDIZED, SO NOISE, FOODTYPE, EAN, etc. CAN USE SAME JS, LESS CLUTTER

$(function () {
    if ($('body').hasClass('FoodType')) {
        initFoodType();
    } else if ($('body').hasClass('Noise')) {
        initNoise();
    }
    $('#resetnoisewordscache').click(function () {
        var result = confirm('Reset Cache? This will impact the stability and loadtime of the server.');

        //pre ajax
        document.body.style.cursor = 'wait';
        var $this = $(this);
        $this.prop('disabled', true);

        $.ajax({
            url: "/api/cache/?action=reset",
            type: 'GET',
            success: function (data) {
                $this.removeProp('disabled');
                document.body.style.cursor = 'default';
                alert('Cache reset. Actions that use the cache will automatically regenerate the cache.');
            }
        });
    });
});