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


function GetDataGradePointBySubject(idClass) {
    debugger
    $.ajax({
        url: `https://localhost:44335/gradepoint/subject?idClass=${idClass}&semester=1`,
        method: 'GET',
        success: function (res) {
            var data = JSON.parse(res);
            
            var columns = [
                { data: 'Id', title: 'ID', visible: false },
                { data: 'userName', title: 'Tên học sinh' },
                { data: 'subjectName', title: 'Môn học' },
                { data: 'ExaminationPoint', title: 'Điểm thành phần' },
                { data: 'Midterm_Grades', title: 'Điểm giữa kỳ' },
                { data: 'Final_Grades', title: 'Điểm cuối kỳ' },
                { data: 'Average', title: 'Điểm trung bình' }
            ];

            $('#Subject-1').DataTable({
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
                "pageLength": 100,
            });
        }
    });
}
function GetDataGradePointBySubject2(idClass) {
    debugger
    $.ajax({
        url: `https://localhost:44335/gradepoint/subject?idClass=${idClass}&semester=2`,
        method: 'GET',
        success: function (res) {
            var data = JSON.parse(res);

            var columns = [
                { data: 'Id', title: 'ID', visible: false },
                { data: 'userName', title: 'Tên học sinh' },
                { data: 'subjectName', title: 'Môn học' },
                { data: 'ExaminationPoint', title: 'Điểm thành phần' },
                { data: 'Midterm_Grades', title: 'Điểm giữa kỳ' },
                { data: 'Final_Grades', title: 'Điểm cuối kỳ' },
                { data: 'Average', title: 'Điểm trung bình' }
            ];

            $('#Subject-2').DataTable({
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
                "pageLength": 100,
            });
        }
    });
}
function GetDataGradePointBySubject3(idClass) {
    debugger
    $.ajax({
        url: `https://localhost:44335/gradepoint/subject?idClass=${idClass}&semester=3`,
        method: 'GET',
        success: function (res) {
            var data = JSON.parse(res);

            var columns = [
              
                { data: 'UserName', title: 'Tên học sinh' },
                { data: 'SubjectName', title: 'Môn học' },
                { data: 'Semester1', title: 'kỳ 1' },
                { data: 'Semester2', title: 'kỳ 2' },
                { data: 'Average_Whole_year', title: 'Điểm trung bình' }
            ];

            $('#Subject-3').DataTable({
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
                "pageLength": 100,
            });
        }
    });
}
}
function GetClassInfo(classInfo) {
    let html = `<div class="card-body">
                    <h3 class="card-title">Lớp: ${classInfo.Name}</h3>
                    <h6 class="card-subtitle mb-2 text-muted">Mã lớp: ${classInfo.ClassCode}</h6>
                    <p class="card-text">Khối: ${classInfo.GradeLevel}</p>
                    <p class="card-text">Tên khóa học: ${classInfo.CourseName}</p>
                </div>`
    document.getElementById("classInfo").innerHTML = html 

}