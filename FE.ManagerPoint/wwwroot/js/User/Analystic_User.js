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
                title: {
                    text: 'Số lượng giáo viên theo từng môn'
                },
                chart: {
                    type: 'pie',
                    height: 350,
                    toolbar: {
                        show: true,
                        tools: {
                            download: true,
                            selection: true,
                            zoom: true,
                            zoomin: true,
                            zoomout: true,
                            pan: true
                        },
                        export: {
                            csv: {
                                filename: 'Teacher_Subject.csv',
                                columnDelimiter: ',',
                                headerCategory: 'Môn học',
                                headerValue: 'Số lượng giáo viên',
                                dateFormatter: function (timestamp) {
                                    return new Date(timestamp).toDateString();
                                }
                            },
                            svg: {
                                filename: 'Teacher_Subject.svg'
                            },
                            png: {
                                filename: 'Teacher_Subject.png'
                            }
                        },
                        autoSelected: 'zoom'
                    }
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

function count_student_rank_gradelevel() {
    $.ajax({
        url: 'https://localhost:44335/user/Count_All_Rank_Student_GradeLevel',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var gradeLevel = data.map(item => item.GradeLevel);
            var seriesData = {};

            data.forEach(item => {
                item.Ranks.forEach(rank => {
                    if (!seriesData[rank.Rank]) {
                        seriesData[rank.Rank] = [];
                    }
                    seriesData[rank.Rank].push(rank.StudentCount);
                });
            });

            var series = Object.keys(seriesData).map(rank => {
                return {
                    name: rank,
                    data: seriesData[rank]
                };
            });

            var options = {
                chart: {
                    type: 'bar',
                    height: 450,
                    stacked: true
                },
                series: series,
                xaxis: {
                    categories: gradeLevel
                },
                plotOptions: {
                    bar: {
                        horizontal: true,
                        columnWidth: '55%',
                        columnHeigth: '55%',
                        endingShape: 'rounded'
                    },
                },
                title: {
                    text: 'Số học lực của học sinh theo từng khối'
                }
            };

            var chart = new ApexCharts(document.querySelector("#chart_all_rank_student_gradelevel"), options);
            chart.render();
        },
        error: function (error) {
            console.log('Error fetching data:', error);
        }
    });
}


function count_student_rank_year() {
    $.ajax({
        url: 'https://localhost:44335/user/Count_All_Rank_Student_Year',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var years = data.map(item => item.Year);
            var seriesData = {};

            data.forEach(item => {
                item.Ranks.forEach(rank => {
                    if (!seriesData[rank.Rank]) {
                        seriesData[rank.Rank] = [];
                    }
                    seriesData[rank.Rank].push(rank.Count);
                });
            });

            var series = Object.keys(seriesData).map(rank => {
                return {
                    name: rank,
                    data: seriesData[rank]
                };
            });

            var options = {
                chart: {
                    type: 'bar',
                    height: 450,
                    stacked: true
                },
                series: series,
                xaxis: {
                    categories: years
                },
                plotOptions: {
                    bar: {
                        horizontal: true,
                        columnWidth: '55%',
                        columnHeigth: '55%',
                        endingShape: 'rounded'
                    },
                },
                title: {
                    text: 'Số học lực của học sinh theo từng năm'
                }
            };

            var chart = new ApexCharts(document.querySelector("#chart_all_rank_student_year"), options);
            chart.render();
        },
        error: function (error) {
            console.log('Error fetching data:', error);
        }
    });
}
function count_student_rank_course() {
    $.ajax({
        url: 'https://localhost:44335/user/Count_All_Rank_Student_Course',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var course = data.map(item => item.Course);
            var seriesData = {};

            data.forEach(item => {
                item.Ranks.forEach(rank => {
                    if (!seriesData[rank.Rank]) {
                        seriesData[rank.Rank] = [];
                    }
                    seriesData[rank.Rank].push(rank.Count);
                });
            });

            var series = Object.keys(seriesData).map(rank => {
                return {
                    name: rank,
                    data: seriesData[rank]
                };
            });

            var options = {
                chart: {
                    type: 'bar',
                    height: 450,
                    stacked: true
                },
                series: series,
                xaxis: {
                    categories: course
                },
                plotOptions: {
                    bar: {
                        horizontal: true,
                        columnWidth: '55%',
                        columnHeigth: '55%',
                        endingShape: 'rounded'
                    },
                },
                title: {
                    text: 'Số học lực của học sinh theo từng khoá'
                }
            };

            var chart = new ApexCharts(document.querySelector("#chart_all_rank_student_course"), options);
            chart.render();
        },
        error: function (error) {
            console.log('Error fetching data:', error);
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
                title: {
                    text: 'Số lượng giáo viên và học sinh'
                },
                chart: {
                    type: 'bar',
                    height: 350,
                    toolbar: {
                        show: true,
                        tools: {
                            download: true,
                            selection: true,
                            zoom: true,
                            zoomin: true,
                            zoomout: true,
                            pan: true
                        },
                        export: {
                            csv: {
                                filename: 'Teacher_Student.csv',
                                columnDelimiter: ',',
                                dateFormatter: function (timestamp) {
                                    return new Date(timestamp).toDateString();
                                }
                            },
                            svg: {
                                filename: 'Teacher_Student.svg'
                            },
                            png: {
                                filename: 'Teacher_Student.png'
                            }
                        },
                        autoSelected: 'zoom'
                    }
                },
                plotOptions: {
                    bar: {
                        horizontal: true,
                        columnWidth: '55%',
                        columnHeigth: '55%',
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
function count_all_rank_student() {
    $.ajax({
        url: 'https://localhost:44335/user/Count_Student_Rank',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            // Chuyển đổi dữ liệu từ API thành định dạng mà ApexCharts yêu cầu
            var categories = data.map(function (item) {
                return item.Rank;
            });
            var seriesData = data.map(function (item) {
                return item.Student;
            });

            // Cấu hình biểu đồ
            var options = {
                series: [{
                    name: 'Học sinh',
                    data: seriesData
                }],
                title: {
                    text: 'Số lượng danh hiệu của học sinh toàn trường'
                },
                chart: {
                    type: 'bar',
                    height: 350,
                    toolbar: {
                        show: true,
                        tools: {
                            download: true,
                            selection: true,
                            zoom: true,
                            zoomin: true,
                            zoomout: true,
                            pan: true
                        },
                        export: {
                            csv: {
                                filename: 'Rank_Student.csv',
                                columnDelimiter: ',',
                                dateFormatter: function (timestamp) {
                                    return new Date(timestamp).toDateString();
                                }
                            },
                            svg: {
                                filename: 'Rank_Student.svg'
                            },
                            png: {
                                filename: 'Rank_Student.png'
                            }
                        },
                        autoSelected: 'zoom'
                    }
                },
                plotOptions: {
                    bar: {
                        horizontal: false,
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
                    categories: categories
                },
                fill: {
                    opacity: 1
                },
                tooltip: {
                    y: {
                        formatter: function (val) {
                            return val + " em";
                        }
                    }
                }
            };

            // Render biểu đồ với cấu hình và dữ liệu đã chuyển đổi từ API
            var chart = new ApexCharts(document.querySelector("#chart_all_rank_student"), options);
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
                title: {
                    text: 'Số lượng học sinh theo kì'
                },
                chart: {
                    type: 'pie',
                    height: 350,
                    toolbar: {
                        show: true,
                        tools: {
                            download: true,
                            selection: true,
                            zoom: true,
                            zoomin: true,
                            zoomout: true,
                            pan: true
                        },
                        export: {
                            csv: {
                                filename: 'Count_Student.csv',
                                columnDelimiter: ',',
                                headerCategory: 'Khoá',
                                headerValue: 'Số lượng học sinh',
                                dateFormatter: function (timestamp) {
                                    return new Date(timestamp).toDateString();
                                }
                            },
                            svg: {
                                filename: 'Count_Student.svg'
                            },
                            png: {
                                filename: 'Count_Student.png'
                            }
                        },
                        autoSelected: 'zoom'
                    }
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
function count_student_year() {
    $.ajax({
        url: 'https://localhost:44335/user/Count_Student_In_Year',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            // Chuyển đổi dữ liệu từ API thành mảng ApexCharts yêu cầu
            var chartData = data.map(function (item) {
                return {
                    x: item.Years,
                    y: item.Student
                };
            });
            console.log(chartData);
            // Cấu hình biểu đồ
            var options = {
                series: chartData.map(function (item) {
                    return item.y;
                }),
                title: {
                    text: 'Số lượng học sinh theo năm'
                },
                chart: {
                    type: 'pie',
                    height: 350,
                    toolbar: {
                        show: true,
                        tools: {
                            download: true,
                            selection: true,
                            zoom: true,
                            zoomin: true,
                            zoomout: true,
                            pan: true
                        },
                        export: {
                            csv: {
                                filename: 'Count_Student.csv',
                                columnDelimiter: ',',
                                headerCategory: 'Năm',
                                headerValue: 'Số lượng học sinh',
                                dateFormatter: function (timestamp) {
                                    return new Date(timestamp).toDateString();
                                }
                            },
                            svg: {
                                filename: 'Count_Student.svg'
                            },
                            png: {
                                filename: 'Count_Student.png'
                            }
                        },
                        autoSelected: 'zoom'
                    }
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
            var chart = new ApexCharts(document.querySelector("#chart_student_year"), options);
            chart.render();
        },
        error: function (xhr, status, error) {
            console.error('Error calling API:', error);
        }
    });
}
function count_student_class() {
    $.ajax({
        url: 'https://localhost:44335/user/Count_Students_By_Class',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            // Chuyển đổi dữ liệu từ API thành mảng ApexCharts yêu cầu
            var chartData = data.map(function (item) {
                return {
                    x: item.Classes,
                    y: item.Student
                };
            });
            console.log(chartData);
            // Cấu hình biểu đồ
            var options = {
                series: chartData.map(function (item) {
                    return item.y;
                }),
                title: {
                    text: 'Số lượng học sinh theo lớp'
                },
                chart: {
                    type: 'pie',
                    height: 350,
                    toolbar: {
                        show: true,
                        tools: {
                            download: true,
                            selection: true,
                            zoom: true,
                            zoomin: true,
                            zoomout: true,
                            pan: true
                        },
                        export: {
                            csv: {
                                filename: 'Count_Student.csv',
                                columnDelimiter: ',',
                                headerCategory: 'Lớp',
                                headerValue: 'Số lượng học sinh',
                                dateFormatter: function (timestamp) {
                                    return new Date(timestamp).toDateString();
                                }
                            },
                            svg: {
                                filename: 'Count_Student.svg'
                            },
                            png: {
                                filename: 'Count_Student.png'
                            }
                        },
                        autoSelected: 'zoom'
                    }
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
            var chart = new ApexCharts(document.querySelector("#chart_student_classes"), options);
            chart.render();
        },
        error: function (xhr, status, error) {
            console.error('Error calling API:', error);
        }
    });
}