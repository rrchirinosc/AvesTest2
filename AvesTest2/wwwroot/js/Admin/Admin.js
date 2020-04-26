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
                let th = `<th style="color:#0366D6">`
                var table = `<table><thead><tr>${th}BirdId</th>${th}FileName</th>${th}Location</th>${th}Date</th>${th}Country</th>${th}Coordinate</th>${th}KeyImage</th>${th}</tr></thead><tbody>`;
                var index = 2;
                for (image in imageList) {
                    let birdid = imageList[image].birdId;
                    let filename = imageList[image].fileName;
                    let location = imageList[image].location;
                    let date = imageList[image].date;
                    let country = imageList[image].country;
                    let coordinate = imageList[image].coordinate;
                    let keyimage = imageList[image].keyImage;
                    let td = '<td style="padding:0 10px; color:#fff">';
                    let tr = (index++ % 2 === 0) ? '<tr style="background-color:#444">' : '<tr>';
                    table = table.concat(`${tr}${td}${birdid}</td>${td}${filename}</td>${td}${location}</td>` +
                        `${td}${date}</td>${td}${country}</td>${td}${coordinate}</td>${td}${keyimage}</td></tr>`);
                }
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