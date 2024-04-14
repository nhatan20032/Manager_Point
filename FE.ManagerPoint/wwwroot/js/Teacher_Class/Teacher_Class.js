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
                    if (selectedRowIdClass !== null && selectedRowIdClass !== id) {
                        // Uncheck previous row if another row is checked
                        $('#teacher_table').find(`[data-id="${selectedRowIdClass}"]`).prop('checked', false);
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
function createClass() {
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
        success: function (response, textStatus, xhr) {
            if (response && response.length > 0) {
                // Lặp qua từng đối tượng trong dữ liệu và tạo card
                response.forEach(function (item) {
                    var cardHtml = `
                        <div class="card mt-3" style="width: 100%">
                            <div class="card-body">
                                <h3 class="card-title">Tên lớp: ${item.name}</h5>
                                <h5 class="card-title">Mã Lớp: ${item.classCode}</h5>
                                <p class="card-text">Khối: ${item.gradeLevel}</p>
                                <a href="/Class/Add_Teacher_To_Class?id=${item.id}" class="btn btn-primary" ">Vào lớp</a>
                            </div>
                        </div>
                    `;
                    $('#cardTeacher').append(cardHtml);
                });
                $('#create_homeroom').hide();
            } else {
                $('#create_homeroom').show();
                $('#cardTeacher').append('<h2>Lớp chưa có giáo viên chủ nhiệm</h2>');
            }
        },
        error: function () {
            // Xử lý khi có lỗi xảy ra khi gọi API
            $('#cardTeacher').append('<p>Error loading data</p>');
        }
    });
}
