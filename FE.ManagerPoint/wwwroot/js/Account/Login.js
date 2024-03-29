function Login(request) {
    $.ajax({
        url: "https://localhost:44335/user/authenticate",
        method: "POST",
        data: JSON.stringify(request),
        contentType: 'application/json',
        success: function (res) {
            console.log(res)
            sessionStorage.setItem('Token', res.token);
            sessionStorage.setItem('UserId', res.id);
            sessionStorage.setItem('UserCode', res.userCode);
            sessionStorage.setItem('UserRole', res.role);
            if (sessionStorage.getItem('Token')) {
                // Chuyển hướng đến trang Home/Index
                window.location.href = '/Home/Index';
            }
        },
    })
}