
var urlOriginal = "";
window.onload = function () {
    IniciarGrafico();
    urlOriginal = "" + window.location.href;
    cargarAutocompletado('nombreUsuario', '/api/AutoComplete', function () {
        var obj = $("#nombreUsuario").getSelectedItemData();
        var elementoBoton = document.getElementById("btn-insertarUsuario");
        elementoBoton.onclick = guardarIntegrante(obj.UsuarioId);
        //var elemId = document.getElementById("txt_id");
        //elemId.value = obj.correoElectronicoUsuario;
    });
    $('frm-agregar').submit(function (e) {
        e.preventDefault();
        window.history.back();
    });

    
};

function guardarIntegrante(IdUsuario) {
    return function () {
        var IdProyecto = document.getElementById("txt-idProyecto").value;
        var elem_rol = document.getElementById("cmb-rol");
        var rol = elem_rol.options[elem_rol.selectedIndex].text;
        var urlnew = '../Proyectoes/UsuariosProyecto?IdProyecto='+IdProyecto;
        
        $.ajax({
            url: '/api/AgregarAEquipo/' + IdProyecto + '/' + IdUsuario + '/' + rol,
            cache: false,
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            dataType: "json",
            success: function (data) {
                window.location.href = urlOriginal;
            }
        }).fail(
        function (xhr, textStatus, err) {
            alert("mal =(");
            window.location.href = urlOriginal;
        });
    }
    
}



function cargarAutocompletado(inputName, baseurl, fun) {
    var options = {
        url: function (searchText) {
            return baseurl + "/" + searchText;
        },
        getValue: function (element) {
            return element.Nombre
        },
        list: {
            onChooseEvent: fun
        }
    };
    $('#' + inputName).easyAutocomplete(options);
}

