//TODO: CONSIDER MAKING CRUD-MODULE EVENTS STANDARDIZED, SO NOISE, FOODTYPE, EAN, etc. CAN USE SAME JS, LESS CLUTTER

$(function () {
    if ($('body').hasClass('FoodType')) {
        initFoodType();
    } else if ($('body').hasClass('Noise')) {
        initNoise();
    }
});