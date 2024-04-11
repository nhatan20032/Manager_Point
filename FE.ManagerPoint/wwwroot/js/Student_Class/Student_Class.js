var selectedIdsClass = [];
var selectedIdsUser = [];
function studentGrid() {
    this.$table = $('#student_class_table').DataTable({
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
                d.check_class = 1;
            },
            "dataSrc": "data"
        },
        "rowId": "Id",
        "columns": [
            { 'data': 'Id', "orderable": false },
            { 'data': 'Name', "orderable": false },
            { 'data': 'User_Code', "orderable": false },
            { 'data': 'Student_Class_Code', "orderable": false },
            { 'data': 'Student_Class_Name', "orderable": false },
            {
                "data": "AvatarUrl", "className": "img_td", "orderable": false, render: function (data) {
                    let html = `<img src="${data}" alt="AvatarUrl" />`;
                    return html;
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
                    let edit = `<a class="link-warning" onclick="GetById(${row.Id})" style="cursor: pointer; margin-right: 20px; text-decoration: none"><i class="fa-solid fa-wrench"></i>Cập nhật</a>`;
                    let remove = `<a class="link-danger" onclick="deleteSubject(${row.Id})" style="cursor: pointer; text-decoration: none;"><i class="fa-solid fa-trash"></i>Xoá</a>`;
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
function student_No_Class() {
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
                d.check_class = 2;
            },
            "dataSrc": "data"
        },
        "initComplete": function () {
            studentGrid();
            $('#student_table').on('change', '.select-checkbox', function () {
                var id = $(this).data('id');
                if ($(this).prop('checked')) {
                    if (!selectedIdsUser.includes(id)) {
                        selectedIdsUser.push(id);
                    }
                } else {
                    var index = selectedIdsUser.indexOf(id);
                    if (index !== -1) {
                        selectedIdsUser.splice(index, 1);
                    }
                }
                console.log(selectedIdsUser);
            });
            $(document).on('change', '#checkAllCheckbox', function () {
                var checkboxes = $('.select-checkbox');
                checkboxes.prop('checked', $(this).prop('checked'));
                if ($(this).prop('checked')) {
                    checkboxes.each(function () {
                        var id = $(this).data('id');
                        if (!selectedIdsUser.includes(id)) {
                            selectedIdsUser.push(id); // Thêm ID vào mảng nếu chưa tồn tại
                        }
                    });
                } else {
                    selectedIdsUser = []; // Thiết lập lại mảng selectedIds khi checkbox "Check All" bị bỏ chọn
                }
                console.log(selectedIdsUser);
            });
            this.api().on('draw', function () {
                selectedIdsUser = [];
                $('#checkAllCheckbox').prop('checked', false);
                $('.select-checkbox').prop('checked', false);
            });
        },
        "rowId": "Id",
        "columns": [
            {
                "data": null,
                "className": "checkbox-column",
                "orderable": false,
                "title": '<input type="checkbox" id="checkAllCheckbox">',
                "render": function (data) {
                    return `<input type="checkbox" class="select-checkbox" data-id="${data.Id}">`;
                }
            },
            { 'data': 'Id', "orderable": false },
            { 'data': 'Name', "orderable": false },
            { 'data': 'User_Code', "orderable": false },
            {
                "data": "AvatarUrl", "className": "img_td", "orderable": false, render: function (data) {
                    let html = `<img src="${data}" alt="AvatarUrl" />`;
                    return html;
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
        ],
        "searching": true,
        "paging": true,
        "lengthChange": true,
        "info": true,
        "pageLength": 10,
    });
}
function classGrid() {
    this.$table = $('#class_table').DataTable({
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
            "url": "https://localhost:44335/class/get_all",
            "data": function (d) {
                delete d.columns;
                d.search = d.search.value;
            },
            "dataSrc": "data"
        },
        "initComplete": function () {
            $('#class_table').on('change', '.select-checkbox-class', function () {
                var id = $(this).data('id');
                if ($(this).prop('checked')) {
                    if (!selectedIdsClass.includes(id)) {
                        selectedIdsClass.push(id);
                    }
                } else {
                    var index = selectedIdsClass.indexOf(id);
                    if (index !== -1) {
                        selectedIdsClass.splice(index, 1);
                    }
                }
                console.log(selectedIdsClass);
            });
            $(document).on('change', '#checkAllCheckboxClass', function () {
                var checkboxes = $('.select-checkbox-class');
                checkboxes.prop('checked', $(this).prop('checked'));
                if ($(this).prop('checked')) {
                    checkboxes.each(function () {
                        var id = $(this).data('id');
                        if (!selectedIdsClass.includes(id)) {
                            selectedIdsClass.push(id);
                        }
                    });
                } else {
                    selectedIdsClass = [];
                }
                console.log(selectedIdsClass);
            });
            this.api().on('draw', function () {
                selectedIdsClass = [];
                $('#checkAllCheckboxClass').prop('checked', false);
                $('.select-checkbox-class').prop('checked', false);
            });
        },
        "rowId": "Id",
        "columns": [
            {
                "data": null,
                "className": "checkbox-column",
                "orderable": false,
                "title": '<input type="checkbox" id="checkAllCheckboxClass">',
                "render": function (data) {
                    return `<input type="checkbox" class="select-checkbox-class" data-id="${data.Id}">`;
                }
            },
            { 'data': 'Id', "orderable": false },
            { 'data': 'Name', "orderable": false },
            { 'data': 'ClassCode', "orderable": false },
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
        ],
        "searching": true,
        "paging": true,
        "lengthChange": true,
        "info": true,
        "pageLength": 10,
    });
}

function createClass_Student(callback) {
    var mergedData = [];

    for (var i = 0; i < selectedIdsUser.length; i++) {
        var userId = selectedIdsUser[i];
        for (var j = 0; j < selectedIdsClass.length; j++) {
            var classId = selectedIdsClass[j];
            var userData = {
                userId: userId,
                classId: classId
            };
            mergedData.push(userData);
        }
    }

    console.log(mergedData);
    $.ajax({
        url: "https://localhost:44335/student_class/batch_create",
        method: "POST",
        data: JSON.stringify(mergedData),
        contentType: 'application/json',
        success: function (res) {
            if (res && res.length < 0) {
                toastr.error('Lớp đã đạt tối đa học sinh, vui lòng kiểm tra lại!');
            }
            if (callback && typeof callback === "function") {
                callback();
            }
            $('#class_table').DataTable().ajax.reload();
            $('#student_table').DataTable().ajax.reload();
            $('#student_class_table').DataTable().ajax.reload();
        },
    });
}