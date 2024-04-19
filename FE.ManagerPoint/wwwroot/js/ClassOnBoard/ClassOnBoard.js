function GetClassOnBoard(classInfo) {
    if (classInfo && classInfo.length > 0) {
        var classInfo10Html = '';
        var classInfo11Html = '';
        var classInfo12Html = '';

        classInfo.forEach(item => {
            var statusBadge = item.status === "Đang giảng dạy" ?
                '<span class="badge rounded-pill bg-success">Đang giảng dạy</span>' :
                '<span class="badge rounded-pill bg-danger">Đã giảng dạy</span>';

            var classCardHtml = `
                <div class="card mt-3" style="width: 20rem;">
                    <img class="card-img-top" src="https://images.template.net/112327/free-classroom-image-background-0680z.jpg" alt="${item.classData.Name}">
                    <div class="card-body">
                        <h3 class="card-title">Tên lớp: ${item.classData.Name}</h3>
                        <h5 class="card-title">Mã Lớp: ${item.classData.ClassCode}</h5>
                        <p class="card-text">Khối: ${item.classData.GradeLevel}</p>
                        <p class="card-text">Trạng thái: ${statusBadge}</p>
                        <a href="#" class="btn btn-primary">Vào lớp</a>
                    </div>
                </div>
            `;

            switch (item.classData.GradeLevel) {
                case 10:
                    classInfo10Html += classCardHtml;
                    break;
                case 11:
                    classInfo11Html += classCardHtml;
                    break;
                case 12:
                    classInfo12Html += classCardHtml;
                    break;
                default:
                    break;
            }
        });

        document.getElementById("classInfo10Div").innerHTML = classInfo10Html || "<div style='text-align: center;'><h5>Không có lớp</h5></div>";
        document.getElementById("classInfo11Div").innerHTML = classInfo11Html || "<div style='text-align: center;'><h5>Không có lớp</h5></div>";
        document.getElementById("classInfo12Div").innerHTML = classInfo12Html || "<div style='text-align: center;'><h5>Không có lớp</h5></div>";

    } else {
        document.getElementById("classInfo10Div").innerHTML = "<h5>No class information available.</h5>";
        document.getElementById("classInfo11Div").innerHTML = "<h5>No class information available.</h5>";
        document.getElementById("classInfo12Div").innerHTML = "<h5>No class information available.</h5>";
    }
}
