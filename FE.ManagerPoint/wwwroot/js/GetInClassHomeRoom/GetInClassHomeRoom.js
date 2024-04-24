function GetDataGradePointS1(idClass) {
    $.ajax({
        url: `https://localhost:44335/gradepoint/GradePointByClass?idClass=${idClass}&semester=1`,
        method: 'GET',
        success: function (res) {
            var data = JSON.parse(res);
            var columns = [
                { title: '', data: 'UserName' },
                { title: 'Lớp', data: 'ClassName' },
                ...data.data[0].SubjectClasses.map(function (subject) {
                    return {
                        title: subject.SubjectName,
                        data: function (row) {
                            return row.SubjectClasses.find(s => s.SubjectName === subject.SubjectName).Avegare;
                        }
                    };
                }),
                { title: 'Điểm trung bình', data: 'TotalPoint' },
                { title: 'Danh hiệu', data: 'Rank' }
            ];

            $('#GradePointStudent-1').DataTable({
                layout: {
                    topStart: {
                        buttons: ['copy', 'csv', 'excel', 'pdf', 'print']
                    }
                },
                "processing": true,
                data: data.data,
                columns: columns,
                "searching": true,
                "paging": true,
                "lengthChange": true,
                "info": true,
                "pageLength": 20,
            });
        }
    });
}
function GetDataGradePointS2(idClass) {
    $.ajax({
        url: `https://localhost:44335/gradepoint/GradePointByClass?idClass=${idClass}&semester=2`,
        method: 'GET',
        success: function (res) {
            var data = JSON.parse(res);
            var columns = [
                { title: '', data: 'UserName' },
                { title: 'Lớp', data: 'ClassName' },
                ...data.data[0].SubjectClasses.map(function (subject) {
                    return {
                        title: subject.SubjectName,
                        data: function (row) {
                            return row.SubjectClasses.find(s => s.SubjectName === subject.SubjectName).Avegare;
                        }
                    };
                }),
                { title: 'Điểm trung bình', data: 'TotalPoint' },
                { title: 'Danh hiệu', data: 'Rank' }
            ];

            $('#GradePointStudent-2').DataTable({
                layout: {
                    topStart: {
                        buttons: ['copy', 'csv', 'excel', 'pdf', 'print']
                    }
                },
                "processing": true,
                data: data.data,
                columns: columns,
                "searching": true,
                "paging": true,
                "lengthChange": true,
                "info": true,
                "pageLength": 20,
            });
        }
    });
}
function GetDataGradePointS3(idClass) {
    $.ajax({
        url: `https://localhost:44335/gradepoint/GradePointByClass?idClass=${idClass}`,
        method: 'GET',
        success: function (res) {
            var data = JSON.parse(res);
            var columns = [
                { title: '', data: 'UserName' },
                { title: 'Lớp', data: 'ClassName' },
                ...data.data[0].SubjectClasses.map(function (subject) {
                    return {
                        title: subject.SubjectName,
                        data: function (row) {
                            return row.SubjectClasses.find(s => s.SubjectName === subject.SubjectName).Avegare;
                        }
                    };
                }),
                { title: 'Điểm trung bình', data: 'TotalPoint' },
                { title: 'Danh hiệu', data: 'Rank' }
            ];

            $('#GradePointStudent-3').DataTable({
                layout: {
                    topStart: {
                        buttons: ['copy', 'csv', 'excel', 'pdf', 'print']
                    }
                },
                "processing": true,
                data: data.data,
                columns: columns,
                "searching": true,
                "paging": true,
                "lengthChange": true,
                "info": true,
                "pageLength": 20,
            });
        }
    });
}