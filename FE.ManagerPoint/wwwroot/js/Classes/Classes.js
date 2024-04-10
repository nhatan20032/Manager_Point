function setupGrid() {

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
        "rowId": "Id",
        "columns": [
            {
                "data": null, "orderable": false, "render": function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            //{ "data": "Id", "orderable": false },
            { "data": "ClassCode", "orderable": false },
            { "data": "Name", "orderable": false },
            { "data": "GradeLevel", "orderable": false },
            { "data": "CourseName", "orderable": false },

            {
                "data": "Status", "orderable": true, "render": function (data) {
                    if (data == 5) return "Trong thời gian";
                    if (data == 4) return "Đã kết thúc";
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
function createClass(object, callback) {
    $.ajax({
        url: "https://localhost:44335/class/create",
        method: "POST",
        data: JSON.stringify(object),
        contentType: 'application/json',
      
        success: function (res) {
         
            if (res.message) {
                toastr.error(res.message);
            }
            else {
                if (callback && typeof callback === "function") {
                    callback();
                }
            }
            $('#class_table').DataTable().ajax.reload();
        },
        error: function (xhr, status, error) {
            var errorMessage = "Unexpected error occurred";
            if (xhr.responseJSON && xhr.responseJSON.message) {
                errorMessage = xhr.responseJSON.message;
            }
            toastr.error(errorMessage);
        }
    });
}




function updateSub(id, object, callback) {
    $.ajax({
        url: `https://localhost:44335/class/modified?id=${id}`,
        method: "PUT",
        data: JSON.stringify(object),
        contentType: 'application/json',
        success: function (res) {

            if (res.message) {
                toastr.error(res.message);
            }
            else {
                if (callback && typeof callback === "function") {
                    callback();
                }
            }
            $('#class_table').DataTable().ajax.reload();
        },
    });
}

function GetById(id) {
    $.ajax({
        url: `https://localhost:44335/class/get_by_id`,
        method: "GET",
        data: {
            id: id,
        },
        success: function (res) {
            console.log(res);
            if (res == null) {
                toastr.error('Không tìm thấy vai trò');
                return;
            }
          
            $("#class_id").val(res.id);
            $("#class_classcode_md").val(res.classCode);
            $("#class_name_md").val(res.name);
            $("#class_gradeLevel_md").val(res.gradeLevel);
            $("#class_courseName_md").val(res.courseId);
            $("#status_list_md").val(res.status)
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
                url: "https://localhost:44335/class/remove?id=" + id,
                method: "DELETE",
                success: function (res) {
                    toastr.success('remove class success');
                    $('#class_table').DataTable().ajax.reload();
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

