$("#logout").on("click", function () {
    sessionStorage.clear();
    window.location.href = "/Account/Login";
})
