// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

async function loader(show, forceView = false) {
    if (show) {
        var zIndex = 1000;
        var imageURL = `/Images/Site/puff.svg`;
        var html = `<div class="loadingCircles" z-index=${zIndex}><img src="${imageURL}" alt="loading..."</div>`;
        var timeout = 1000 * 180; //Max 3 minutes
        $('body').append(html);
        setTimeout(function () {
            $('.loadingCircles').remove();
        }, timeout);
    }
    else {
        // forceView, forces loader to show for an instance -- used as feedback
        if (forceView) {
            timeout = 250; //hack so it shows at least 1/4 sec
            setTimeout(function () {
                $('.loadingCircles').remove();
            }, timeout);
        }
        else
            $('.loadingCircles').remove();
    }
}


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

    $(".category-head").click(function () {
        loader(true);
    });

    $(".category-item").click(function () {
        loader(true);
    });

    $(".blurb-selected").click(function () {
        loader(true);
    });

})