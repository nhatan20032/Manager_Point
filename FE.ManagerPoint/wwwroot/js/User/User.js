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
                d.classes = $("#search_class_student").val();
            },
            "dataSrc": "data"
        },
        "initComplete": function () {
            teacherGrid();
        },
        "rowId": "Id",
        "columns": [
            { 'data': 'Id', "orderable": false },
            { 'data': 'Name', "orderable": false },
            { 'data': 'User_Code', "orderable": false },
            { 'data': 'Student_Class_Name', "orderable": false },
            { 'data': 'Password', "orderable": false },
            { 'data': 'PhoneNumber', "orderable": false },
            { 'data': 'DOB', "orderable": false },
            {
                "data": "Gender", "orderable": false, render: function (data) {
                    if (data == 1) return "Nam";
                    if (data == 2) return "Nữ";
                    if (data == 3) return "Khác";
                    return "Unknown";
                }
            },
            { 'data': 'Email', "orderable": false },
            {
                "data": "AvatarUrl", "className": "img_td", "orderable": false, render: function (data) {
                    let html = `<img src="${data}" alt="AvatarUrl" />`;
                    return html;
                }
            },
            { 'data': 'Address', "orderable": false },
            {
                "data": "Nation", "orderable": false, render: function (data) {
                    if (data === "vi") return "Việt Nam";
                    if (data === "kr") return "Hàn Quốc";
                    if (data === "zh") return "Trung Quốc";
                    if (data === "us") return "Mỹ";
                    if (data === "uk") return "Anh";
                    return "Unknown";
                }
            },
            {
                "data": "Status", "orderable": false, render: function (data) {
                    if (data == 1) return "Hoạt động";
                    if (data == 2) return "Thất bại";
                    if (data == 3) return "Ra trường";
                    if (data == 3) return "Kết thúc";
                    if (data == 3) return "Đang xử lý";
                    return "Unknown";
                }
            },
            {
                "data": null, "orderable": false, render: function (row) {
                    let edit = `<a class="link-warning" onclick="GetById(${row.Id})" style="cursor: pointer; margin-right: 20px; text-decoration: none"><i class="fa-solid fa-wrench"></i>Edit</a>`;
                    let remove = `<a class="link-danger" onclick="Remove(${row.Id})" style="cursor: pointer; text-decoration: none;"><i class="fa-solid fa-trash"></i>Remove</a>`;
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
            { 'data': 'Teacher_Class_Name', "orderable": false },
            { 'data': 'Subject_User', "orderable": false },
            { 'data': 'Password', "orderable": false },
            { 'data': 'PhoneNumber', "orderable": false },
            { 'data': 'DOB', "orderable": false },
            {
                "data": "Gender", "orderable": false, render: function (data) {
                    if (data == 1) return "Nam";
                    if (data == 2) return "Nữ";
                    if (data == 3) return "Khác";
                    return "Unknown";
                }
            },
            { 'data': 'Email', "orderable": false },
            {
                "data": "AvatarUrl", "className": "img_td", "orderable": false, render: function (data) {
                    let html = `<img src="${data}" alt="AvatarUrl" />`;
                    return html;
                }
            },
            { 'data': 'Address', "orderable": false },
            {
                "data": "Nation", "orderable": false, render: function (data) {
                    if (data === "vi") return "Việt Nam";
                    if (data === "kr") return "Hàn Quốc";
                    if (data === "zh") return "Trung Quốc";
                    if (data === "us") return "Mỹ";
                    if (data === "uk") return "Anh";
                    return "Unknown";
                }
            },
            {
                "data": "Status", "orderable": false, render: function (data) {
                    if (data == 1) return "Hoạt động";
                    if (data == 2) return "Thất bại";
                    if (data == 3) return "Ra trường";
                    if (data == 3) return "Kết thúc";
                    if (data == 3) return "Đang xử lý";
                    return "Unknown";
                }
            },
            {
                "data": null, "orderable": false, render: function (row) {
                    let edit = `<a class="link-warning" onclick="GetById(${row.Id})" style="cursor: pointer; margin-right: 20px; text-decoration: none"><i class="fa-solid fa-wrench"></i>Edit</a>`;
                    let remove = `<a class="link-danger" onclick="Remove(${row.Id})" style="cursor: pointer; text-decoration: none;"><i class="fa-solid fa-trash"></i>Remove</a>`;
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
        "initComplete": function () {
            studentGrid();
        },
        "rowId": "Id",
        "columns": [
            { 'data': 'Id', "orderable": false },
            { 'data': 'Name', "orderable": false },
            { 'data': 'User_Code', "orderable": false },
            { 'data': 'Password', "orderable": false },
            { 'data': 'PhoneNumber', "orderable": false },
            { 'data': 'DOB', "orderable": false },
            {
                "data": "Gender", "orderable": false, render: function (data) {
                    if (data == 1) return "Nam";
                    if (data == 2) return "Nữ";
                    if (data == 3) return "Khác";
                    return "Unknown";
                }
            },
            { 'data': 'Email', "orderable": false },
            {
                "data": "AvatarUrl", "className": "img_td", "orderable": false, render: function (data) {
                    let html = `<img src="${data}" alt="AvatarUrl" />`;
                    return html;
                }
            },
            { 'data': 'Address', "orderable": false },
            {
                "data": "Nation", "orderable": false, render: function (data) {
                    if (data === "vi") return "Việt Nam";
                    if (data === "kr") return "Hàn Quốc";
                    if (data === "zh") return "Trung Quốc";
                    if (data === "us") return "Mỹ";
                    if (data === "uk") return "Anh";
                    return "Unknown";
                }
            },
            {
                "data": "Status", "orderable": false, render: function (data) {
                    if (data == 1) return "Hoạt động";
                    if (data == 2) return "Thất bại";
                    if (data == 3) return "Ra trường";
                    if (data == 3) return "Kết thúc";
                    if (data == 3) return "Đang xử lý";
                    return "Unknown";
                }
            },
            {
                "data": null,"orderable": false, render: function (row) {
                    let edit = `<a class="link-warning" onclick="GetById(${row.Id})" style="cursor: pointer; margin-right: 20px; text-decoration: none"><i class="fa-solid fa-wrench"></i>Edit</a>`;
                    let remove = `<a class="link-danger" onclick="Remove(${row.Id})" style="cursor: pointer; text-decoration: none;"><i class="fa-solid fa-trash"></i>Remove</a>`;
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
function getStudentClass() {
    $.ajax({
        url: "https://localhost:44335/class/get_list",
        method: "GET",
        success: function (res) {
            if (res && res.length > 0) {
                var select = $("#search_class_student");
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
            $("#avatar_md").val(res.avatarUrl);
            $("#updateModal").modal("show");
        },
    });
}

function Remove(id) {
    swal({
        title: "Bạn chắc chắn muốn xóa?",
        text: "Hành động này không thể hoàn tác!",
        icon: "warning",
        buttons: ["Hủy", "Xóa"],
        dangerMode: true,
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: "https://localhost:44335/user/remove?id=" + id,
                method: "DELETE",
                success: function (res) {
                    toastr.success('Xóa vai trò thành công');
                    $('#user_table').DataTable().ajax.reload();
                },
            });
        } else {
            // Người dùng nhấn Hủy
            swal("Hủy xóa!", {
                icon: "info",
            });
        }
    });
}
function Import_Excel(callback) {
    var fd = new FormData();
    var files = $('#file_excel')[0].files[0];
    fd.append('file', files);
    $.ajax({
        url: "https://localhost:44335/user/import_excel",
        method: "POST",
        data: fd,
        contentType: false,
        processData: false,
        success: function (result) {
            if (callback && typeof callback === "function") {
                callback();
            }
            $('#user_table').DataTable().ajax.reload();
            console.log(result);
        },
    })
}
function downloadExcelFile() {
    var fileUrl = '/excel/sample.xlsx';
    var link = document.createElement('a');
    link.href = fileUrl;
    link.download = 'sample.xlsx';
    link.click();
}
function handleFileUpload(input) {
    var file = input.files[0];
    var formData = new FormData();
    formData.append('file', file);

    $.ajax({
        url: '/user/upload', // Đường dẫn xử lý tệp tin tải lên (ví dụ: '/upload')
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            console.log(response);
            $("#AvatarUrl").val(response);
        },
        error: function (xhr, status, error) {
            // Xử lý lỗi (nếu có)
            console.log(error);
        }
    });
}
function handleFileUpload_md(input) {
    var file = input.files[0];
    var formData = new FormData();
    formData.append('file', file);

    $.ajax({
        url: '/user/upload',
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            console.log(response);
            $("#avatar_md").val(response);
        },
        error: function (xhr, status, error) {
            // Xử lý lỗi (nếu có)
            console.log(error);
        }
    });
}