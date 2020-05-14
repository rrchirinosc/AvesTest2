window.addEventListener('DOMContentLoaded', (event) => {

    var init = function () {
        $.validator.unobtrusive.parse("#addBirdForm");
        $.validator.unobtrusive.parse("#addImageForm");
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
                //alert('bird table loaded');
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
                //alert('family table loaded');
                let th = `<th style="color:#0366D6">`
                var table = `<table><thead><tr>${th}Id</th>${th}Name</th>${th}SciName</th></tr></thead><tbody>`;
                var index = 2;
                for (family in familyList) {
                    let id = familyList[family].id;
                    let name = familyList[family].name;
                    let sciname = familyList[family].sciName;
                    let td = `<td style="padding:0 10px; color:#fff">`;
                    let tr = (index++ % 2 === 0) ? `<tr style="background-color:#444">` : `<tr>`;
                    table = table.concat(`${tr}${td}${id}</td>${td}${name}</td>${td}${sciname}</td></tr>`);
                }
                table = table.concat(`</tbody></table>`);
                $('#family-table').empty();
                $('#family-table').append(`${table}`);
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
                //alert('image table loaded');
                let th = `<th style="color:#0366D6">`;
                var table = `<table><thead><tr>${th}Id</th>${th}BirdId</th>${th}FileName</th>${th}Location</th>${th}Date</th>${th}Country</th>${th}Coordinate</th>${th}KeyImage</th>${th}</tr></thead><tbody>`;
                var index = 2;
                for (image in imageList) {
                    let id = imageList[image].id;
                    let birdid = imageList[image].birdId;
                    let filename = imageList[image].fileName;
                    let location = imageList[image].location;
                    let date = imageList[image].date;
                    let country = imageList[image].country;
                    let coordinate = imageList[image].coordinate;
                    let keyimage = imageList[image].keyImage;
                    let td = '<td style="padding:0 8px; color:#fff">';
                    let tr = (index++ % 2 === 0) ? '<tr style="background-color:#444">' : '<tr>';
                    table = table.concat(`${tr}${td}${id}</td>${td}${birdid}</td>${td}${filename}</td>${td}${location}</td>` +
                        `${td}${date}</td>${td}${country}</td>${td}${coordinate}</td>${td}${keyimage}</td></tr>`);
                }
                $('#image-table').empty();
                $('#image-table').append(`${table}`);
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
                //alert('stats loaded');
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

                //TODO: column styling
                let th = `<th style="color:#0366D6">`
                var table = `<table><thead><tr>${th}Bird (Id)</th>${th}Images</th></tr></thead><tbody>`;
                var index = 2;
                for (bird in stats.imagesPerBird) {
                    let id = stats.imagesPerBird[bird].id;
                    let name = stats.imagesPerBird[bird].name;
                    let images = stats.imagesPerBird[bird].images;
                    let td = `<td style="padding:0 10px; color:#fff">`;
                    let tr = (index++ % 2 === 0) ? `<tr style="background-color:#444">` : `<tr>`;
                    table = table.concat(`${tr}${td}${name} (${id})</td>${td}${images}</td></tr>`);
                }
                table = table.concat(`</tbody></table>`);
                $('#stats-image-list').empty();
                $('#stats-image-list').append(`${table}`);
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