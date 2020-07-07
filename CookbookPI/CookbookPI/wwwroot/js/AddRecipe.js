$(document).ready(function () {
    var _page = 0;
    $("#NextButton").click(function () {
        _page++;
        changePage(_page);
    });

    $("#PrevButton").click(function () {
        _page--;
        changePage(_page);
    });

    $("#AddComponent").click(function () {
        if ($("#NameOfComp").val() != "" || $("#AmountOfComp").val() != "")
        {
            if ($.isNumeric($("#AmountOfComp").val())) {
                $("#tblComponents").append('<tr><td>' +
                    $("#NameOfComp").val() + '</td><td>' +
                    $("#AmountOfComp").val() + '</td><td>' +
                    $("#unitOfMeasureComp").val() + '</td><td><button class="btn btn-danger" id="deleteComponent">-</button></td></tr>');
            }
    }
    }
    );

    $("#tblComponents").on('click', '#deleteComponent', function () {
        $(this).closest('tr').remove();
    });
});

function changePage(_x) {
    switch (_x) {
        case 0:
            $('#secondPage').addClass("collapse");
            $('#firstPage').removeClass("collapse");
            $("#PrevButton").addClass("collapse");
            break;
        case 1:
            $("#PrevButton").removeClass("collapse");
            $("#NextButton").removeClass("collapse");
            $('#firstPage').addClass("collapse");
            $('#thirdPage').addClass("collapse");
            $('#EndCreateRecipe').addClass("collapse");
            $('#ErrorEndCreate').addClass("collapse");
            $('#secondPage').removeClass("collapse");
            break;
        case 2:
            $("#NextButton").addClass("collapse");
            $('#secondPage').addClass("collapse");
            $('#thirdPage').removeClass("collapse");
            $('#EndCreateRecipe').removeClass("collapse");
            $('#ErrorEndCreate').removeClass("collapse");
            break;
    }
}


function sendComponents() {
    var _components = new Array();
    $("#tblComponents TBODY TR").each(function () {
        var row = $(this);
        var _x = {};
        _x.NameOfComponent = row.find("TD").eq(0).html();
        _x.Unit = row.find("TD").eq(2).html();
        _x.Amount = row.find("TD").eq(1).html();
        _components.push(_x);
    });
    $.ajax({
        method: "POST",
        url: "/Recipes/InsertComponents",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(_components),
        dataType: "json",
        success: function (r) {
            $('#AddRecipeForm').hide();
            $('#successAlert').show();
        }
    });
}


sendRecipe = form => {
    try {
        $.ajax({
            type: 'POST',
            url: '/Recipes/AddRecipe/',
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function () {
                sendComponents();
            },
                error: function (err) {

                }
            })
    }
    catch (e) {
        console.log(e);
    }
    return false;
}