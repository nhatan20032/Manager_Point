function studentGrid() {
    this.$table = $('#student_table').DataTable({
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
            "url": "https://localhost:44335/user/get_all_student",
            "data": function (d) {
                delete d.columns;
                d.search = d.search.value;
                d.class = 0;
            },
            "dataSrc": "data"
        },
        "rowId": "Id",
        "columns": [
            { 'data': 'Id', "orderable": false },
            { 'data': 'Role_User', "orderable": false },
            { 'data': 'User_Class', "orderable": false },
            { 'data': 'User_Code', "orderable": false },
            { 'data': 'Password', "orderable": false },
            { 'data': 'PhoneNumber', "orderable": false },
            { 'data': 'Name', "orderable": false },
            { 'data': 'Description', "orderable": false },
            { 'data': 'DOB', "orderable": false },
            { 'data': 'Gender', "orderable": false },
            { 'data': 'Email', "orderable": false },
            { 'data': 'AvatarUrl', "orderable": false },
            { 'data': 'Nation', "orderable": false },
            { 'data': 'Email', "orderable": false },
            {
                "data": "Status", "orderable": false, render: function (data) {
                    if (data == 1) return "Chờ duyệt";
                    if (data == 2) return "Đã duyệt";
                    if (data == 3) return "Từ chối duyệt";
                    return "Unknown";
                }
            },
            {
                "data": null, "orderable": false, render: function (row) {
                    let edit = `<a class="link-warning" style="cursor: pointer; margin-right: 20px; text-decoration: none"><i class="fa-solid fa-wrench"></i>Edit</a>`;
                    let remove = `<a class="link-danger" style="cursor: pointer; text-decoration: none;"><i class="fa-solid fa-trash"></i>Remove</a>`;
                    return `<div>${edit} ${remove}</div>`;
                }
            },
        ],
        "searching": true,
        "paging": true,
        "lengthChange": true,
        "info": true,
        "pageLength": 10,
    });
}
function teacherGrid() {
    this.$table = $('#teacher_table').DataTable({
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
            "url": "https://localhost:44335/user/get_all_teacher",
            "data": function (d) {
                delete d.columns;
                d.search = d.search.value;
                d.class = 0;
                d.subject = 0;
            },
            "dataSrc": "data"
        },
        "rowId": "Id",
        "columns": [
            { 'data': 'Id', "orderable": false },
            { 'data': 'Role_User', "orderable": false },
            { 'data': 'User_Class', "orderable": false },
            { 'data': 'Subject_User', "orderable": false },
            { 'data': 'User_Code', "orderable": false },
            { 'data': 'Password', "orderable": false },
            { 'data': 'PhoneNumber', "orderable": false },
            { 'data': 'Name', "orderable": false },
            { 'data': 'Description', "orderable": false },
            { 'data': 'DOB', "orderable": false },
            { 'data': 'Gender', "orderable": false },
            { 'data': 'Email', "orderable": false },
            { 'data': 'AvatarUrl', "orderable": false },
            { 'data': 'Nation', "orderable": false },
            { 'data': 'Email', "orderable": false },
            {
                "data": "Status", "orderable": false, render: function (data) {
                    if (data == 1) return "Chờ duyệt";
                    if (data == 2) return "Đã duyệt";
                    if (data == 3) return "Từ chối duyệt";
                    return "Unknown";
                }
            },
            {
                "data": null, "orderable": false, render: function (row) {
                    let edit = `<a class="link-warning" style="cursor: pointer; margin-right: 20px; text-decoration: none"><i class="fa-solid fa-wrench"></i>Edit</a>`;
                    let remove = `<a class="link-danger" style="cursor: pointer; text-decoration: none;"><i class="fa-solid fa-trash"></i>Remove</a>`;
                    return `<div>${edit} ${remove}</div>`;
                }
            },
        ],
        "searching": true,
        "paging": true,
        "lengthChange": true,
        "info": true,
        "pageLength": 10,
    });
}
