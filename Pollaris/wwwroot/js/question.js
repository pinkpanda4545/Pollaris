function exitQuestionSet(userId) {
    window.location = "/Dashboard/UserDashboard?userId=" + userId;
}

function chooseTF(bool) {
    if (bool) {
        $("#div-true").addClass("chosen-option");
        $("#div-false").removeClass("chosen-option");
    } else {
        $("#div-false").addClass("chosen-option");
        $("#div-true").removeClass("chosen-option");
    }
}

function chooseMC(optionName) {
    $("#" + optionName).parent().children().removeClass("chosen-option");
    $("#" + optionName).addClass("chosen-option");  
}

function answerQuestionSubmit() {

}

function allowDrop(ev) {
    ev.preventDefault();

    if (ev.target.className == "ranking-question-grid-container") {
        dragged.parentNode.removeChild(dragged);
        ev.target.appendChild(dragged);
    }
}

function drag(ev) {
    ev.dataTransfer.setData("text", ev.target.id);
}

function drop(ev) {
    ev.preventDefault();
    var data = ev.dataTransfer.getData("text");
    ev.target.appendChild(document.getElementById(data));
}

function createQuestion(userId, roomId, setId, type) {
    window.location = "/Question/CreateThenEditQuestion?userId=" + userId + "&roomId=" + roomId +
        "&setId=" + setId + "&type=" + type; 
}

function returnToEditSet(userId, roomId, setId) {
    window.location = "/Set/EditSet?userId=" + userId + "&roomId=" + roomId + "&setId=" + setId;
}
