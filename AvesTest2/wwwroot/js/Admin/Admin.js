window.addEventListener('DOMContentLoaded', (event) => {

   // keep track of a tab being loaded (=1) or not
    var reloadTabs = {
        BIRDS: 0,
        FAMILIES: 0,
        IMAGES: 0,
        STATS: 0
    };

    var loadedStats = null;
    var sortAscending = 1;

    var init = function () {
        $.validator.unobtrusive.parse("#addBirdForm");
        $.validator.unobtrusive.parse("#addImageForm");
        $.validator.unobtrusive.parse("#updateImageForm");
        $.validator.unobtrusive.parse("#removeImageForm");

        // refresh table buttons click events
        document.getElementById("reload-birds").addEventListener("click", loadBirds);
        document.getElementById("reload-families").addEventListener("click", loadFamilies);
        document.getElementById("reload-image-data").addEventListener("click", loadImages);
        document.getElementById("reload-stats").addEventListener("click", loadStats);
        document.getElementById("sort-stats").addEventListener("click", sortStats);
    };

    $('.nav-link').click(function (e) {
        switch ($(e.target).text()) {
            case 'Bird':
                if (reloadTabs.BIRDS == 0)
                    loadBirds();
                break;
            case 'Family':
                if (reloadTabs.FAMILIES == 0)
                    loadFamilies();
                break;
            case 'Image':
                if (reloadTabs.IMAGES == 0)
                    loadImages();
                break;
            case 'Stats':
                if (reloadTabs.STATS == 0)
                    loadStats();
                break;
            default:
                break;
        }
        //alert($(e.target).text());
    })

    // functions to load tables
    function loadBirds() {
        var url = "/Admin/GetBirdTable";
        loader(true);
        $.ajax(
            {
                type: 'GET',
                url: url,
                dataType: 'json',
                cache: false,
                contentType: "application/json; charset=utf-8"
            }).fail(function (jqXHR, textStatus, errorThrown) {
                alert(errorThrown + this.url);
            }).done(function (birdList, textStatus, jqXHR) {
                // build and display html table with birdList
                let th = `<th style="color:#0366D6">`
                var table = `<table><thead><tr>${th}Id</th>${th}Name</th>${th}SciName</th>${th}FamilyId</th></tr></thead><tbody>`;
                var index = 2;
                for (bird in birdList) {
                    let id = birdList[bird].id;
                    let name = birdList[bird].name;
                    let sciname = birdList[bird].sciName;
                    let familyid = birdList[bird].familyId;
                    let td = '<td style="padding-left: 10px; color:#fff">';
                    let tr = (index++ % 2 === 0) ? '<tr style="background-color:#444">' : '<tr>';
                    table = table.concat(`${tr}${td}${id}</td>${td}${name}</td>${td}${sciname}</td>${td}${familyid}</td></tr>`);
                }
                table = table.concat(`</tbody></table>`);
                $('#bird-table').empty();
                $('#bird-table').append(`${table}`);
                $('#reload-birds').css('visibility', 'visible');
                reloadTabs.BIRDS = 1;
            });
        loader(false, true);
    };

    function loadFamilies() {
        var url = "/Admin/GetFamilyTable";
        loader(true);

        $.ajax(
            {
                type: 'GET',
                url: url,
                dataType: 'json',
                cache: false,
                contentType: "application/json; charset=utf-8"
            }).fail(function (jqXHR, textStatus, errorThrown) {
                alert(errorThrown + this.url);
            }).done(function (familyList, textStatus, jqXHR) {
                // build 2 html tables with acquired families data
                let th = `<th style="color:#0366D6">`
                var table = `<table style="border:1px solid #fffffe20; padding:0 2px;font-size:14px"><thead><tr>${th}Id</th>${th}Name</th>${th}SciName</th></tr></thead><tbody>`;
                var table2 = table;
                var index = 2;
                // determine number of rows for each of the 2 tables
                var cutSize = Math.trunc(familyList.length / 2);
                // max cutSize
                cutSize += familyList.length % 2 ? 1 : 0;

                // add a row at the time as we iterate through list
                for (family in familyList) {
                    let id = familyList[family].id;
                    let name = familyList[family].name;
                    let sciname = familyList[family].sciName;
                    let td = `<td style="padding-left:10px; color:#fff">`;
                    let tr = (index++ % 2 === 0) ? `<tr style="background-color:#444">` : `<tr>`;

                    if (family < cutSize) {
                        table = table.concat(`${tr}${td}${id}</td>${td}${name}</td>${td}${sciname}</td></tr>`);
                    }
                    else {
                        if (family == cutSize) {
                            // end first table
                            table = table.concat(`</tbody></table>`);
                        }
                        table2 = table2.concat(`${tr}${td}${id}</td>${td}${name}</td>${td}${sciname}</td></tr>`);
                    }
                }
                //end second table and display both tables
                table2 = table2.concat(`</tbody></table>`);
                $('#family-list').empty();
                $('#family-list').append(`${table}${table2}`);
                $('#reload-families').css('visibility', 'visible');
                reloadTabs.FAMILIES = 1;
            });
        loader(false, true);
    };

    function loadImages() {

        var url = "/Admin/GetImageTable";
        loader(true);
        $.ajax(
            {
                type: 'GET',
                url: url,
                dataType: 'json',
                cache: false,
                contentType: "application/json; charset=utf-8"
            }).fail(function (jqXHR, textStatus, errorThrown) {
                alert(errorThrown + this.url);
            }).done(function (imageList, textStatus, jqXHR) {
                // build html table with acquired data
                let th = `<th style="color:#0366D6">`;
                var table = `<table style="font-size:14px"><thead><tr>${th}Id</th>${th}BirdId</th>${th}FileName</th>${th}Location</th>${th}Date</th>${th}Country</th>${th}Coordinate</th>${th}Key</th>${th}</tr></thead><tbody>`;
                var index = 2;
                // add a row at the time as we iterate through list
                for (image in imageList) {
                    let id = imageList[image].id;
                    let birdid = imageList[image].birdId;
                    let filename = imageList[image].fileName;
                    let location = imageList[image].location;
                    let date = imageList[image].date;
                    let country = imageList[image].country;
                    let coordinate = imageList[image].coordinate;
                    let keyimage = imageList[image].keyImage == true ? 'x' : ' ';
                    let td = '<td style="padding-left: 10px; color:#fff">';
                    let tr = (index++ % 2 === 0) ? '<tr style="background-color:#444">' : '<tr>';
                    table = table.concat(`${tr}${td}${id}</td>${td}${birdid}</td>${td}${filename}</td>${td}${location}</td>` +
                        `${td}${date}</td>${td}${country}</td>${td}${coordinate}</td>${td}${keyimage}</td></tr>`);
                }
                table = table.concat(`</tbody></table>`);
                $('#image-table').empty();
                $('#image-table').append(`${table}`);
                $('#reload-image-data').css('visibility', 'visible');
                reloadTabs.IMAGES = 1;
            });
        loader(false, true);
    };   

    function buildStatsTable(imagesPerBird) {

        // build 3 html tables with acquired images per bird data
        let th = `<th style="color:#0366D6;padding-left:2px">`
        var table = `<table style="border:1px solid #fffffe20; padding:0 2px;font-size:14px"><thead><tr>${th}Bird (Id)</th>${th}Images</th></tr></thead><tbody>`;
        var table2 = table;
        var table3 = table;
        var index = 2;
        // determine number of rows for each of the 3 tables
        var cutSize = Math.trunc(imagesPerBird.length / 3);
        // max cutSize
        cutSize += imagesPerBird.length % 3 ? 1 : 0;
        
        // add a row at the time as we iterate through list
        for (bird in imagesPerBird) {
            let id = imagesPerBird[bird].id;
            let name = imagesPerBird[bird].name;
            let images = imagesPerBird[bird].images;
            let td = `<td style="padding-left: 8px; color:#fff">`;
            let tr = (index++ % 2 === 0) ? `<tr style="background-color:#444">` : `<tr>`;
            if (bird < cutSize) {
                table = table.concat(`${tr}${td}${name} (${id})</td>${td}${images}</td></tr>`);
            }
            else if (bird >= cutSize && bird < cutSize * 2) {
                if (bird == cutSize) {
                    // end first table
                    table = table.concat(`</tbody></table>`);
                }
                table2 = table2.concat(`${tr}${td}${name} (${id})</td>${td}${images}</td></tr>`);
            }
            else {
                if (bird == cutSize * 2) {
                    // end second table
                    table2 = table2.concat(`</tbody></table>`);
                }
                table3 = table3.concat(`${tr}${td}${name} (${id})</td>${td}${images}</td></tr>`);
            }
        }
        // end 3rd table, clear div and display all tables
        table3 = table3.concat(`</tbody></table>`);
        $('#stats-image-list').empty();
        $('#stats-image-list').append(`${table}${table2}${table3}`);
        $('#reload-stats').css('visibility', 'visible');
        $('#sort-stats').css('visibility', 'visible');
        reloadTabs.STATS = 1;
    }

    function loadStats() {
        var url = "/Admin/GetStats";
        loader(true);

        $.ajax(
            {
                type: 'GET',
                url: url,
                dataType: 'json',
                cache: false,
                contentType: "application/json; charset=utf-8"
            }).fail(function (jqXHR, textStatus, errorThrown) {
                alert(errorThrown + this.url);
                loadedStats = null;
            }).done(function (stats, textStatus, jqXHR) {

                //save stats for sort usage
                loadedStats = stats;

                // first build/display top stats list
                let value = stats.birdCount;
                let value2 = stats.haveKeyImages;
                let value3 = stats.birdCount - stats.haveKeyImages;
                let div = '<div style="padding:0 10px; color:#fff; margin-top:25px">';
                var statsList = `${div}<ul>` +
                    `<li> Birds: ${value} </li>` +
                    `<li> KeyImages: ${value2} </li>` +
                    `<li> Birds w/o images: ${value3} </li>` +
                    `</ul></div>`;
                $('#stats-list').empty();
                $('#stats-list').append(`${statsList}`);

                buildStatsTable(stats.imagesPerBird);
                
                reloadTabs.STATS = 1;
            });
        loader(false, true);
    };

    // function for (ascending) sorting images per bird count
    function compareImagesNumber(order = 'asc') {
        return function compare(a, b) {
            const imagesA = a.images;
            const imagesB = b.images;

            let comparison = 0;
            if (imagesA > imagesB) {
                comparison = 1;
            } else if (imagesA < imagesB) {
                comparison = -1;
            }
            return ((order === 'desc') ? (comparison * -1) : comparison);
        }
    };

    // sort and rebuild stats table
    function sortStats() {
        if (loadedStats == null)
            return;

        // save sorted list as so
        var sorted = null;
        if (sortAscending)
            sorted = [...loadedStats.imagesPerBird].sort(compareImagesNumber());
        else
            sorted = [...loadedStats.imagesPerBird].sort(compareImagesNumber('desc'));

        // reverse sort order for next time
        sortAscending ^= 1;

        buildStatsTable(sorted);
    };
    

    $('#add-image').click(function (e) {

        e.preventDefault();
        loader(true);
        if ($('#addImageForm').valid()) {

            var url = "/Admin/AddImage";
            var data = {
                BirdId: $('#birdId').val(),
                FileName: $('#fileName').val(),
                Location: $('#location').val(),
                Date: $('#date').val(),
                Country: $('#country').val(),
                Coordinate: $('#coordinates').val(),
                Comment: $('#comment').val(),
                KeyImage: $('#keyImage').prop('checked')
            };

            $.ajax(
                {
                    type: 'POST',
                    url: url,
                    dataType: 'text',
                    cache: false,
                    data: data
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    alert(errorThrown + this.url);
                }).done(function (res, textStatus, jqXHR) {
                    if (res === "0") {
                        alert('Error, no data added');
                    }
                    else {
                        $('#addImageForm')[0].reset();
                    }
                });
        }
        loader(false, true);
    });   

    $('#update-image').click(function (e) {

        e.preventDefault();
        loader(true);
        if ($('#updateImageForm').valid()) {

            var url = "/Admin/UpdateImage";
            var data = {
                ImageId: $('#image-id').val(),
                FileName: $('#image-fileName').val(),
                Location: $('#image-location').val(),
                Date: $('#image-date').val(),
                Country: $('#image-country').val(),
                Coordinate: $('#image-coordinates').val(),
                Comment: $('#image-comment').val()
            };

            $.ajax(
                {
                    type: 'POST',
                    url: url,
                    dataType: 'text',
                    cache: false,
                    data: data
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    alert(errorThrown + this.url);
                }).done(function (res, textStatus, jqXHR) {
                    if (res === "0") {
                        alert('Error, no data updated');
                    }
                    else {
                        $('#updateImageForm')[0].reset();
                    }
                });
        }
        loader(false, true);
    });   


    $('#remove-image').click(function (e) {

        e.preventDefault();
        loader(true);

        if ($('#removeImageForm').valid()) {

            var url = "/Admin/RemoveImage";
            var data = {
                ImageId: $('#remove-image-id').val()              
            };

            $.ajax(
                {
                    type: 'POST',
                    url: url,
                    dataType: 'text',
                    cache: false,
                    data: data
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    alert(errorThrown + this.url);
                }).done(function (res, textStatus, jqXHR) {
                    if (res === "0") {
                        alert('Error, no image removed');
                    }
                    else {
                        $('#removeImageForm')[0].reset();
                    }
                });
        }
        loader(false, true);
    });   

    $('#add-bird').click(function (e) {

        e.preventDefault();
        loader(true);
        if ($('#addBirdForm').valid()) {

            var url = "/Admin/AddBird";
            var data = {
                Name: $('#name').val(),
                SciName: $('#sciName').val(),
                FamilyId: $('#familyId').val()
            };

            $.ajax(
                {
                    type: 'POST',
                    url: url,
                    dataType: 'text',
                    cache: false,
                    data: data
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    alert(errorThrown + this.url);
                }).done(function (res, textStatus, jqXHR) {
                    if (res === "0") {
                        alert('Error, no data added');
                    }
                    else {
                        $('#addBirdForm')[0].reset();
                    }
                });
        }
        loader(false, true);
    });   

    init();
});