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
    $("#" + optionName).parent().children().removeClass("dark-green");
    $("#" + optionName).addClass("dark-green");  
}

function answerQuestionSubmit(userId, roomId, setId, questionId, questionType) {
    answers = []
    if (questionType == "MC") {
        answers = $("#grid-mc").children(".dark-green").val();
    } else if (questionType == "TF") {
        answers = $("#div-tf-container").children(".dark-green").val();
    } else if (questionType == "R") {
        answers = $("#grid-ranking").children().val(); 
    } else {
        answers = $("#div-sa").val();
    }

    var datastring = { userId: userId, roomId: roomId, questionId: questionId, answers: answers };

    $.ajax({
        url: "/Question/SubmitStudentAnswer",
        method: "POST",
        data: datastring
    })
        .done(function (result) {
            if (result) {
              //???   
            }
        })
        .fail(function () {
            alert('ERROR - question.js, answerQuestionSubmit');
        });
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
function redCircle(element, optionId) {
    if ($(element).attr('class') == "red-circle") {
        $(element).removeClass("red-circle");
        $(element).addClass("empty-circle");
    } else {
        $(element).removeClass("empty-circle");
        $(element).addClass("red-circle");

        var datastring = { optionId: optionId };

        $.ajax({
            url: "/Question/MakeOptionCorrect",
            method: "POST",
            data: datastring
        });
    }
}

function chooseButton(element, questionId) {
    var changeGraded = false; 
    var changeToYes = false; 
    if ($(element).attr('id') == "graded-yes") {
        $(element).addClass("chosen-button");
        $("#graded-no").removeClass("chosen-button");
        changeGraded = true;
        changeToYes = true;
    } else if ($(element).attr('id') == "graded-no") {
        $(element).addClass("chosen-button");
        $("#graded-yes").removeClass("chosen-button");
        changeGraded = true;
    } else if ($(element).attr("id") == "anonymous-yes") {
        $(element).addClass("chosen-button");
        $("#anonymous-no").removeClass("chosen-button");
        changeToYes = true;
    } else if ($(element).attr("id") == "anonymous-no") {
        $(element).addClass("chosen-button");
        $("#anonymous-yes").removeClass("chosen-button");
    }

    var datastring = { questionId: questionId, isGraded: changeToYes };

    if (changeGraded) {
        $.ajax({
            url: "/Question/ChangeGraded",
            method: "POST",
            data: datastring
        });
    } else {
        $.ajax({
            url: "/Question/ChangeAnonymous",
            method: "POST",
            data: datastring
        });
    }
} 

function createNewOption(questionId, questionType) {
    var datastring = { questionId: questionId };

    $.ajax({
        url: "/Question/CreateNewOption",
        method: "POST",
        data: datastring
    }).done((result) => {
        addOption(result, questionType);
    });
}

function addOption(id, questionType) {
    var index = $("#edit-question-grid").children(".list-number").length + 1;
    $("#edit-question-grid").css("grid-template-rows", "repeat(" + index + ", 100px)");
    $("#edit-question-grid").append("<div id='ln-" + id + "' class='list-number'>" + index + "</div>");
    $("#edit-question-grid").append("<textarea id='qnb-" + id + "' class='question-name-bar'></textarea>");
    if (questionType == "MC") {
        $("#edit-question-grid").append("<div id='circle-" + id + "' class='empty-circle' onclick='redCircle(this)'></div>");
    } else {
        $("#edit-question-grid").append("<div id='circle-" + id + "'></div>");
    }
    $("#edit-question-grid").append(
        "<div id='bd-" + id + "' class='btn-delete' onclick='deleteOption(" + id + ")'>" +
        "<img class='img-btn-delete' src='/images/trash.png' />" +
        "</div>"
    );
}

function deleteOption(optionId) {
    $("#ln-" + optionId).remove(); 
    $("#qnb-" + optionId).remove();
    $("#circle-" + optionId).remove();
    $("#bd-" + optionId).remove();

    var datastring = { optionId: optionId };

    $.ajax({
        url: "/Option/DeleteOption",
        method: "POST",
        data: datastring
    });

    var numberList = $("#edit-question-grid").children(".list-number");
    $("#edit-question-grid").css("grid-template-rows", "repeat(" + numberList.length + ", 100px)");
    for (let i = 0; i < numberList.length; i++) {
        numberList[i].textContent = i + 1;
    }
}

function saveOptionNames(userId, roomId, setId) {
    var elements = $("#edit-question-grid").children(".question-name-bar");
    elements.foreach((elem) => {
        var optionId = $(elem).attr("id").substring(4); 
        var optionName = $(elem).val(); 

        var datastring = { optionId: optionId, optionName: optionName };

        $.ajax({
            url: "/Option/ChangeOptionName",
            method: "POST",
            data: datastring
        });
    });
}