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
                d.classes = $("#search_class").val();
            },
            "dataSrc": "data"
        },
        "rowId": "Id",
        "columns": [
            { 'data': 'Id', "orderable": false },
            { 'data': 'Name', "orderable": false },
            { 'data': 'User_Code', "orderable": false },
            { 'data': 'Student_Class', "orderable": false },
            { 'data': 'Password', "orderable": false },
            { 'data': 'PhoneNumber', "orderable": false },
            { 'data': 'DOB', "orderable": false },
            { 'data': 'Gender', "orderable": false },
            { 'data': 'Email', "orderable": false },
            { 'data': 'AvatarUrl', "orderable": false },
            { 'data': 'Address', "orderable": false },
            { 'data': 'Nation', "orderable": false },
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
                d.classes = $("#search_class").val();
                d.subject = $("#search_subject").val();
            },
            "dataSrc": "data"
        },
        "rowId": "Id",
        "columns": [
            { 'data': 'Id', "orderable": false },
            { 'data': 'Name', "orderable": false },
            { 'data': 'User_Code', "orderable": false },
            { 'data': 'Teacher_Class', "orderable": false },
            { 'data': 'Subject_User', "orderable": false },
            { 'data': 'Password', "orderable": false },
            { 'data': 'PhoneNumber', "orderable": false },
            { 'data': 'DOB', "orderable": false },
            { 'data': 'Gender', "orderable": false },
            { 'data': 'Email', "orderable": false },
            { 'data': 'AvatarUrl', "orderable": false },
            { 'data': 'Address', "orderable": false },
            { 'data': 'Nation', "orderable": false },
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

function userNoRoleGrid() {
    this.$table = $('#user_table').DataTable({
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
            "url": "https://localhost:44335/user/get_all_user_no_role",
            "data": function (d) {
                delete d.columns;
                d.search = d.search.value;
            },
            "dataSrc": "data"
        },
        "rowId": "Id",
        "columns": [
            { 'data': 'Id', "orderable": false },
            { 'data': 'Name', "orderable": false },
            { 'data': 'User_Code', "orderable": false },
            { 'data': 'Password', "orderable": false },
            { 'data': 'PhoneNumber', "orderable": false },
            { 'data': 'DOB', "orderable": false },
            { 'data': 'Gender', "orderable": false },
            { 'data': 'Email', "orderable": false },
            { 'data': 'AvatarUrl', "orderable": false },
            { 'data': 'Address', "orderable": false },
            { 'data': 'Nation', "orderable": false },
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
                    let edit = `<a class="link-warning" onclick="GetById(${row.Id})" style="cursor: pointer; margin-right: 20px; text-decoration: none"><i class="fa-solid fa-wrench"></i>Edit</a>`;
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
function getSubject() {
    $.ajax({
        url: "https://localhost:44335/subject/get_list",
        method: "GET",
        success: function (res) {
            if (res && res.length > 0) {
                var select = $("#search_subject");
                $.each(res, function (index, item) {
                    select.append($("<option></option>")
                        .attr("value", item.id)
                        .text(item.name));
                });
            }
        },
    });
}
function getClass() {
    $.ajax({
        url: "https://localhost:44335/class/get_list",
        method: "GET",
        success: function (res) {
            if (res && res.length > 0) {
                var select = $("#search_class");
                $.each(res, function (index, item) {
                    select.append($("<option></option>")
                        .attr("value", item.id)
                        .text(item.classCode));
                });
            }
        },
    });
}

function createUser(object, callback) {
    $.ajax({
        url: "https://localhost:44335/user/create",
        method: "POST",
        data: JSON.stringify(object),
        contentType: 'application/json',
        success: function (res) {
            if (callback && typeof callback === "function") {
                callback();
            }
            $('#user_table').DataTable().ajax.reload();
        },
    })
}

function updateUser(id, object, callback) {
    $.ajax({
        url: `https://localhost:44335/user/modified?id=${id}`,
        method: "PUT",
        data: JSON.stringify(object),
        contentType: 'application/json',
        success: function (res) {
            if (callback && typeof callback === "function") {
                callback();
            }
            $('#user_table').DataTable().ajax.reload();
        },
    });
}

function GetById(id) {
    $.ajax({
        url: `https://localhost:44335/user/get_by_id`,
        method: "GET",
        data: {
            id: id,
        },
        success: function (res) {
            if (res == null) {
                toastr.error('Không tìm thấy người dùng');
                return;
            }
            console.log(res);
            $("#id").val(res.id);
            $("#user_Code_md").val(res.user_Code);
            $("#name_md").val(res.name);
            $("#gender_md").val(res.gender);
            $("#nation_md").val(res.nation);
            $("#password_md").val(res.password);
            $("#address_md").val(res.address);
            $("#email_md").val(res.email);
            $("#DOB_md").val(res.dob);
            $("#phone_md").val(res.phoneNumber);
            $("#description_md").val(res.description);
            $("#updateModal").modal("show");
        },
    });
}