var tareas = [];

var estados = ['Aceptada', 'En Desarrollo', 'Pendiente de Pruebas', 'Terminada']
var idusuario = '1';
var sprintId = '1';

function Iniciar() {
    idusuario = document.getElementById('input-idUsuario').value;
    sprintId = document.getElementById('input-idSprint').value;
    Do_GetCall('/api/Tareas/GetSprint', [sprintId], cargarSprints(idusuario));
    Do_GetCall('/api/Tareas/GetTareasAsignar', [sprintId], cargarTareasAsignar);
}

function cargarTareasAsignar(data) {
    limpiarElemento('ul-tareasAsignar');
    var elemLista = document.getElementById('ul-tareasAsignar');
    for (var i in data) {
        var li = document.createElement('li');
        li.appendChild(crearCajaTarea(data[i]));
        elemLista.appendChild(li);
    }
    
}

function cargarSprints(idusuario) {
    return function (data) {
        limpiarElemento('span-sprint');
        var elemSpan = document.getElementById('span-sprint');
        
        elemSpan.appendChild(document.createTextNode(data.SprintId));
        Do_GetCall('/api/Tareas/GetTareas', [idusuario, data.SprintId], cargarTareas(data));
    }
}

function limpiarElemento(nombreElemento) {
    var elemento = document.getElementById(nombreElemento);
    while (elemento.firstChild) {
        elemento.removeChild(elemento.firstChild);
    }
}

function cargarTareas(sprint) {
    return function (data) {
        limpiarElemento('tblbody');
        var tr = document.createElement('tr');
        for (var index in estados) {
            var estadoActual = estados[index];
            var td = document.createElement('td');
            $(td).droppable({
                classes: {
                    "ui-droppable-hover": "ui-state-hover"
                },
                drop: crearFuncionDroppable(estadoActual),
                accept: crearFuncionAccept(estadoActual)

            });
            for (var i in data) {
                var tarea = data[i];
                if (tarea.estado == estadoActual) {
                    td.appendChild(crearCajaTarea(tarea));
                }
                tr.appendChild(td);
            }
        }
        var tblBody = document.getElementById('tblbody');
        tblBody.appendChild(tr);
    }
    
}

function crearCajaTarea(tarea) {
    var mainDiv = document.createElement('div');
    var innerDiv = document.createElement('div');
    innerDiv.appendChild(document.createTextNode("Tarea: " + tarea.asunto));
    innerDiv.appendChild(document.createElement('br'));
    innerDiv.appendChild(document.createTextNode("Descripción: " + tarea.descripcion));
    innerDiv.appendChild(document.createElement('br'));
    innerDiv.appendChild(document.createTextNode("Tiempo Estimado: " + tarea.estimacionHoras));
    innerDiv.setAttribute('class', 'bx-tarea');
    mainDiv.tarea = tarea;
    mainDiv.appendChild(innerDiv);
    $(mainDiv).draggable({
        revert: 'invalid',
        scroll: true
    });
    return mainDiv;
}

function crearFuncionDroppable(estado) {
    return function (event, ui) {
        var $this = $(this);
        ui.draggable.position({
            my: "center",
            at: "center",
            of: $this,
            using: function (pos) {
                $(this).animate(pos, 600, "linear");
            }
        });
        ui.draggable[0].tarea.estado = estado;
        ui.draggable[0].tarea.UsuarioId = idusuario;
        editarTarea(ui.draggable[0].tarea);
    }
}

function crearFuncionAccept(estado) {
    return function (dropElem) {
        return (dropElem[0].tarea.estado !== estado);
    }
}


