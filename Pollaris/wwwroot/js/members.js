//Edit Profile
function changePassword() {
    $("#change-password-popup").css("display", "flex");
}

function closeChangePassword() {
    $("#change-password-popup").css("display", "none"); 
}

function changeImage() {

}

function editProfileSave(userId) {
    firstName = $("#edit-profile-first-name").val();
    lastName = $("#edit-profile-last-name").val();

    var datastring = { firstName: firstName, lastName: lastName };

    $.ajax({
        url: "/Members/SaveProfileInformation",
        method: "POST",
        data: datastring
    })
        .done(function (result) {
            if (result) {
                window.location = "/Dashboard/UserDashboard?userId=" + userId;
            }
        })
        .fail(function () {
            alert('ERROR - members.js, saveProfileInformation');
        });
}

function savePassword(userId) { 
    oldPassword = $("#old-password").val();
    newPassword = $("#new-password").val();
    newPasswordRepeat = $("#new-password-repeat").val();

    var datastring = { userId: userId, oldPassword: oldPassword, newPassword: newPassword, newPassword2: newPasswordRepeat };

    $.ajax({
        url: "/Members/ValidateAndSavePassword",
        method: "POST",
        data: datastring
    })
        .done(function (result) {
            if (result) {
                closeChangePassword(); 
            }
        })
        .fail(function () {
            alert('ERROR - members.js, savePassword');
        });
}

//MemberList

function openMemberPermissions(userId, roomId, memberId) {
    window.location = "/Members/MemberPermissions?userId=" + userId + "&roomId=" + roomId + "&memberId=" + memberId;
}

//MemberPermissions

function makeMemberTA(userId, roomId, memberId) {
    //member controller
}

function removeTA(userId, roomId, memberId) {
    //member controller
}

function kick(userId, roomId, memberId) {
    //member controller
}