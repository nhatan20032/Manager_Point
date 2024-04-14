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
