function Get_Teacher_No_HomeRoom() {
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
            "url": "https://localhost:44335/user/Get_All_Teacher_No_HomeRoom",
            "data": function (d) {
                delete d.columns;
                d.search = d.search.value;
                //d.classes = $("#search_class").val();
                //d.subject = $("#search_subject").val();
            },
            "dataSrc": "data"
        },
        "rowId": "Id",
        "initComplete": function () {
            $('#teacher_table').on('change', '.select-checkbox-teacher', function () {
                var id = $(this).data('id');
                if ($(this).prop('checked')) {
                    if (selectedIdsUser !== null && selectedIdsUser !== id) {
                        // Uncheck previous row if another row is checked
                        $('#teacher_table').find(`[data-id="${selectedIdsUser}"]`).prop('checked', false);
                    }
                    selectedIdsUser = id;
                } else {
                    selectedIdsUser = null;
                }
                console.log(selectedIdsUser);
            });
            this.api().on('draw', function () {
                selectedIdsUser = null;
                $('.select-checkbox-teacher').prop('checked', false);
            });
        },
        "columns": [
            {
                "data": null,
                "className": "checkbox-column",
                "orderable": false,
                "render": function (data) {
                    return `<input type="checkbox" class="select-checkbox-teacher" data-id="${data.Id}">`;
                }
            },
            { 'data': 'Id', "orderable": false },
            { 'data': 'Name', "orderable": false },
            { 'data': 'User_Code', "orderable": false },
            { 'data': 'Subject_User', "orderable": false },
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
function getClass() {
    // Gọi API để lấy dữ liệu
    $.ajax({
        url: 'https://localhost:44335/class/get_list',
        type: 'GET',
        success: function (response) {
            console.log(response)
            // Khi nhận được dữ liệu từ API
            if (response && response.length > 0) {
                // Lặp qua từng đối tượng trong dữ liệu
                response.forEach(function (item) {
                    // Tạo HTML cho mỗi card
                    var cardHtml = `
                                <div class="card mt-3" style="width: 18rem;">
                                    <img class="card-img-top" src="https://th.bing.com/th/id/R.887d4cd1bf56ab24260b630f02610e7e?rik=Q20YCivVEVZoYw&pid=ImgRaw&r=0" alt="${item.name}">
                                    <div class="card-body">
                                        <h3 class="card-title">Tên lớp: ${item.name}</h5>
                                        <h5 class="card-title">Mã Lớp: ${item.classCode}</h5>
                                        <p class="card-text">Khối: ${item.gradeLevel}</p>
                                        <a href="/Class/Add_Teacher_To_Class?id=${item.id}" class="btn btn-primary" ">Vào lớp</a>
                                    </div>
                                </div>
                            `;

                    // Thêm card vào container
                    $('#cardContainer').append(cardHtml);
                });
            } else {
                // Xử lý trường hợp không có dữ liệu trả về
                $('#cardContainer').append('<p>No data available</p>');
            }
        },
        error: function () {
            // Xử lý khi có lỗi xảy ra khi gọi API
            $('#cardContainer').append('<p>Error loading data</p>');
        }
    });
}
function Get_HomeRoom_Teacher(idClass) {
    $.ajax({
        url: 'https://localhost:44335/user/Get_By_HomeRoom_Id?idClass=' + idClass,
        type: 'GET',
        success: function (response) {
            if (response != null) {
                // Lặp qua từng đối tượng trong dữ liệu và tạo card
                console.log(response);
                $('#cardTeacher').empty();
                var cardHtml = `
                        <div class="card mt-3" style="width: 100%">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-3">
                                        <img class="card-img-top" src="${response.avatarUrl}" alt="${response.name}">
                                    </div>
                                    <div class="col-9">
                                        <h3 class="card-title">Tên giáo viên: ${response.name}</h5>
                                        <h5 class="card-title">Mã Lớp: ${response.teacher_Class_Code}</h5>
                                        <p class="card-text">Mã người dùng: ${response.user_Code}</p>
                                        <button id="editHomeRoom" class="btn btn-dark" data-bs-toggle="modal" data-bs-target="#updateModal">Đổi giáo viên chủ nhiệm</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    `;
                $('#cardTeacher').append(cardHtml);
                idUser = response.id;
                $('#create_homeroom').hide();
            } else {
                $('#create_homeroom').show();
                $('#cardTeacher').append('<h2 style="text-align: center;">Lớp chưa có giáo viên chủ nhiệm</h2>');
            }
        },
        error: function () {
            // Xử lý khi có lỗi xảy ra khi gọi API
            $('#cardTeacher').append('<p>Error loading data</p>');
        }
    });
}
function Get_Subject_Teacher(idClass) {
    $.ajax({
        url: `https://localhost:44335/user/Get_By_Subject_Teacher_Id?idClass=${idClass}`,
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            console.log(response);
            if (response && response.length > 0) {
                // Xóa hết các card trước đó trước khi append mới
                $('#cardTeacherSubject').empty();
                $.each(response, function (index, teacher) {
                    // Tạo một phần tử div để hiển thị thông tin giáo viên
                    var cardHtml = `
                        <div class="card mt-3" style="width: 28rem;" id="teacherCard">
                            <img class="card-img-top" src="${teacher.data.AvatarUrl}" alt="${teacher.data.Name}">
                            <div class="card-body">
                                <h3 class="card-title">Tên giáo viên: ${teacher.data.Name}</h3>
                                <h5 class="card-title">Bộ môn: ${teacher.data.Subject_User}</h5>
                                <h5 class="card-title">Bộ môn dạy tại lớp: ${teacher.subjectInClass}</h5>
                                <p class="card-text">Mã người dùng: ${teacher.data.User_Code}</p>
                                <p class="card-text">Số điện thoại: ${teacher.data.PhoneNumber}</p>
                                <button class="btn btn-danger button_delete" style="display: none;" onclick="Delete_Teacher_Subject(${teacher.data.Id}, ${teacher.idSubject}, ${idClass})">Xoá giáo viên bộ môn</button>
                            </div>
                        </div>
                    `;
                    // Thêm card vào container
                    $('#cardTeacherSubject').append(cardHtml);
                });
            } else {
                // Xóa hết các card trước đó trước khi append mới
                $('#cardTeacherSubject').empty();

                // Xử lý trường hợp không có dữ liệu trả về
                $('#cardTeacherSubject').append('<p>No data available</p>');
            }
        },
        error: function () {
            // Xóa hết các card trước đó trước khi append mới
            $('#cardTeacherSubject').empty();

            // Xử lý khi có lỗi xảy ra khi gọi API
            $('#cardTeacherSubject').append('<p>Error loading data</p>');
        }
    });
}

function Delete_Teacher_Subject(userId, subjectId, idClass) {
    swal({
        title: "Bạn chắc chắn muốn xóa?",
        text: "Hành động này không thể hoàn tác!",
        icon: "warning",
        buttons: ["Hủy", "Xóa"],
        dangerMode: true,
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: `https://localhost:44335/teacher_class/Remove_Item_By_IdUser_and_IdSubject/?userId=${userId}&subjectId=${subjectId}`,
                method: "DELETE",
                success: function () {
                    Get_Subject_Teacher(idClass);
                    $('#myToggle').removeClass('active');
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

function Add_Teacher_HomeRoom(idClass, callback) {
    var mergedData = [];
    var userId = selectedIdsUser;
    var classId = idClass;
    var userData = {
        userId: userId,
        classId: classId,
        typeTeacher: 0
    };
    mergedData.push(userData);
    console.log(mergedData);
    $.ajax({
        url: "https://localhost:44335/teacher_class/batch_create_homeroom",
        method: "POST",
        data: JSON.stringify(mergedData),
        contentType: 'application/json',
        success: function (res) {
            if (res && res.length < 0) {
                toastr.error('Đã xảy ra lỗi xin vui lòng thử lại');
            }
            if (callback && typeof callback === "function") {
                callback();
            }
            $('#teacher_table').DataTable().ajax.reload();
        }
    })
}

function editSubject(idClass, callback) {
    var mergedData = [];
    var userId = selectedIdsUser;
    var classId = idClass;
    var userData = {
        userId: userId,
        classId: classId,
        typeTeacher: 0
    };
    mergedData.push(userData);
    $.ajax({
        url: "https://localhost:44335/teacher_class/Batch_Remove_Item_HomeRoom/" + classId,
        method: "DELETE",
        success: function () {
            $.ajax({
                url: "https://localhost:44335/teacher_class/batch_create_homeroom",
                method: "POST",
                data: JSON.stringify(mergedData),
                contentType: 'application/json',
                success: function (res) {
                    if (callback && typeof callback === "function") {
                        callback();
                    }
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
function getSubject() {
    $.ajax({
        url: "https://localhost:44335/subject/get_list",
        method: "GET",
        success: function (res) {
            if (res && res.length > 0) {
                var select = $("#subject_selected");
                $.each(res, function (index, item) {
                    select.append($("<option></option>")
                        .attr("value", item.id)
                        .text(item.name));
                });
            }
        },
    });
}

function Get_Teacher_No_Subject() {
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
                d.subject = $("#subject_selected").val();
            },
            "dataSrc": "data"
        },
        "rowId": "Id",
        "initComplete": function () {
            $('#teacher_subject_table').on('change', '.select-checkbox-teacher', function () {
                var id = $(this).data('id');
                if ($(this).prop('checked')) {
                    if (selectedIdsUser !== null && selectedIdsUser !== id) {
                        // Uncheck previous row if another row is checked
                        $('#teacher_subject_table').find(`[data-id="${selectedIdsUser}"]`).prop('checked', false);
                    }
                    selectedIdsUser = id;
                } else {
                    selectedIdsUser = null;
                }
                console.log(selectedIdsUser);
            });
            this.api().on('draw', function () {
                selectedIdsUser = null;
                $('.select-checkbox-teacher').prop('checked', false);
            });
        },
        "columns": [
            {
                "data": null,
                "className": "checkbox-column",
                "orderable": false,
                "render": function (data) {
                    return `<input type="checkbox" class="select-checkbox-teacher" data-id="${data.Id}">`;
                }
            },
            { 'data': 'Id', "orderable": false },
            { 'data': 'Name', "orderable": false },
            { 'data': 'User_Code', "orderable": false },
            { 'data': 'Subject_User', "orderable": false },
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
function Create_Subject_Class_Teacher(idClass, idSubject, callback) {
    var mergedData = [];
    var userId = selectedIdsUser;
    var classId = idClass;
    var userData = {
        userId: userId,
        classId: classId,
        subjectId: idSubject,
        typeTeacher: 1
    };
    mergedData.push(userData);
    console.log(mergedData);
    $.ajax({
        url: "https://localhost:44335/teacher_class/batch_create_subject",
        method: "POST",
        data: JSON.stringify(mergedData),
        contentType: 'application/json',
        success: function (res) {
            if (res === "exist") {
                console.log(res);
                toastr.warning('Giáo viên dạy môn này đã tồn tại vui lòng kiểm tra');
            } else {
                if (callback && typeof callback === "function") {
                    callback();
                }
                $('#teacher_subject_table').DataTable().ajax.reload();
                Get_Subject_Teacher(idClass);
            }
        }
    })
}