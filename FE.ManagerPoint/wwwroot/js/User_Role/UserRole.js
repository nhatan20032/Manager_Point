var selectedIdsRole = [];
var selectedIdsUser = [];
function userNoRoleGrid() {
    this.$table = $('#user_role_table').DataTable({
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
            $('#user_role_table').on('change', '.select-checkbox', function () {
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

function roleGrid() {
    this.$table = $('#role_table').DataTable({
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
            "url": "https://localhost:44335/role/get_all",
            "data": function (d) {
                delete d.columns;
                d.search = d.search.value;
            },
            "dataSrc": "data"
        },
        "initComplete": function () {
            $('#role_table').on('change', '.select-checkbox-role', function () {
                var id = $(this).data('id');
                if ($(this).prop('checked')) {
                    if (!selectedIdsRole.includes(id)) {
                        selectedIdsRole.push(id);
                    }
                } else {
                    var index = selectedIdsRole.indexOf(id);
                    if (index !== -1) {
                        selectedIdsRole.splice(index, 1);
                    }
                }
                console.log(selectedIdsRole);
            });
            $(document).on('change', '#checkAllCheckboxRole', function () {
                var checkboxes = $('.select-checkbox-role');
                checkboxes.prop('checked', $(this).prop('checked'));
                if ($(this).prop('checked')) {
                    checkboxes.each(function () {
                        var id = $(this).data('id');
                        if (!selectedIdsRole.includes(id)) {
                            selectedIdsRole.push(id); // Thêm ID vào mảng nếu chưa tồn tại
                        }
                    });
                } else {
                    selectedIdsRole = []; // Thiết lập lại mảng selectedIds khi checkbox "Check All" bị bỏ chọn
                }
                console.log(selectedIdsRole);
            });
            this.api().on('draw', function () {
                selectedIdsRole = [];
                $('#checkAllCheckboxRole').prop('checked', false);
                $('.select-checkbox-role').prop('checked', false);
            });
        },
        "rowId": "Id",
        "columns": [
            {
                "data": null,
                "className": "checkbox-column",
                "orderable": false,
                "title": '<input type="checkbox" id="checkAllCheckboxRole">',
                "render": function (data) {
                    return `<input type="checkbox" class="select-checkbox-role" data-id="${data.Id}">`;
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
function CreateRole(callback) {
    var mergedData = [];

    for (var i = 0; i < selectedIdsUser.length; i++) {
        var userId = selectedIdsUser[i];
        for (var j = 0; j < selectedIdsRole.length; j++) {
            var roleId = selectedIdsRole[j];
            var userData = {
                userId: userId,
                roleId: roleId
            };
            mergedData.push(userData);
        }
    }

    console.log(mergedData);
    $.ajax({
        url: "https://localhost:44335/role_user/batch_create",
        method: "POST",
        data: JSON.stringify(mergedData),
        contentType: 'application/json',
        success: function (res) {
            if (callback && typeof callback === "function") {
                callback();
            }
            $('#user_role_table').DataTable().ajax.reload();
            $('#role_table').DataTable().ajax.reload();
        },
    });
}