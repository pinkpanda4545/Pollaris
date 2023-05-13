//EDIT SET

function openCreateQuestion(userId, roomId, setId) {
    // Redirects the user to the page for creating a new question within a set. Passes the user ID, room ID,
    // and set ID as parameters.
    window.location = "/Question/CreateQuestion?userId=" + userId + "&roomId=" + roomId + "&setId=" + setId;
}

function openEditQuestion(userId, roomId, setId, questionId) {
    // Redirects the user to the page for editing a specific question within a set.Passes the user ID, room ID,
    // set ID, and question ID as parameters.
    window.location = "/Question/EditQuestion?userId=" + userId + "&roomId=" + roomId + "&setId=" + setId + "&questionId=" + questionId;
}

function deleteQuestion(questionId) {
    // Deletes a question from the UI and sends an AJAX request to delete the question from the server.
    // Updates the layout of the question list.
    $("#ln-" + questionId).remove(); 
    $("#qnb-" + questionId).remove(); 
    $("#qt-" + questionId).remove(); 
    $("#bs-" + questionId).remove(); 
    $("#bd-" + questionId).remove();

    var datastring = { questionId: questionId };

    $.ajax({
        url: "/Question/DeleteQuestion",
        method: "POST",
        data: datastring
    });

    var numberList = $("#questions-list").children(".list-number");
    $("#questions-list").css("grid-template-rows", "repeat(" + numberList.length + ", 100px)");
    for (let i = 0; i < numberList.length; i++) {
        numberList[i].textContent = i + 1; 
    } 
}

function deleteSetPopup() {
    // Displays the popup for confirming the deletion of a set.
    $("#delete-set-popup").css('display', 'flex'); 
}

function closeDeleteSetPopup() {
    // Closes the popup for confirming the deletion of a set.
    $("#delete-set-popup").css('display', 'none'); 
}

function deleteSet(userId, roomId, setId) {
    // Deletes a set and its associated questions. Redirects the user to the appropriate page after deletion.
    // Sends a request to the server to delete the set.
    closeDeleteSetPopup();
    window.location = "/Set/DeleteSet?userId=" + userId + "&roomId=" + roomId + "&setId=" + setId; 
}

function changeName(roomName, setName) {
    // Displays the form for changing the name of a set.
    $("#txtarea-change-set-name").val(setName);
    $("#txtarea-change-set-name").css('display', 'block');
    $("#h1-set-name").text(roomName + " - New Set Name:");
    $("#btn-submit-name").css('display', 'block');
    $("#btn-change-name").css('display', 'none');
}

function submitNewName(setId, roomName) {
    // Submits the new name for a set and updates the UI. Sends an AJAX request to update the set name on the server.
    var newName = $("#txtarea-change-set-name").val();
    $("#txtarea-change-set-name").text() == "";
    $("#txtarea-change-set-name").css("display", "none");
    $("#h1-set-name").text(roomName + " - " + newName);
    $("#btn-submit-name").css('display', 'none');
    $("#btn-change-name").css('display', 'block');

    var datastring = { setId: setId, newName: newName };

    $.ajax({
        url: "/Set/ChangeSetName",
        method: "POST",
        data: datastring
    });
}

//Set Responses

function changeQuestionAndReset(userId, roomId, setId, questionId) {
    // Redirects the user to change the active question within a set and reset the responses.
    // Passes the user ID, room ID, set ID, and question ID as parameters.
    window.location = "/Set/ChangeQuestionAndReset?userId=" + userId + "&roomId=" + roomId + "&setId=" + setId + "&questionId=" + questionId;
}

function finishSetAndClose(userId, roomId, setId) {
    // Marks a set as finished and closes it. Redirects the user to the appropriate page.
    // Passes the user ID, room ID, and set ID as parameters.
    window.location = "/Set/FinishStatusAndExit?userId=" + userId + "&roomId=" + roomId + "&setId=" + setId;
}

function exitAndChangeStatus(userId, roomId, setId) {
    // Exits a set while keeping it open and allows for further changes. Redirects the user to the appropriate page.
    // Passes the user ID, room ID, and set ID as parameters.
    window.location = "/Set/ContinueStatusAndExit?userId=" + userId + "&roomId=" + roomId + "&setId=" + setId;
}