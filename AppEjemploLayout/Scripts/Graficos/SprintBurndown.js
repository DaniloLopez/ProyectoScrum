function IniciarGrafico() {
    var proyectoId = document.getElementById('txt-idProyecto').value;
    Do_GetCall("/api/Graficos", [proyectoId],
        dibujarGrafico);
}



function dibujarGrafico(data) {
    var rows = [];
    var index = 0;
    
    var total = 0;
    for (var i in data) {
        total+= data[i].Cantidad;
    }

    var previo = 0;
    for (var i in data) {
        var obj = data[i];

        rows[index] = [obj.Dato, total - previo - obj.Cantidad];
        previo += obj.Cantidad;
        index++;
    }

    var funGraficar = drawLineChart('chart-div',
        'Sprint burndown', 'Sprints', 'Horas',
        rows);
    google.charts.setOnLoadCallback(funGraficar);
}



