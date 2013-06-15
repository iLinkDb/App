//bh namesapce
if (typeof am == 'undefined' || !am) {
    am = {};
}

am.EditDialog = function (formName) {
    console.dir(formName);
    console.dir(this);

    //    $('#modalEdit').on('hide', function () {
    var form = $('#' + formName);
    form.validate().resetForm();
    form.get(0).reset();
    form.removeData('itemOne');
    form.find("input[type='hidden'][id='id']").remove();
    //    });

};

am.PostEdit = function (form) {
    form = $(form);
    //                if (!form.valid())
    //                    return;
    var json = JSON.stringify(am._getItemFromForm(form));

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
                var match = ko.utils.arrayFirst(self.itemList(), function (item) {
                    return jsonObject.id === item.id();
                });
                match.updateItem(jsonObject);
            }
            else {
                self.itemList.push(new itemOne(jsonObject));
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