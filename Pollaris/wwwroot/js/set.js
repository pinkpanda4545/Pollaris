//EDIT SET

function openCreateQuestion(userId, roomId, setId) {
    window.location = "/Question/CreateQuestion?userId=" + userId + "&roomId=" + roomId + "&setId=" + setId;
}

function openEditQuestion(userId, roomId, setId, questionId) {
    window.location = "/Question/EditQuestion?userId=" + userId + "&roomId=" + roomId + "&setId=" + setId + "&questionId=" + questionId;
}

function deleteQuestion(questionId) {
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
    $("#delete-set-popup").css('display', 'flex'); 
}

function closeDeleteSetPopup() {
    $("#delete-set-popup").css('display', 'none'); 
}

function deleteSet(userId, roomId, setId) {
    closeDeleteSetPopup();
    window.location = "/Set/DeleteSet?userId=" + userId + "&roomId=" + roomId + "&setId=" + setId; 
}

function changeName(roomName, setName) {
    $("#txtarea-change-set-name").val(setName);
    $("#txtarea-change-set-name").css('display', 'block');
    $("#h1-set-name").text(roomName + " - New Set Name:");
    $("#btn-submit-name").css('display', 'block');
    $("#btn-change-name").css('display', 'none');
}

function submitNewName(setId, roomName) {
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
    window.location = "/Set/ChangeQuestionAndReset?userId=" + userId + "&roomId=" + roomId + "&setId=" + setId + "&questionId=" + questionId;
}

function finishSetAndClose(userId, roomId, setId) {
    window.location = "/Set/FinishStatusAndExit?userId=" + userId + "&roomId=" + roomId + "&setId=" + setId;
}

function exitAndChangeStatus(userId, roomId, setId) {
    window.location = "/Set/ContinueStatusAndExit?userId=" + userId + "&roomId=" + roomId + "&setId=" + setId;
}