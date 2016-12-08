



function editarTarea(tarea) {
    var id = tarea.TareaSprintId;
    var cita = JSON.stringify(tarea);
    var urlNueva = '/api/CrudTareas' + '?' + "id= " + id;
    $.ajax({
        url: urlNueva,
        cache: false,
        type: 'PUT',
        contentType: 'application/json; charset=utf-8',
        data: cita,
        dataType: "json",
        success: function (data) {
            Iniciar();
        }
    }).fail(
    function (xhr, textStatus, err) {
        alert("No se pudo completar la solicitud");
    });
}

