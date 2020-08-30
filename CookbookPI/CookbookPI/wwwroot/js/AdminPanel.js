var urlPath = ["/Admin/GetUsers", "/Admin/GetRecipesNotAccept", "/Admin/GetRecipesAccept"];

$(document).ready(function () {
    loadStyle();
    getData(urlPath[0]);

    $('#AllUsers').click(function () {
        getData(urlPath[0]);
    });
    $('#RecipesNotAccept').click(function () {
        getData(urlPath[1]);
    });
    $('#RecipesAccept').click(function () {
        getData(urlPath[2]);
    });
});

function loadStyle() {
    $(".wrapper").css("background-image", "url('../img/adminbg.jpg')");
    $("#navmenu").removeClass("bg-success");
    $("#navmenu").addClass("bg-primary");
    $(".card").css("background-color", "#bcfdff");
}

function getData(url) {
    $.ajax({
        type: "GET",
        url: url,
        dataType: 'html',
        success: function (data) {
            $("#panelresult").html(data)
        }
    });
}