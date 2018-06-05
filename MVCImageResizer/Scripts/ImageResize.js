(function () {

    var resultDiv = $('#resultDiv');

    $('#form0').ajaxForm({
        beforeSerialize: function () {

        },
        success: function (d) {

        },
        complete: function (xhr) {

            var obj = jQuery.parseJSON(xhr.responseText);

            alert(obj.Data);
            
            //<div class="col-xs-12 text-center">

            var newDiv = document.createElement('div');
            newDiv.setAttribute('class', 'col-xs-12 text-center');

            var newImg = document.createElement('img');
            newImg.setAttribute('class', 'image-preview');
            newImg.setAttribute('src', obj.src);

            newDiv.appendChild(newImg);

            resultDiv.append(newDiv);

        }
    });







    /*
    var resizeTypeSelect = $('#resizeTypeSelect');
    var resizeType = $('#resizeType');

    ///
    ///
    //defining the settings
    ///
    ///
    var MVCImageResizeTypes = {
        ByWidth: 1,
        ByHeight: 2,
        ByRatio: 3
    };

    ///
    ///
    //set the settings
    ///
    ///

    //default value
    resizeType.attr("value", MVCImageResizeTypes.ByWidth);

    resizeTypeSelect.change(function () {
        var changeResult = resizeTypeSelect.val();
        if (changeResult == MVCImageResizeTypes.ByWidth) {
            resizeType.attr("value", MVCImageResizeTypes.ByWidth);
        }
        else if (changeResult == MVCImageResizeTypes.ByHeight) {
            resizeType.attr("value", MVCImageResizeTypes.ByHeight);
        }
        else if (changeResult == MVCImageResizeTypes.ByRatio) {
            resizeType.attr("value", MVCImageResizeTypes.ByRatio);
        }
    });

    */
})();