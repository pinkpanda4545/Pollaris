function signInSubmit() {
    // Retrieves the email and password from the input fields and redirects the user to validate the user
    // credentials by sending an AJAX request to the server.
    email = $("#email").val();
    password = $("#password").val(); 

    window.location = "/Home/ValidateUser?email=" + email + "&password=" + password; 
}

function signUpSubmit() {
    // Retrieves the first name, last name, email, and password from the input fields. Validates the password
    // for length and specific characters.If valid, redirects the user to create a new user by sending an AJAX
    // request to the server.If not valid, displays an alert message.
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
