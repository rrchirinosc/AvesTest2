window.addEventListener('DOMContentLoaded', (event) => {

    var init = function () {
        $.validator.unobtrusive.parse("#addBirdForm");
        $.validator.unobtrusive.parse("#addImageForm");
        $.validator.unobtrusive.parse("#updateImageForm");
        $.validator.unobtrusive.parse("#removeImageForm");
    };

    $('#load-birds').click(function () {
        var url = "/Admin/GetBirdTable";
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
                    let td = '<td style="padding:0 10px; color:#fff">';
                    let tr = (index++ % 2 === 0) ? '<tr style="background-color:#444">' : '<tr>';
                    table= table.concat(`${tr}${td}${id}</td>${td}${name}</td>${td}${sciname}</td>${td}${familyid}</td></tr>`);                   
                }
                table = table.concat(`</tbody></table>`);
                $('#bird-table').empty();
                $('#bird-table').append(`${table}`);
        });
    });

    $('#load-families').click(function () {
        var url = "/Admin/GetFamilyTable";
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
                // determine number of rows for each of the 2 tables (last one could be larger)
                var cutSize = familyList.length / 2;
                // add a row at the time as we iterate through list
                for (family in familyList) {
                    let id = familyList[family].id;
                    let name = familyList[family].name;
                    let sciname = familyList[family].sciName;
                    let td = `<td style="padding:0 10px; color:#fff">`;
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
                $('#load-families').hide();
                $('#family-list').empty();
                $('#family-list').append(`${table}${table2}`);
            });
    });

    $('#load-stats').click(function () {
        var url = "/Admin/GetStats";
        $.ajax(
            {
                type: 'GET',
                url: url,
                dataType: 'json',
                cache: false,
                contentType: "application/json; charset=utf-8"
            }).fail(function (jqXHR, textStatus, errorThrown) {
                alert(errorThrown + this.url);
            }).done(function (stats, textStatus, jqXHR) {
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

                // build 3 html tables with acquired images per bird data
                let th = `<th style="color:#0366D6;padding-left:2px">`
                var table = `<table style="border:1px solid #fffffe20; padding:0 2px;font-size:14px"><thead><tr>${th}Bird (Id)</th>${th}Images</th></tr></thead><tbody>`;
                var table2 = table;
                var table3 = table;
                var index = 2;
                // determine number of rows for each of the 3 tables (last one could be larger)
                var cutSize = stats.imagesPerBird.length / 3;
                // add a row at the time as we iterate through list
                for (bird in stats.imagesPerBird) {
                    let id = stats.imagesPerBird[bird].id;
                    let name = stats.imagesPerBird[bird].name;
                    let images = stats.imagesPerBird[bird].images;
                    let td = `<td style="padding:0 10px; color:#fff">`;
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
            });
    });

    $('#load-image-data').click(function () {
        var url = "/Admin/GetImageTable";
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
                var table = `<table style="font-size:14px"><thead><tr>${th}Id</th>${th}BirdId</th>${th}FileName</th>${th}Location</th>${th}Date</th>${th}Country</th>${th}Coordinate</th>${th}KeyImage</th>${th}</tr></thead><tbody>`;
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
                    let td = '<td style="padding:0 8px; color:#fff">';
                    let tr = (index++ % 2 === 0) ? '<tr style="background-color:#444">' : '<tr>';
                    table = table.concat(`${tr}${td}${id}</td>${td}${birdid}</td>${td}${filename}</td>${td}${location}</td>` +
                        `${td}${date}</td>${td}${country}</td>${td}${coordinate}</td>${td}${keyimage}</td></tr>`);
                }
                table = table.concat(`</tbody></table>`);
                $('#image-table').empty();
                $('#image-table').append(`${table}`);
            });
    });   

    $('#add-image').click(function (e) {

        e.preventDefault();

        if ($('#addImageForm').valid()) {

            var url = "/Admin/AddImage";
            var data = {
                BirdId: $('#birdId').val(),
                FileName: $('#fileName').val(),
                Location: $('#location').val(),
                Date: $('#date').val(),
                Country: $('#country').val(),
                Coordinate: $('#coordinates').val(),
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
    });   

    $('#update-image').click(function (e) {

        e.preventDefault();

        if ($('#updateImageForm').valid()) {

            var url = "/Admin/UpdateImage";
            var data = {
                ImageId: $('#image-id').val(),
                FileName: $('#image-fileName').val(),
                Location: $('#image-location').val(),
                Date: $('#image-date').val(),
                Country: $('#image-country').val(),
                Coordinate: $('#image-coordinates').val(),
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
    });   


    $('#remove-image').click(function (e) {

        e.preventDefault();

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
    });   

    $('#add-bird').click(function (e) {

        e.preventDefault();

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
    });   

    init();
});