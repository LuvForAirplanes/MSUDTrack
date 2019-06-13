﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$("select, input").on("input", function (e) {
    //here goes the api post
    var elements = $("#" + e.target.id);

    var record = {
        FoodId: elements[0].value,
        Food: {
            Id: elements[0].value,
            ProteinGrams: elements[1].value
        }
    };

    $.ajax({
        type: 'POST',
        url: '/api/Records',
        data: JSON.stringify(record),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json'
    });
});
