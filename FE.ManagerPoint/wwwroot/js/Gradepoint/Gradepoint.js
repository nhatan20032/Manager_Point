function setupGrid() {
    this.$table = $('#gradepoint_table').DataTable({
        "language": {
            "sProcessing": "Đang xử lý...",
            "sLengthMenu": "Xem _MENU_ mục",
            "sZeroRecords": "Không tìm thấy dòng nào phù hợp",
            "sInfo": "Đang xem _START_ đến _END_ trong tổng số _TOTAL_ mục",
            "sInfoEmpty": "Đang xem 0 đến 0 trong tổng số 0 mục",
            "sInfoFiltered": "(được lọc từ _MAX_ mục)",
            "sInfoPostFix": "",
            "sUrl": "",
            "oPaginate": {
                "sFirst": "Đầu",
                "sPrevious": "Trước",
                "sNext": "Tiếp",
                "sLast": "Cuối"
            }
        },
        "pageLength": 10,
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "https://localhost:44335/gradepoint/get_all",
            "data": function (d) {
                
                delete d.columns;
                d.search = d.search.value;
                d.semester = $('#semester').val();
            },
            "dataSrc": "data",
           
        },
        
       

        "rowId": "Id",
        "columns": [
            { "data": "subjectName", "orderable": false },
            { "data": "ExaminationPoint", "orderable": false },
            { "data": "Midterm_Grades", "orderable": false },
            { "data": "Final_Grades", "orderable": false },
            { "data": "Average", "orderable": false },
            
        ],

        "searching": true,
        "paging": true,
        "lengthChange": true,
        "info": true,
        "pageLength": 10,
    });
    window.dt = this.$table;

}


function GetAllYear() {
    this.$table = $('#gradepoint_year_table').DataTable({
        "language": {
            "sProcessing": "Đang xử lý...",
            "sLengthMenu": "Xem _MENU_ mục",
            "sZeroRecords": "Không tìm thấy dòng nào phù hợp",
            "sInfo": "Đang xem _START_ đến _END_ trong tổng số _TOTAL_ mục",
            "sInfoEmpty": "Đang xem 0 đến 0 trong tổng số 0 mục",
            "sInfoFiltered": "(được lọc từ _MAX_ mục)",
            "sInfoPostFix": "",
            "sUrl": "",
            "oPaginate": {
                "sFirst": "Đầu",
                "sPrevious": "Trước",
                "sNext": "Tiếp",
                "sLast": "Cuối"
            }
        },
        "pageLength": 10,
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "https://localhost:44335/gradepoint/get_all_year",
            "data": function (d) {
                delete d.columns;
                d.search = d.search.value;
            },
            "dataSrc": "data",

        },



        "rowId": "Id",
        "columns": [
            { "data": "SubjectName", "orderable": false },
            { "data": "Semester1", "orderable": false },
            { "data": "Semester2", "orderable": false },
            { "data": "Average_Whole_year", "orderable": false },

        ],

        "searching": true,
        "paging": true,
        "lengthChange": true,
        "info": true,
        "pageLength": 10,
    });
    window.dt = this.$table;

}


