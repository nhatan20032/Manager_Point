var selectedIdsSubject = [];
var selectedIdsUser = [];
function teacherGrid() {
    this.$table = $('#teacher_subject_table').DataTable({
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
                d.subject = $("#search_subject").val();
                d.check_subject = 1;
            },
            "dataSrc": "data"
        },
        "rowId": "Id",
        "columns": [
            { 'data': 'Id', "orderable": false },
            { 'data': 'Name', "orderable": false },
            { 'data': 'User_Code', "orderable": false },
            { 'data': 'Subject_User', "orderable": false },
            {
                "data": "AvatarUrl", "className": "img_td_teacher", "orderable": false, render: function (data) {
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
function teacherNoSBGrid() {
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
                d.subject = $("#search_subject").val();
                d.check_subject = 2;
            },
            "dataSrc": "data"
        },
        "initComplete": function () {
            teacherGrid();
            $('#teacher_table').on('change', '.select-checkbox', function () {
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
function subjectGrid() {
    this.$table = $('#subject_table').DataTable({
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
            "url": "https://localhost:44335/subject/get_all",
            "data": function (d) {
                delete d.columns;
                d.search = d.search.value;
            },
            "dataSrc": "data"
        },
        "initComplete": function () {
            $('#subject_table').on('change', '.select-checkbox-subject', function () {
                var id = $(this).data('id');
                if ($(this).prop('checked')) {
                    if (!selectedIdsSubject.includes(id)) {
                        selectedIdsSubject.push(id);
                    }
                } else {
                    var index = selectedIdsSubject.indexOf(id);
                    if (index !== -1) {
                        selectedIdsSubject.splice(index, 1);
                    }
                }
                console.log(selectedIdsSubject);
            });
            $(document).on('change', '#checkAllCheckboxSubject', function () {
                var checkboxes = $('.select-checkbox-subject');
                checkboxes.prop('checked', $(this).prop('checked'));
                if ($(this).prop('checked')) {
                    checkboxes.each(function () {
                        var id = $(this).data('id');
                        if (!selectedIdsSubject.includes(id)) {
                            selectedIdsSubject.push(id); // Thêm ID vào mảng nếu chưa tồn tại
                        }
                    });
                } else {
                    selectedIdsSubject = []; // Thiết lập lại mảng selectedIds khi checkbox "Check All" bị bỏ chọn
                }
                console.log(selectedIdsSubject);
            });
            this.api().on('draw', function () {
                selectedIdsSubject = [];
                $('#checkAllCheckboxSubject').prop('checked', false);
                $('.select-checkbox-subject').prop('checked', false);
            });
        },
        "rowId": "Id",
        "columns": [
            {
                "data": null,
                "className": "checkbox-column",
                "orderable": false,
                "title": '<input type="checkbox" id="checkAllCheckboxSubject">',
                "render": function (data) {
                    return `<input type="checkbox" class="select-checkbox-subject" data-id="${data.Id}">`;
                }
            },
            { 'data': 'Id', "orderable": false },
            { 'data': 'Name', "orderable": false },
            {
                "data": "Status", "orderable": false, render: function (data) {
                    if (data == 1) return "Chờ duyệt";
                    if (data == 2) return "Đã duyệt";
                    if (data == 3) return "Từ chối duyệt";
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
    window.dt = this.$table;
}
function getSubject_user() {
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
function CreateSubject_Teacher(callback) {
    var mergedData = [];

    for (var i = 0; i < selectedIdsUser.length; i++) {
        var userId = selectedIdsUser[i];
        for (var j = 0; j < selectedIdsSubject.length; j++) {
            var subjectId = selectedIdsSubject[j];
            var userData = {
                userId: userId,
                subjectId: subjectId
            };
            mergedData.push(userData);
        }
    }

    console.log(mergedData);
    $.ajax({
        url: "https://localhost:44335/subject_tecaher/batch_create",
        method: "POST",
        data: JSON.stringify(mergedData),
        contentType: 'application/json',
        success: function (res) {
            if (callback && typeof callback === "function") {
                callback();
            }
            $('#subject_table').DataTable().ajax.reload();
            $('#teacher_table').DataTable().ajax.reload();
            $('#teacher_subject_table').DataTable().ajax.reload();
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
            $("#subject_selected").val(res.subject_id).trigger('change');
            $("#updateModal").modal("show");
        },
    });
}
function getSubject() {
    $.ajax({
        url: "https://localhost:44335/subject/get_list",
        method: "GET",
        success: function (res) {
            if (res && res.length > 0) {
                console.log(res);
                var select = $("#subject_selected");
                $.each(res, function (index, role) {
                    select.append(`<option value="${role.id}">${role.name}</option>`);
                });
            }
        },
    });
}
function editSubject(userIds, ids, callback) {
    var intIds = ids.map(function (item) {
        return parseInt(item);
    });
    var mergedData = [];
    for (var j = 0; j < intIds.length; j++) {
        var roleId = intIds[j];
        var userData = {
            userId: userIds,
            subjectId: roleId
        };
        mergedData.push(userData);
    }
    $.ajax({
        url: "https://localhost:44335/subject_tecaher/batch_remove_by_userid/" + parseInt(userIds),
        method: "DELETE",
        success: function () {
            $.ajax({
                url: "https://localhost:44335/subject_tecaher/batch_create",
                method: "POST",
                data: JSON.stringify(mergedData),
                contentType: 'application/json',
                success: function (res) {
                    if (callback && typeof callback === "function") {
                        callback();
                    }
                    mergedData = [];
                    $('#teacher_table').DataTable().ajax.reload();
                    $('#teacher_subject_table').DataTable().ajax.reload();
                },
                error: function (xhr, status, error) {
                    console.error("Lỗi khi tạo các roles mới:", error);
                }
            });
        },
        error: function (xhr, status, error) {
            console.error("Lỗi khi xóa các roles cũ:", error);
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
                url: "https://localhost:44335/subject_tecaher/batch_remove_by_userid/" + parseInt(userIds),
                method: "DELETE",
                success: function () {
                    $('#teacher_table').DataTable().ajax.reload();
                    $('#teacher_subject_table').DataTable().ajax.reload();
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