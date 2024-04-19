function GetClassOnBoard(classInfo) {
    if (classInfo && classInfo.length > 0) {
        var classInfoHtml = '';
        classInfo.forEach(item => {
            var statusBadge = '';
            if (item.status === "Đang chủ nhiệm") {
                statusBadge = '<span class="badge rounded-pill bg-success">Đang chủ nhiệm</span>';
            } else {
                statusBadge = '<span class="badge rounded-pill bg-danger">Đã chủ nhiệm</span>';
            }
            classInfoHtml += `
                    <div class="card mt-3" style="width: 20rem;">
                                <img class="card-img-top" src="https://images.template.net/112327/free-classroom-image-background-0680z.jpg" alt="${item.classData.Name}">
                        <div class="card-body">
                                <h3 class="card-title">Tên lớp: ${item.classData.Name}</h5>
                                <h5 class="card-title">Mã Lớp: ${item.classData.ClassCode}</h5>
                                <p class="card-text">Khối: ${item.classData.GradeLevel}</p>
                                <p class="card-text">Trạng thái: ${statusBadge}</p>
                                <a href="" class="btn btn-primary" ">Vào lớp</a>
                        </div>
                    </div>
                    `;
            subject_InClass = '';
            real_subject = '';
        });
        document.getElementById("classInfoDiv").innerHTML = classInfoHtml;
    } else {
        document.getElementById("classInfoDiv").innerHTML = "No class information available.";
    }
}