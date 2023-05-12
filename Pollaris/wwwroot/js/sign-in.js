function signInSubmit() {
    email = $("#email").val();
    password = $("#password").val(); 

    window.location = "/Home/ValidateUser?email=" + email + "&password=" + password; 
}

function signUpSubmit() {
    firstName = $("#first-name").val();
    lastName = $("#last-name").val();
    email = $("#email").val();
    password = $("#password").val(); 

    if (password.length < 8 || password.includes("#") || password.includes("&") || password.includes("%") || password.includes("=")) {
        alert("Please try a different password that is 8+ characters long and doesn't include '#', '&', '%', or '='.");
    } else {
        window.location = "/Home/CreateUser?firstName=" + firstName + "&lastName=" + lastName +
            "&email=" + email + "&password=" + password;
    }
}