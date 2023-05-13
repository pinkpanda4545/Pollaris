// Edit Profile
function changePassword() {
    // Displays the change password popup.
    $("#change-password-popup").css("display", "flex");
}

function closeChangePassword() {
    // Closes the change password popup.
    $("#change-password-popup").css("display", "none");
}

function changeImage(event, userId) {
    // Changes the profile image with the selected image file.
    // Sends an AJAX request to update the profile photo on the server.
    var image = document.getElementById('photo');
    var file = event.target.files;
    var ext = file[0].type;

    if (ext == "image/jpeg" || ext == "image/png") {
        var filePath = URL.createObjectURL(file[0]);
        image.src = filePath;

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
    // Saves the edited profile information by sending an AJAX request to the server.
    // Redirects to the User Dashboard on successful save.
    var firstName = $("#first-name").val();
    var lastName = $("#last-name").val();

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
    // Saves the changed password by sending an AJAX request to the server.
    // Closes the change password popup and redirects to the Edit Profile page.
    var oldPassword = $("#old-password").val();
    var newPassword = $("#new-password").val();
    var newPasswordRepeat = $("#new-password-repeat").val();

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

// MemberList
function openMemberPermissions(userId, roomId, memberId) {
    // Redirects the user to the Member Permissions page for the specified user, room, and member IDs.
    window.location = "/Members/MemberPermissions?userId=" + userId + "&roomId=" + roomId + "&memberId=" + memberId;
}

// MemberPermissions
function makeMemberTA(userId, roomId, memberId) {
    // Intended for TA implementation.
}

function removeTA(userId, roomId, memberId) {
    // Intended for TA implementation.
}

function kick(userId, roomId, memberId) {
    // Intended for TA implementation.
}

