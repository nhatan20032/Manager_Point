var roleCode = sessionStorage.getItem('RoleCode');
if (roleCode !== 'admin') {
    $('#user_manage').hide();
    $('#class_manage').hide();
    $('#course_manage').hide();
    $('#subject_manage').hide();
}
if (roleCode == 'gv') {
    $('#grade_point').hide();
}
if (roleCode == 'hs') {
    $('#headTeacher').hide();
    $('#subjectTeacher').hide();
    $('#analystic').hide();
}
if (roleCode == 'admin') {
    $('#grade_point').hide();
    $('#headTeacher').hide();
    $('#subjectTeacher').hide();
}