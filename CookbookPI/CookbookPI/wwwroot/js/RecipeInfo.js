var form, a4 = [595.28, 841.89]; // for a4 size paper width and height
$(document).ready(function () {
    $("#saveToPDF").on("click", function (e) {
        var doc = new jsPDF();
        doc.fromHTML($('#Recipe-Form').get(0), 20, 20, {
            'width': 500
        });
        doc.save('Example.pdf');
    });
});

function createPDF(selector) {
    form = $(selector);
    getCanvas().then(function (canvas) {
        var cache_width = form.width()

        var img = canvas.toDataURL("image/png");
        var doc = new jsPDF({
                unit: 'px',
                format: 'a4'
            });
        doc.addImage(img, 'JPEG', 20, 20);
        doc.save('example.pdf');
        form.width(cache_width);
    });
}

// create canvas object
function getCanvas() {
    form.width((a4[0] * 1.33333) - 80).css('max-width', 'none');
    return html2canvas(form, {
        imageTimeout: 2000,
        removeContainer: true
    });
}