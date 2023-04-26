//AnswerQuestion

function chooseTF(bool) {
    if (bool) {
        $("#div-true").addClass("dark-green");
        $("#div-false").removeClass("dark-green");
    } else {
        $("#div-false").addClass("dark-green");
        $("#div-true").removeClass("dark-green");
    }
}

function chooseMC(optionName) {
    $("#" + optionName).parent().children().removeClass("dark-greenn");
    $("#" + optionName).addClass("dark-green");  
}

function answerQuestionSubmit() {
    //Take all the information and do something with it. 
    //Switch questions when done. 
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

//Create Question 
function createQuestion(userId, roomId, setId, type) {
    window.location = "/Question/CreateThenEditQuestion?userId=" + userId + "&roomId=" + roomId +
        "&setId=" + setId + "&type=" + type; 
}

//Edit Question
function redCircle(element) {
    if ($(element).attr('class') == "red-circle") {
        $(element).removeClass("red-circle");
        $(element).addClass("empty-circle");
    } else {
        $(element).removeClass("empty-circle");
        $(element).addClass("red-circle");
    }
}

function chooseButton(element) {
    if ($(element).attr('id') == "graded-yes") {
        $(element).addClass("chosen-button");
        $("#graded-no").removeClass("chosen-button");
    } else if ($(element).attr('id') == "graded-no") {
        $(element).addClass("chosen-button");
        $("#graded-yes").removeClass("chosen-button");
    } else if ($(element).attr("id") == "anonymous-yes") {
        $(element).addClass("chosen-button");
        $("#anonymous-no").removeClass("chosen-button");
    } else if ($(element).attr("id") == "anonymous-no") {
        $(element).addClass("chosen-button");
        $("#anonymous-yes").removeClass("chosen-button");
    }
} 

function addOption(questionType) {
    var id = $("#edit-question-grid").children(".list-number").length + 1;
    $("#edit-question-grid").css("grid-template-rows", "repeat(" + id + ", 100px)");
    $("#edit-question-grid").append("<div id='ln-" + id + "' class='list-number'>" + id + "</div>");
    $("#edit-question-grid").append("<textarea id='qnb-" + id + "' class='question-name-bar'></textarea>");
    if (questionType != "R") {
        $("#edit-question-grid").append("<div id='circle-" + id + "' class='empty-circle' onclick='redCircle(this)'></div>");
    } else {
        $("#edit-question-grid").append("<div id='circle-" + id + "'></div>");
    }
    if (questionType != "TF") {
        $("#edit-question-grid").append(
            "<div id='bd-" + id + "' class='btn-delete' onclick='deleteOption(" + id + ")'>" +
            "<img class='img-btn-delete' src='/images/trash.png' />" +
            "</div>"
        );
    } else {
        $("#edit-question-grid").append("<div id='bd-" + id + "'></div>");
    }
}

function deleteOption(optionId) {
    $("#ln-" + optionId).remove(); 
    $("#qnb-" + optionId).remove();
    $("#circle-" + optionId).remove();
    $("#bd-" + optionId).remove();

    var numberList = $("#edit-question-grid").children(".list-number");
    $("#edit-question-grid").css("grid-template-rows", "repeat(" + numberList.length + ", 100px)");
    for (let i = 0; i < numberList.length; i++) {
        numberList[i].textContent = i + 1;
    }
}

function saveQuestionEdits() {
    //Save Question Edits
}