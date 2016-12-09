function dibujarGrafico(data) {
    var rows = [];
    var index = 0;
    for (var i in data) {
        var obj = data[i];

        rows[index] = [obj.Dato, obj.Cantidad];
        index++;
    }

    var funGraficar = drawLineChart('chart-div',
        'Sprint burndown', 'Sprints', 'Horas',
        rows);
    google.charts.setOnLoadCallback(funGraficar);
}


window.onload = function () {
    var cursoId = document.getElementById('metaInfo').innerText;
    Do_RestCall("/api/InfoApi/getRenSesiones", cursoId,
        dibujarGrafico);
}