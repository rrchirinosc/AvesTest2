window.addEventListener('DOMContentLoaded', (event) => {

   
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
                $('#bird-table').empty();
                $('#bird-table').append(`<table>`);
                for (bird in birdList) {
                    let id = birdList[bird].id;
                    let name = birdList[bird].name;
                    let sciname = birdList[bird].sciName;
                    let familyid = birdList[bird].familyId;
                    let td = '<td style="padding:0 10px; color:#fff">';
                    $('#bird-table').append(`<tr>${td}${id}</td>${td}${name}</td>${td}${sciname}</td>${td}${familyid}</td></tr>`);
                }
                $('#bird-table').append(`</table>`);
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
                $('#family-table').empty();
                $('#family-table').append(`<table">`);
                for (family in familyList) {
                    let id = familyList[family].id;
                    let name = familyList[family].name;
                    let sciname = familyList[family].sciName;
                    let td = '<td style="padding:0 10px; color:#fff">';
                    $('#family-table').append(`<tr>${td}${id}</td>${td}${name}</td>${td}${sciname}</td></tr>`);
                }
                $('#family-table').append('</table>');
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
                $('#image-table').empty();
                $('#image-table').append(`<table>`);
                for (image in imageList) {
                    let birdid = imageList[image].birdId;
                    let filename = imageList[image].fileName;
                    let location = imageList[image].location;
                    let date = imageList[image].date;
                    let country = imageList[image].country;
                    let coordinate = imageList[image].coordinate;
                    let keyimage = imageList[image].keyImage;
                    let td = '<td style="padding:0 10px; color:#fff">';
                    $('#image-table').append(`<tr>${td}${birdid}</td>${td}${filename}</td>${td}${location}</td>` +
                        `${td}${date}</td>${td}${country}</td>${td}${coordinate}</td>${td}${keyimage}</td></tr>`);
                }
                $('#image-table').append('</table>');
            });
    });       
});