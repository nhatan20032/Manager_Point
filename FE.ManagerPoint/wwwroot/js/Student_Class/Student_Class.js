var selectedIdsUser = [];
var selectedRowIdClass = null;
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
                    if (data == 4) return "Kết thúc";
                    if (data == 5) return "Đang hoạt động";
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
                    if (data == 4) return "Kết thúc";
                    if (data == 5) return "Đang hoạt động";
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
                    if (selectedRowIdClass !== null && selectedRowIdClass !== id) {
                        // Uncheck previous row if another row is checked
                        $('#class_table').find(`[data-id="${selectedRowIdClass}"]`).prop('checked', false);
                    }
                    selectedRowIdClass = id;
                } else {
                    selectedRowIdClass = null;
                }
                console.log(selectedRowIdClass);
            });
            this.api().on('draw', function () {
                selectedRowIdClass = null;
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
                    if (data == 4) return "Kết thúc";
                    if (data == 5) return "Đang hoạt động";
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
        var classId = selectedRowIdClass;
        var userData = {
            userId: userId,
            classId: classId
        };
        mergedData.push(userData);
    }

    console.log(mergedData);
    $.ajax({
        url: "https://localhost:44335/student_class/batch_create",
        method: "POST",
        data: JSON.stringify(mergedData),
        contentType: 'application/json',
        success: function (res) {
            if (res === 4000) {
                toastr.error('Chưa tạo môn học, vui lòng tạo môn học!');
            } else if (res && res.length < 0) {
                toastr.error('Lớp đã đạt tối đa học sinh, vui lòng kiểm tra lại!');
            } else {
                if (callback && typeof callback === "function") {
                    callback();
                }
                $('#class_table').DataTable().ajax.reload();
                $('#student_table').DataTable().ajax.reload();
                $('#student_class_table').DataTable().ajax.reload();
            }
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
            $("#id").val(res.id);
            $("#name_md").val(res.name);
            $("#student_selected").val(res.student_Class_id).trigger('change');
            $("#updateModal").modal("show");
        },
    });
}
function getClass() {
    $.ajax({
        url: "https://localhost:44335/class/get_list",
        method: "GET",
        success: function (res) {
            if (res && res.length > 0) {
                console.log(res);
                var select = $("#student_selected");
                $.each(res, function (index, role) {
                    select.append(`<option value="${role.id}">${role.name} - ${role.classCode}</option>`);
                });
            }
        },
    });
}
function editClass(userIds, ids, callback) {
    var intIds = ids.map(function (item) {
        return parseInt(item);
    });
    var mergedData = [];
    for (var j = 0; j < intIds.length; j++) {
        var classId = intIds[j];
        var userData = {
            userId: userIds,
            classId: classId
        };
        mergedData.push(userData);
    }
    $.ajax({
        url: "https://localhost:44335/student_class/batch_remove_by_userid/" + parseInt(userIds),
        method: "DELETE",
        success: function () {
            $.ajax({
                url: "https://localhost:44335/student_class/batch_create",
                method: "POST",
                data: JSON.stringify(mergedData),
                contentType: 'application/json',
                success: function (res) {
                    if (callback && typeof callback === "function") {
                        callback();
                    }
                    mergedData = [];
                    $('#student_table').DataTable().ajax.reload();
                    $('#student_class_table').DataTable().ajax.reload();
                },
                error: function (xhr, status, error) {
                    console.error("Lỗi khi tạo các user_class mới:", error);
                }
            });
        },
        error: function (xhr, status, error) {
            console.error("Lỗi khi xóa các user_class cũ:", error);
        }
    });
}
function deleteSubject(userIds) {
    swal({
        title: "Bạn chắc chắn muốn xóa?",
        text: "Hành động này không thể hoàn tác!",
        icon: "warning",
        buttons: ["Hủy", "Xóa"],
        dangerMode: true,
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: "https://localhost:44335/student_class/batch_remove_by_userid/" + parseInt(userIds),
                method: "DELETE",
                success: function () {
                    $('#student_table').DataTable().ajax.reload();
                    $('#student_class_table').DataTable().ajax.reload();
                }
            })
        } else {
            // Người dùng nhấn Hủy
            swal("Hủy xóa!", {
                icon: "info",
            });
        }
    });
}