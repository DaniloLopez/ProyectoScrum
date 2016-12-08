
function EditarNuevaCita(objCita) {
    var datoCita = {
        IdDoctor: objCita.doctor,
        Fecha: objCita.fecha,
        Hora: objCita.hora,
        Duracion: objCita.duracion,
        Id: objCita.id,
        IdPaciente: objCita.pacienteId,
        Motivo: objCita.motivoConsulta,
        Observaciones: objCita.observaciones,
        Estado: objCita.estado,
        Color: objCita.color,
        PrioridadId: objCita.prioridad,
        Asistencia: objCita.asistencia
    };
    editarCita(datoCita);
}



function editarTarea(tarea) {
    var id = tarea.TareaSprintId;
    var cita = JSON.stringify(tarea);
    $.ajax({
        url: '../api/CrudTareas' + '?' + "id= " + id,
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

