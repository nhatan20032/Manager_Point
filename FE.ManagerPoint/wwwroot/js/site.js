var roleCode = sessionStorage.getItem('RoleCode');
var userId = parseInt(sessionStorage.getItem('UserId'));
var classId = parseInt(sessionStorage.getItem('ClassId'));
if (!roleCode.includes('admin')){
    $('#user_manage').hide();
    $('#class_manage').hide();
    $('#course_manage').hide();
    $('#subject_manage').hide();
}
if (roleCode.includes('gv')) {
    $('#grade_point').hide();
    $('#analystic').hide();
}
if (roleCode.includes('hs')){
    $('#headTeacher').hide();
    $('#subjectTeacher').hide();
    $('#analystic').hide();
}
if (roleCode.includes('admin')) {
    $('#grade_point').hide();
    $('#headTeacher').hide();
    $('#subjectTeacher').hide();
}