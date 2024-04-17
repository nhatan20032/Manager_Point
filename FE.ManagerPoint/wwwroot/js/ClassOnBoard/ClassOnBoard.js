function GetClassOnBoard(classInfo) {
    if (classInfo && classInfo.length > 0) {
        var classInfoHtml = '';
        var subject_InClass = '';
        var real_subject = '';
        classInfo.forEach(item => {
            item.subjects.forEach(a => {
                subject_InClass += a.Name + ', ';
            });
            real_subject = subject_InClass.slice(0, -2);
            var statusBadge = '';
            if (item.status === "Đang giảng dạy") {
                statusBadge = '<span class="badge rounded-pill bg-success">Đang giảng dạy</span>';
            } else {
                statusBadge = '<span class="badge rounded-pill bg-danger">Đã giảng dạy</span>';
            }
            classInfoHtml += `
                    <div class="card mt-3" style="width: 20rem;">
                                <img class="card-img-top" src="https://th.bing.com/th/id/R.f2e7f87378d459e66b392710a2b78166?rik=2yS%2b0rf6cyD%2b3w&pid=ImgRaw&r=0" alt="${item.classData.Name}">
                        <div class="card-body">
                                <h3 class="card-title">Tên lớp: ${item.classData.Name}</h5>
                                <h5 class="card-title">Mã Lớp: ${item.classData.ClassCode}</h5>
                                <p class="card-text">Khối: ${item.classData.GradeLevel}</p>
                                <p class="card-text">Môn dạy: ${real_subject}</p>
                                <p class="card-text">Giáo viên chủ nhiệm: ${item.homeroom}</p>
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