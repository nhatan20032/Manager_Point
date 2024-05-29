function showLoadingSpinner() {
    $("#loading-spinner").show();
    $("#submit").prop("disabled", true);
}

function hideLoadingSpinner() {
    $("#loading-spinner").hide();
    $("#submit").prop("disabled", false);
}
function Login(request) {
    showLoadingSpinner();
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
            sessionStorage.setItem('ClassId', res.classId);
            sessionStorage.setItem('RoleCode', res.roleCode);
            sessionStorage.setItem('UserRole', res.role);
            sessionStorage.setItem('FullName', res.fullName);
            hideLoadingSpinner();
            if (sessionStorage.getItem('Token')) {
                window.location.href = '/Home/Index';
            }
        },
        error: function (xhr, status, error) {
            if (xhr.status === 400) {
                var errorMessage = xhr.responseJSON ? xhr.responseJSON.message : "Bad Request";
                toastr.error(errorMessage, "Error 400");
                hideLoadingSpinner();
            }
        }
    })
}

