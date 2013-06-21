//bh namesapce
if (typeof am == 'undefined' || !am) {
    am = {};
}

am.DialogHide = function (formName) {
    var form = $('#' + formName);
    form.validate().resetForm();
    form.get(0).reset();
    form.removeData('itemOne');
    form.find("input[type='hidden'][id='id']").remove();
};

am.DialogShow = function (formName) {
    var form = $('#' + formName);
    var itemOne = form.data('itemOne');
    if (!itemOne)
        return;

    $('<input>').attr('type', 'hidden')
                .attr('id', 'id')
                .attr('name', 'id')
                .val(itemOne.id()).prependTo(form);
    form.find('#Domain').val(itemOne.domain());
}

am.PostEdit = function (form, modalName) {
    form = $(form);
    //                if (!form.valid())
    //                    return;
    var json = JSON.stringify(am._getItemFromForm(form));

    console.log('In PostEdit');
    console.dir(json);

    var update = form.find("input[type='hidden'][id='id']").val();
    var httpVerb = !update ? "POST" : "PUT";

    var self = this;
    $.ajax({
        url: 'http://' + restHost + '/' + controller + '/' + httpVerb.toLowerCase(),
        type: httpVerb,
        data: json,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (jsonObject) {
            if (update) {
                var match = ko.utils.arrayFirst(viewModel.itemList(), function (item) {
                    return jsonObject.id === item.id();
                });
                match.updateItem(jsonObject);
            }
            else {
                viewModel.itemList.push(new itemOne(jsonObject));
            }
            $('#' + modalName).modal('hide');
        }
    });
}

am.PostDelete = function (form, modalName) {
    form = $(form);
    var json = JSON.stringify(this._getItemFromForm(form));

    var deleteId = form.find("input[type='hidden'][id='id']").val();

    var self = this;
    $.ajax({
        url: 'http://' + restHost + '/' + controller + '/delete',
        type: 'DELETE',
        data: json,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (success) {
            if (success) {
                var match = ko.utils.arrayFirst(viewModel.itemList(), function (item) {
                    return deleteId == item.id();
                });
                viewModel.itemList.remove(match);
            }
            $('#' + modalName).modal('hide');
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

am.assert = function (value, desc) {
    var li = document.createElement("li");
    li.className = value ? "pass" : "fail";
    li.appendChild(document.createTextNode(desc));
    document.getElementById("assertResults").appendChild(li);
}

am.initModal = function (modalName) {
    // Without this line, the dialog closes when somebody clicks outside of it.
    $('#' + modalName).modal({ "backdrop": "static", show: false });
}