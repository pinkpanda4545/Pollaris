 //handles the C# connection with jquery
//type is GET because you are getting whether the combination is valid
//url is the hook to the C#
//data is the C# parameters
//If the query runs successfully and doesn't throw an error, it will enter the done function
//the done function will then begin the next step of getting the user's data
//and opening their dashboard
//otherwise, it will tell the user that their nuid/password combination is invalid
//that will be in the fail function.
//both the done and fail functions will call separate functions for better readability
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
