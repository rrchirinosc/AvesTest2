﻿//$(document).ready(function () {
//    $('#birdShow').on('slid.bs.carousel', function () {
//        alert("The sliding transition of previous carousel item has been fully completed.");
//    });
//});

window.addEventListener('DOMContentLoaded', (event) => {

    // Set event for info display
    document.getElementById("show-info").addEventListener("click", infoClicked);

    // Fill bird info under each carousel item
    $('#bird-show').on('slide.bs.carousel', function (event) {
        let carouselIndex = parseInt(this.children[0].children[event.to].attributes[0].ownerElement.children[0].dataset.index);
        setBirdInfo(carouselIndex);
    });

    // Hack to overcome the fact that the carousel does not fire an event upon starting 
    // to be able to fill up info for first slide */
    function infoClicked() {
        let carouselIndex = $('.carousel-item.active').index();
        if (carouselIndex == 0) {
            setBirdInfo(carouselIndex);
        }         
    };

    // Set bird info display with the current slide values
    function setBirdInfo(index) {
        $('#bird-location').html(birddata.Birds[index].Location + " - " + Countries[Countries.findIndex(x => parseInt(x.code) == birddata.Birds[index].Country)].name);

        // put date into Month Day, Year format
        let date = birddata.Birds[index].Date.split("/");
        let months = Months.split(",");
        let month = parseInt(date[1]) - 1;
        $('#bird-date-taken').html(months[month] + " " + date[0] + ", " + date[2]);

        // add comment
        $('#image-comment').html(birddata.Birds[index].Comment);

        // don't load map if not requested
        if ($('#bird-location').attr('aria-expanded') === "false")
            return;

        loadMap(index);
    };   

    function loadMap(index) {
        // location map
        //let url = 'https://www.google.com/maps/embed/v1/view?key=' + appkey + '&center=' + birddata.Birds[index].Coordinate + '&zoom=15&maptype=satellite';
        let url = 'https://www.google.com/maps?q=' + birddata.Birds[index].Coordinate + '&z=18&output=embed';
        //<iframe src="https://maps.google.com/maps?q=35.856737, 10.606619&z=15&output=embed" width="360" height="270" frameborder="0" style="border:0"></iframe>

        var map = `<iframe width="100%"` +
            `height="300"` +
            `frameborder="0" style="border:1px solid #737373"` +
            `src=${url}></iframe>`;
        $('#bird-map').empty();
        $('#bird-map').append(`${map}`);
    }

    $('#bird-location').click(function (e) {
        if (e.target.attributes.getNamedItem('aria-expanded').value === "false") {
            let carouselIndex = $('.carousel-item.active').index();
            loadMap(carouselIndex);
        }
            
    });
});

var Countries = [
    { "code": "004", "name": "Afghanistan" },
    { "code": "248", "name": "Åland Islands" },
    { "code": "008", "name": "Albania" },
    { "code": "012", "name": "Algeria" },
    { "code": "016", "name": "American Samoa" },
    { "code": "020", "name": "Andorra" },
    { "code": "024", "name": "Angola" },
    { "code": "028", "name": "Antigua and Barbuda" },
    { "code": "032", "name": "Argentina" },
    { "code": "051", "name": "Armenia" },
    { "code": "533", "name": "Aruba" },
    { "code": "036", "name": "Australia" },
    { "code": "040", "name": "Austria" },
    { "code": "031", "name": "Azerbaijan" },
    { "code": "044", "name": "Bahamas" },
    { "code": "048", "name": "Bahrain" },
    { "code": "050", "name": "Bangladesh" },
    { "code": "052", "name": "Barbados" },
    { "code": "112", "name": "Belarus" },
    { "code": "056", "name": "Belgium" },
    { "code": "084", "name": "Belize" },
    { "code": "204", "name": "Benin" },
    { "code": "060", "name": "Bermuda" },
    { "code": "064", "name": "Bhutan" },
    { "code": "068", "name": "Bolivia" },
    { "code": "070", "name": "Bosnia and Herzegovina" },
    { "code": "072", "name": "Botswana" },
    { "code": "074", "name": "Bouvet Island" },
    { "code": "076", "name": "Brazil" },
    { "code": "086", "name": "British Indian Ocean Territory" },
    { "code": "096", "name": "Brunei Darussalam" },
    { "code": "100", "name": "Bulgaria" },
    { "code": "854", "name": "Burkina Faso" },
    { "code": "108", "name": "Burundi" },
    { "code": "116", "name": "Cambodia" },
    { "code": "120", "name": "Cameroon" },
    { "code": "124", "name": "Canada" },
    { "code": "132", "name": "Cape Verde" },
    { "code": "136", "name": "Cayman Islands" },
    { "code": "140", "name": "Central African Republic" },
    { "code": "148", "name": "Chad" },
    { "code": "152", "name": "Chile" },
    { "code": "156", "name": "China" },
    { "code": "162", "name": "Christmas Island" },
    { "code": "166", "name": "Cocos (Keeling) Islands" },
    { "code": "170", "name": "Colombia" },
    { "code": "174", "name": "Comoros" },
    { "code": "178", "name": "Congo" },
    { "code": "180", "name": "Congo, the Democratic Republic of the" },
    { "code": "184", "name": "Cook Islands" },
    { "code": "188", "name": "Costa Rica" },
    { "code": "384", "name": "Cote D'Ivoire" },
    { "code": "191", "name": "Croatia" },
    { "code": "192", "name": "Cuba" },
    { "code": "531", "name": "Curaçao" },
    { "code": "196", "name": "Cyprus" },
    { "code": "203", "name": "Czech Republic" },
    { "code": "208", "name": "Denmark" },
    { "code": "262", "name": "Djibouti" },
    { "code": "212", "name": "Dominica" },
    { "code": "214", "name": "Dominican Republic" },
    { "code": "218", "name": "Ecuador" },
    { "code": "818", "name": "Egypt" },
    { "code": "222", "name": "El Salvador" },
    { "code": "226", "name": "Equatorial Guinea" },
    { "code": "232", "name": "Eritrea" },
    { "code": "233", "name": "Estonia" },
    { "code": "231", "name": "Ethiopia" },
    { "code": "238", "name": "Falkland Islands (Malvinas)" },
    { "code": "234", "name": "Faroe Islands" },
    { "code": "242", "name": "Fiji" },
    { "code": "246", "name": "Finland" },
    { "code": "250", "name": "France" },
    { "code": "254", "name": "French Guiana" },
    { "code": "258", "name": "French Polynesia" },
    { "code": "260", "name": "French Southern Territories" },
    { "code": "266", "name": "Gabon" },
    { "code": "270", "name": "Gambia" },
    { "code": "268", "name": "Georgia" },
    { "code": "276", "name": "Germany" },
    { "code": "288", "name": "Ghana" },
    { "code": "292", "name": "Gibraltar" },
    { "code": "300", "name": "Greece" },
    { "code": "304", "name": "Greenland" },
    { "code": "308", "name": "Grenada" },
    { "code": "312", "name": "Guadeloupe" },
    { "code": "316", "name": "Guam" },
    { "code": "320", "name": "Guatemala" },
    { "code": "324", "name": "Guinea" },
    { "code": "624", "name": "Guinea-Bissau" },
    { "code": "328", "name": "Guyana" },
    { "code": "332", "name": "Haiti" },
    { "code": "334", "name": "Heard Island and Mcdonald Islands" },
    { "code": "336", "name": "Holy See (Vatican City State)" },
    { "code": "340", "name": "Honduras" },
    { "code": "344", "name": "Hong Kong" },
    { "code": "348", "name": "Hungary" },
    { "code": "352", "name": "Iceland" },
    { "code": "356", "name": "India" },
    { "code": "360", "name": "Indonesia" },
    { "code": "364", "name": "Iran, Islamic Republic of" },
    { "code": "368", "name": "Iraq" },
    { "code": "372", "name": "Ireland" },
    { "code": "376", "name": "Israel" },
    { "code": "380", "name": "Italy" },
    { "code": "388", "name": "Jamaica" },
    { "code": "392", "name": "Japan" },
    { "code": "400", "name": "Jordan" },
    { "code": "398", "name": "Kazakhstan" },
    { "code": "404", "name": "Kenya" },
    { "code": "296", "name": "Kiribati" },
    { "code": "408", "name": "Korea, Democratic People's Republic of" },
    { "code": "410", "name": "Korea, Republic of" },
    { "code": "414", "name": "Kuwait" },
    { "code": "417", "name": "Kyrgyzstan" },
    { "code": "418", "name": "Lao People's Democratic Republic" },
    { "code": "428", "name": "Latvia" },
    { "code": "422", "name": "Lebanon" },
    { "code": "426", "name": "Lesotho" },
    { "code": "430", "name": "Liberia" },
    { "code": "434", "name": "Libya" },
    { "code": "438", "name": "Liechtenstein" },
    { "code": "440", "name": "Lithuania" },
    { "code": "442", "name": "Luxembourg" },
    { "code": "446", "name": "Macao" },
    { "code": "807", "name": "Macedonia, the Former Yugoslav Republic of" },
    { "code": "450", "name": "Madagascar" },
    { "code": "454", "name": "Malawi" },
    { "code": "458", "name": "Malaysia" },
    { "code": "462", "name": "Maldives" },
    { "code": "466", "name": "Mali" },
    { "code": "470", "name": "Malta" },
    { "code": "584", "name": "Marshall Islands" },
    { "code": "474", "name": "Martinique" },
    { "code": "478", "name": "Mauritania" },
    { "code": "480", "name": "Mauritius" },
    { "code": "175", "name": "Mayotte" },
    { "code": "484", "name": "Mexico" },
    { "code": "583", "name": "Micronesia, Federated States of" },
    { "code": "498", "name": "Moldova, Republic of" },
    { "code": "492", "name": "Monaco" },
    { "code": "496", "name": "Mongolia" },
    { "code": "499", "name": "Montenegro" },
    { "code": "500", "name": "Montserrat" },
    { "code": "504", "name": "Morocco" },
    { "code": "508", "name": "Mozambique" },
    { "code": "104", "name": "Myanmar" },
    { "code": "516", "name": "Namibia" },
    { "code": "520", "name": "Nauru" },
    { "code": "524", "name": "Nepal" },
    { "code": "528", "name": "Netherlands" },
    { "code": "540", "name": "New Caledonia" },
    { "code": "554", "name": "New Zealand" },
    { "code": "558", "name": "Nicaragua" },
    { "code": "562", "name": "Niger" },
    { "code": "566", "name": "Nigeria" },
    { "code": "570", "name": "Niue" },
    { "code": "574", "name": "Norfolk Island" },
    { "code": "580", "name": "Northern Mariana Islands" },
    { "code": "578", "name": "Norway" },
    { "code": "512", "name": "Oman" },
    { "code": "586", "name": "Pakistan" },
    { "code": "585", "name": "Palau" },
    { "code": "275", "name": "Palestine, State of" },
    { "code": "591", "name": "Panama" },
    { "code": "598", "name": "Papua New Guinea" },
    { "code": "600", "name": "Paraguay" },
    { "code": "604", "name": "Peru" },
    { "code": "608", "name": "Philippines" },
    { "code": "612", "name": "Pitcairn" },
    { "code": "616", "name": "Poland" },
    { "code": "620", "name": "Portugal" },
    { "code": "630", "name": "Puerto Rico" },
    { "code": "634", "name": "Qatar" },
    { "code": "638", "name": "Reunion" },
    { "code": "642", "name": "Romania" },
    { "code": "643", "name": "Russian Federation" },
    { "code": "646", "name": "Rwanda" },
    { "code": "654", "name": "Saint Helena" },
    { "code": "659", "name": "Saint Kitts and Nevis" },
    { "code": "662", "name": "Saint Lucia" },
    { "code": "666", "name": "Saint Pierre and Miquelon" },
    { "code": "670", "name": "Saint Vincent and the Grenadines" },
    { "code": "882", "name": "Samoa" },
    { "code": "674", "name": "San Marino" },
    { "code": "678", "name": "Sao Tome and Principe" },
    { "code": "682", "name": "Saudi Arabia" },
    { "code": "686", "name": "Senegal" },
    { "code": "688", "name": "Serbia" },
    { "code": "690", "name": "Seychelles" },
    { "code": "694", "name": "Sierra Leone" },
    { "code": "702", "name": "Singapore" },
    { "code": "703", "name": "Slovakia" },
    { "code": "705", "name": "Slovenia" },
    { "code": "090", "name": "Solomon Islands" },
    { "code": "706", "name": "Somalia" },
    { "code": "710", "name": "South Africa" },
    { "code": "239", "name": "South Georgia and the South Sandwich Islands" },
    { "code": "724", "name": "Spain" },
    { "code": "144", "name": "Sri Lanka" },
    { "code": "729", "name": "Sudan" },
    { "code": "740", "name": "Suriname" },
    { "code": "744", "name": "Svalbard and Jan Mayen" },
    { "code": "752", "name": "Sweden" },
    { "code": "756", "name": "Switzerland" },
    { "code": "760", "name": "Syrian Arab Republic" },
    { "code": "158", "name": "Taiwan, Province of China" },
    { "code": "762", "name": "Tajikistan" },
    { "code": "834", "name": "Tanzania, United Republic of" },
    { "code": "764", "name": "Thailand" },
    { "code": "626", "name": "Timor-Leste" },
    { "code": "768", "name": "Togo" },
    { "code": "772", "name": "Tokelau" },
    { "code": "776", "name": "Tonga" },
    { "code": "780", "name": "Trinidad and Tobago" },
    { "code": "788", "name": "Tunisia" },
    { "code": "792", "name": "Turkey" },
    { "code": "795", "name": "Turkmenistan" },
    { "code": "796", "name": "Turks and Caicos Islands" },
    { "code": "798", "name": "Tuvalu" },
    { "code": "800", "name": "Uganda" },
    { "code": "804", "name": "Ukraine" },
    { "code": "784", "name": "United Arab Emirates" },
    { "code": "826", "name": "United Kingdom" },
    { "code": "840", "name": "United States" },
    { "code": "581", "name": "United States Minor Outlying Islands" },
    { "code": "858", "name": "Uruguay" },
    { "code": "860", "name": "Uzbekistan" },
    { "code": "548", "name": "Vanuatu" },
    { "code": "862", "name": "Venezuela" },
    { "code": "704", "name": "Viet Nam" },
    { "code": "092", "name": "Virgin Islands, British" },
    { "code": "850", "name": "Virgin Islands US" },
    { "code": "876", "name": "Wallis and Futuna" },
    { "code": "732", "name": "Western Sahara" },
    { "code": "887", "name": "Yemen" },
    { "code": "894", "name": "Zambia" },
    { "code": "716", "name": "Zimbabwe" }
];

var Months = "January,February,March,April,May,June,July,August,September,October,Novermber,December";


