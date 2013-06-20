//bh namesapce
if (typeof am == 'undefined' || !am) {
    am = {};
}

am.EditDialog = function (formName) {
    console.log('am.EditDialog');
    console.dir(formName);

    //    $('#modalEdit').on('hide', function () {
    var form = $('#' + formName);
    console.dir(form);
    form.validate().resetForm();
    form.get(0).reset();
    form.removeData('itemOne');
    console.log("after removeData");
    form.find("input[type='hidden'][id='id']").remove();
    console.log("after remove()");
    //    });

};

am.PostEdit = function (form) {
    //console.log('am.PostEdit');
    //console.dir(form);

    form = $(form);
    //                if (!form.valid())
    //                    return;
//    am.assert(viewModel != undefined, "viewModel != undefined");
    var json = JSON.stringify(am._getItemFromForm(form));

    console.log('In PostEdit');

    var update = form.find("input[type='hidden'][id='id']").val();
    console.log('update: ' + update);
    var httpVerb = !update ? "POST" : "PUT";

    var self = this;
    $.ajax({
        url: 'http://' + restHost + '/' + controller + '/' + httpVerb.toLowerCase(),
        type: httpVerb,
        data: json,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (jsonObject) {
            console.log('self');
            console.dir(self);
            if (update) {
                var match = ko.utils.arrayFirst(viewModel.itemList(), function (item) {
                    return jsonObject.id === item.id();
                });
                match.updateItem(jsonObject);
            }
            else {
                viewModel.itemList.push(new itemOne(jsonObject));
            }
            $('#modalEdit').modal('hide');
        }
    });
}

am._getItemFromForm = function (form) {
    form = $(form);
    var itemOne = {};
    form.find('input[type!=submit],select').each(function () {
        itemOne[this.name] = $(this).val();
    });
    return itemOne;
}

am.assert = function(value, desc) {
    var li = document.createElement("li");
    li.className = value ? "pass" : "fail";
    li.appendChild(document.createTextNode(desc));
    document.getElementById("results").appendChild(li);
}