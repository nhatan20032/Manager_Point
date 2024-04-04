function setupGrid() {

    this.$table = $('#course_table').DataTable({
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
            "url": "https://localhost:44335/course/get_all",
            "data": function (d) {
                delete d.columns;
                d.search = d.search.value;

            },
            "dataSrc": "data"

        },
        "rowId": "Id",
        "columns": [
            { "data": "Id", "orderable": false },
            { "data": "Name", "orderable": false },
            {
                "data": "StartTime",
                "orderable": false,
                "render": function (data) {
                    return new Date(data).toLocaleDateString('en-GB');
                }
            },
            {
                "data": "EndTime",
                "orderable": false,
                "render": function (data) {
                    return new Date(data).toLocaleDateString('en-GB');
                }
            },
            { "data": "Description", "orderable": false },
            {
                "data": "Status", "orderable": false, "render": function (data) {
                    if (data == 1) return "Hoạt động";
                    if (data == 2) return "Không thành công";
                    if (data == 3) return "Đạt";
                    if (data == 4) return "Đã kết thúc";
                    if (data == 5) return "Trong thời gian";
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
function createCourse(object, callback) {
    debugger
    $.ajax({
        url: "https://localhost:44335/course/create",
        method: "POST",
        data: JSON.stringify(object),
        contentType: 'application/json',
        success: function (res) {
            if (callback && typeof callback === "function") {
                callback();
            }
            $('#course_table').DataTable().ajax.reload();
        },
    });
}

function updateSub(id, object, callback) {
    $.ajax({
        url: `https://localhost:44335/course/modified?id=${id}`,
        method: "PUT",
        data: JSON.stringify(object),
        contentType: 'application/json',
        success: function (res) {
            if (callback && typeof callback === "function") {
                callback();
            }
            $('#course_table').DataTable().ajax.reload();
        },
    });
}

function GetById(id) {
    $.ajax({
        url: `https://localhost:44335/course/get_by_id`,
        method: "GET",
        data: {
            id: id,
        },
        success: function (res) {
           
            if (res == null) {
                toastr.error('Không tìm thấy vai trò');
                return;
            }
            $("#course_id").val(res.id);
            $("#course_name_md").val(res.name);
            $("#dateStart_md").val(res.startTime);
            $("#dateEnd_md").val(res.endTime);
            $("#course_description_md").val(res.description);
            $("#status_list_md").val(res.status);
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
                url: "https://localhost:44335/course/remove?id=" + id,
                method: "DELETE",
                success: function (res) {
                    toastr.success('Xóa khóa học  thành công');
                    $('#course_table').DataTable().ajax.reload();
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

