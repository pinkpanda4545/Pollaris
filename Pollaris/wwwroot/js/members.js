//Edit Profile
function changePassword() {
    $("#change-password-popup").css("display", "flex");
}

function closeChangePassword() {
    $("#change-password-popup").css("display", "none"); 
}

function changeImage(event,userId) {
    var image = document.getElementById('photo');
    var file = event.target.files;
    var ext = file[0].type;
    if (ext == "image/jpeg" || ext == "image/png") {
        var filePath = URL.createObjectURL(file[0]);
        image.src = filePath
        $.ajax({
            url: "/Members/ChangeProfilePhoto",
            method: "POST",
            data: {
                userId: userId,
                src: filePath
            }
        });
    } else {
        window.alert("File type not supported. Extension must be .png or .jpg");
    }
}

function editProfileSave(userId) {
    firstName = $("#first-name").val();
    lastName = $("#last-name").val();

    if (firstName == null || lastName == null) {
        alert("You must enter both a first and last name.");
    } else {
        var datastring = { userId: userId, firstName: firstName, lastName: lastName };

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
            closeChangePassword();
            openEditProfile(result); 
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