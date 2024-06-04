var roleCode = sessionStorage.getItem('RoleCode');
var userId = parseInt(sessionStorage.getItem('UserId'));
var classId = parseInt(sessionStorage.getItem('ClassId'));
if (roleCode !== 'admin'){
    $('#user_manage').hide();
    $('#class_manage').hide();
    $('#course_manage').hide();
    $('#subject_manage').hide();
}
if (roleCode === 'gv') {
    $('#grade_point').hide();
    $('#analystic').hide();
}
if (roleCode === 'hs'){
    $('#headTeacher').hide();
    $('#subjectTeacher').hide();
    $('#analystic').hide();
}
if (roleCode  === 'admin') {
    debugger
    $('#grade_point').hide();
    $('#headTeacher').hide();
    $('#subjectTeacher').hide();
}