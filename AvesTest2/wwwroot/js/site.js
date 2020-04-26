// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


window.addEventListener('DOMContentLoaded', (event) => {

    // always start with button to come to the page top hidden
    $('#to-top').hide();

    // code to check whether or not the 'go to top' button is needed
    document.addEventListener("scroll", checkVisibility);

    function checkVisibility() {
        var rect = document.getElementById("page-top").getBoundingClientRect();
        //console.log(rect.top);
        rect.top >= 0 ? $('#to-top').hide() : $('#to-top').show();
    }

})