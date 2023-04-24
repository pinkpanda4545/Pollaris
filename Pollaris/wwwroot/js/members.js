function changePassword() {
    $("#change-password-popup").css("display", "flex");
}

function closeChangePassword() {
    $("#change-password-popup").css("display", "none"); 
}

function changeImage() {

}

function editProfileCancel(userId) {
    window.location = "/Dashboard/UserDashboard?userId=" + userId;
}

function editProfileSave(userId) {
    firstName = $("#edit-profile-first-name").val();
    lastName = $("#edit-profile-last-name").val();

    var datastring = { firstName: firstName, lastName: lastName };

    $.ajax({
        url: "/Members/EditProfile",
        method: "POST",
        data: datastring
    })
        .done(function (result) {
            if (result) {
                window.location = "/Dashboard/UserDashboard";
            }
        })
        .fail(function () {
            alert('ERROR - members.js, editProfileSave');
        });
}


function goToMemberPermissions(userId, roomId, memberId) {
    window.location = "/Members/MemberPermissions?userId=" + userId + "&roomId=" + roomId + "&memberId=" + memberId;
}

function makeMemberTA() {
    //member controller
}

function removeTA() {
    //member controller
}

function kick() {
    //member controller
}