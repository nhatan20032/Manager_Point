function count_teacher_subject() {
    $.ajax({
        url: 'https://localhost:44335/user/Count_Teachers_By_Subject',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            // Chuyển đổi dữ liệu từ API thành mảng ApexCharts yêu cầu
            var chartData = data.map(function (item) {
                return {
                    x: item.SubjectName,
                    y: item.TeacherCount
                };
            });
            console.log(chartData);
            // Cấu hình biểu đồ
            var options = {
                series: chartData.map(function (item) {
                    return item.y;
                }),
                chart: {
                    type: 'pie',
                    height: 350
                },
                labels: chartData.map(function (item) {
                    return item.x;
                }),
                responsive: [{
                    breakpoint: 480,
                    options: {
                        chart: {
                            width: 200
                        },
                        legend: {
                            position: 'bottom'
                        }
                    }
                }]
            };

            // Render biểu đồ với cấu hình và dữ liệu đã chuyển đổi từ API
            var chart = new ApexCharts(document.querySelector("#chart_teacher_subject"), options);
            chart.render();
        },
        error: function (xhr, status, error) {
            console.error('Error calling API:', error);
        }
    });
}
function count_student_teacher() {
    $.ajax({
        url: 'https://localhost:44335/user/count_all_teacher_student',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            // Chuyển đổi dữ liệu từ API thành mảng ApexCharts yêu cầu
            var chartData = [
                {
                    name: 'Tổng số giáo viên',
                    data: [data.totalTeachers]
                },
                {
                    name: 'Tổng số học sinh',
                    data: [data.totalStudents]
                }
            ];

            // Cấu hình biểu đồ
            var options = {
                series: chartData,
                chart: {
                    type: 'bar',
                    height: 350
                },
                plotOptions: {
                    bar: {
                        horizontal: true,
                        columnWidth: '55%',
                        endingShape: 'rounded'
                    },
                },
                dataLabels: {
                    enabled: false
                },
                stroke: {
                    show: true,
                    width: 2,
                    colors: ['transparent']
                },
                xaxis: {
                    categories: ['Tổng']
                },
                fill: {
                    opacity: 1
                },
                tooltip: {
                    y: {
                        formatter: function (val) {
                            return val + " people"
                        }
                    }
                }
            };

            // Render biểu đồ với cấu hình và dữ liệu đã chuyển đổi từ API
            var chart = new ApexCharts(document.querySelector("#chart_teacher_student"), options);
            chart.render();
        },
        error: function (xhr, status, error) {
            console.error('Error calling API:', error);
        }
    });
}
function count_student_course() {
    $.ajax({
        url: 'https://localhost:44335/user/Count_Students_By_Course',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            // Chuyển đổi dữ liệu từ API thành mảng ApexCharts yêu cầu
            var chartData = data.map(function (item) {
                return {
                    x: item.CourseName,
                    y: item.Student
                };
            });
            console.log(chartData);
            // Cấu hình biểu đồ
            var options = {
                series: chartData.map(function (item) {
                    return item.y;
                }),
                chart: {
                    type: 'pie',
                    height: 350
                },
                labels: chartData.map(function (item) {
                    return item.x;
                }),
                responsive: [{
                    breakpoint: 480,
                    options: {
                        chart: {
                            width: 200
                        },
                        legend: {
                            position: 'bottom'
                        }
                    }
                }]
            };

            // Render biểu đồ với cấu hình và dữ liệu đã chuyển đổi từ API
            var chart = new ApexCharts(document.querySelector("#chart_student_course"), options);
            chart.render();
        },
        error: function (xhr, status, error) {
            console.error('Error calling API:', error);
        }
    });
}