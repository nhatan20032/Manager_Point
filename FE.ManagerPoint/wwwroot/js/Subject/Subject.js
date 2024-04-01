function setupGrid() {
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
                d.search = d.search.value;
            },
            "dataSrc": "data"
        },
        "rowId": "Id",
        "columns": [
            { 'data': 'Id', "orderable": false },
            { 'data': 'Name', "orderable": false },
            { 'data': 'Description', "orderable": false },
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
    window.dt = this.$table;
}
function createRole(object, callback) {
    $.ajax({
        url: "https://localhost:44335/subject/create",
        method: "POST",
        data: JSON.stringify(object),
        contentType: 'application/json',
        success: function (res) {
            if (callback && typeof callback === "function") {
                callback();
            }
            $('#subject_table').DataTable().ajax.reload();
        },
    });
}

function updateRole(id, object, callback) {
    $.ajax({
        url: `https://localhost:44335/subject/modified?id=${id}`,
        method: "PUT",
        data: JSON.stringify(object),
        contentType: 'application/json',
        success: function (res) {
            if (callback && typeof callback === "function") {
                callback();
            }
            $('#subject_table').DataTable().ajax.reload();
        },
    });
}

function GetById(id) {
    $.ajax({
        url: `https://localhost:44335/subject/get_by_id`,
        method: "GET",
        data: {
            id: id,
        },
        success: function (res) {
            if (res == null) {
                toastr.error('Không tìm thấy vai trò');
                return;
            }
            $("#id_subject").val(res.id);
            $("#subject_name_md").val(res.name);
            $("#description_md").val(res.description);
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
                url: "https://localhost:44335/subject/remove?id=" + id,
                method: "DELETE",
                success: function (res) {
                    toastr.success('Xóa vai trò thành công');
                    $('#subject_table').DataTable().ajax.reload();
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

