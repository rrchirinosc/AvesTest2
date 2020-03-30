//$(document).ready(function () {
//    $('#birdShow').on('slid.bs.carousel', function () {
//        alert("The sliding transition of previous carousel item has been fully completed.");
//    });
//});

window.addEventListener('DOMContentLoaded', (event) => {

    // display bird name (sciname) stored in the alt property
    $('#birdShow').on('slid.bs.carousel', function (event) {
        $('#bird-name').html(this.children[0].children[event.to].attributes[0].ownerElement.children[0].dataset.name);
        $('#bird-location').html(this.children[0].children[event.to].attributes[0].ownerElement.children[0].dataset.location);

        // put date into Month Day, Year format
        let date = this.children[0].children[event.to].attributes[0].ownerElement.children[0].dataset.date.split("/");
        let months = "January,February,March,April,May,June,July,August,September,October,Novermber,December".split(",");
        let month = parseInt(date[1]) - 1;
        $('#bird-date-taken').html(months[month] + " " + date[0] + ", " + date[2]);
    });
});