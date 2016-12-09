google.charts.load('current', { packages: ['corechart', 'line'] })


function drawPieChart(elementId, title, chartData, width, height, colors) {

    width = width || 300;
    height = height || 400;

    colors = colors || ['#247BA0', '#F25F5C', '#50514F', '#FFE066', '#f6c7b6'];

    return function () {
        var data = new google.visualization.DataTable();
        data.addColumn('string', 'Data1');
        data.addColumn('number', 'Data2');
        data.addRows(chartData);

        var options = {
            'title': title,
            'width': width,
            'height': height,
            'is3D': true,
            'colors': colors
        };
        var chart = new google.visualization.PieChart(document.getElementById(elementId));
        chart.draw(data, options);
    };

}

function drawLineChart(elementid, title, xAxis, yAxis,
    rows, chartWidth, chartHeight) {

    return function () {
        var data = new google.visualization.DataTable();
        data.addColumn('string', 'X');
        data.addColumn('number', 'Rendimiento');

        data.addRows(rows);

        var options = {
            hAxis: {
                title: xAxis
            },
            vAxis: {
                title: yAxis
            },
            colors: ['#000'],
            'width': 900,
            'height': 300
        };

        var chart = new google.visualization.LineChart(document.getElementById(elementid));
        chart.draw(data, options);
    }
}