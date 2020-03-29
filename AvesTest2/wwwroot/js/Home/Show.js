//$(document).ready(function () {
//    $('#birdShow').on('slid.bs.carousel', function () {
//        alert("The sliding transition of previous carousel item has been fully completed.");
//    });
//});

window.addEventListener('DOMContentLoaded', (event) => {

    // display bird name (sciname) stored in the alt property
    $('#birdShow').on('slide.bs.carousel', function (event) {
        $('#bird-name').html(this.children[0].ownerDocument.images[event.to].alt);
    });
});