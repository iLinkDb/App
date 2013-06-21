//bh namesapce
if (typeof am == 'undefined' || !am) {
    am = {};
}

am.assert = function (value, desc) {
    var li = document.createElement("li");
    li.className = value ? "pass" : "fail";
    li.appendChild(document.createTextNode(desc));
    document.getElementById("assertResults").appendChild(li);
}

am.DialogHide = function (formName) {
    var form = $('#' + formName);
    form.validate().resetForm();
    form.get(0).reset();
    form.removeData('tenant');
    form.find("input[type='hidden'][id='id']").remove();
};

am.PostEdit = function (form, modalName, viewModel) {
    form = $(form);
    //                if (!form.valid())
    //                    return;
    var json = JSON.stringify(am.GetItemFromForm(form));

    // console.log('In PostEdit');

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
                viewModel.itemList.push(new tenant(jsonObject));
            }
            $('#' + modalName).modal('hide');
        }
    });
}

am.PostDelete = function (form, modalName, viewModel) {
    form = $(form);
    var json = JSON.stringify(this.GetItemFromForm(form));

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

am.GetItemFromForm = function (form) {
    form = $(form);
    var item = {};
    form.find('input[type!=submit],select').each(function () {
        item[this.name] = $(this).val();
    });
    return item;
}

am.initModal = function (modalName) {
    // Without this line, the dialog closes when somebody clicks outside of it.
    $('#' + modalName).modal({ "backdrop": "static", show: false });
}