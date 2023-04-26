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

    window.location = "/Home/CreateUser?firstName=" + firstName + "&lastName=" + lastName +
        "&email=" + email + "&password=" + password;
}
