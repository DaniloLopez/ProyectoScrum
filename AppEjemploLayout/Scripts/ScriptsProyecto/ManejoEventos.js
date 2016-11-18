window.onload = function () {
    cargarAutocompletado('txt_nombrePersona', 'api/AutoComplete', function () {

        var obj = $("#txt_nombrePersona").getSelectedItemData();

        var elemId = document.getElementById("txt_id");
        elemId.value = obj.correoElectronicoUsuario;
    });
};


function cargarAutocompletado(inputName, baseurl, fun) {
    var options = {
        url: function (searchText) {
            return baseurl + "/" + searchText;
        },
        getValue: function (element) {
            return element.nombresUsuario + " " + element.apellidosUsuario;
        },
        list: {
            onChooseEvent: fun
        }
    };
    $('#' + inputName).easyAutocomplete(options);
}

