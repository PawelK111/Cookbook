$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: '/Admin/GetUsers',
        dataType: 'html',
        success: function (data) {
            $("#panelresult").html(data)
        }
    });
    $('#RecipesAdminPanel').click(function () {
        $.ajax({
            type: "GET",
            url: '/Admin/GetRecipesNotAcc',
            dataType: 'html',
            success: function (data) {
                $("#panelresult").html(data)
            }
        });
    });
});