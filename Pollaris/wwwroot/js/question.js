//AnswerQuestion

function chooseTF(bool) {
    // Updates the visual representation of true/false options based on the
    // boolean value provided.Adds or removes a CSS class to highlight the selected option.
    if (bool) {
        $("#div-true").addClass("dark-green");
        $("#div-false").removeClass("dark-green");
    } else {
        $("#div-false").addClass("dark-green");
        $("#div-true").removeClass("dark-green");
    }
}

function chooseMC(optionName) {
    // Updates the visual representation of multiple-choice options based on the optionName provided.
    // Adds a CSS class to highlight the selected option and removes the class from other options.
    $("#" + optionName).parent().children().removeClass("dark-green");
    $("#" + optionName).addClass("dark-green");  
}

function answerQuestionSubmit(userId, roomId, setId, questionId, questionType) {
    // Gathers the selected answers for a question and submits them to the server. The answers are
    // determined based on the question type(multiple - choice, true / false, ranking, or short answer).
    // Sends an AJAX request to submit the answers.
    answers = []
    if (questionType == "MC") {
        grid = $(".grid-mc").children();
        options = $(".grid-mc").children(".dark-green");
        for (var i = 0; i < options.length; i++) {
            answers.push(options[i].textContent); 
        }
    } else if (questionType == "TF") {
        options = $(".div-tf-container").children(".dark-green");
        for (var i = 0; i < options.length; i++) {
            answers.push(options[i].textContent);
        }
    } else if (questionType == "R") {
        options = $("#grid-ranking-answers").children();
        for (var i = 0; i < options.length; i++) {
            if (options[i].textContent != '') {
                answers.push(options[i].textContent);
            }
        }
    } else {
        answers.push($("#div-sa").val());
    }

    var datastring = { userId: userId, roomId: roomId, questionId: questionId, answers: answers };

    $.ajax({
        url: "/Question/SubmitStudentAnswer",
        method: "POST",
        data: datastring
    })
        .done(function (result) {
            if (result) {
                $("#submit-answer").addClass("grey");
                $("#submit-answer").attr("disabled", true);
            }
        })
        .fail(function () {
            alert('ERROR - question.js, answerQuestionSubmit');
        });
}

function allowDrop(ev) {
    // Allows dropping of draggable elements by preventing the default behavior of the drop event.
    ev.preventDefault();
}

function drag(ev) {
    // Initiates the drag operation by setting the data to be transferred in the drag event.
    ev.dataTransfer.setData("text", ev.target.id);
}

function drop(ev) {
    // Handles the drop event by appending the dragged element to the target element if it is empty.
    ev.preventDefault();
    var data = ev.dataTransfer.getData("text");
    if (ev.target.firstElementChild == null) {
        ev.target.appendChild(document.getElementById(data));
    }
}

//Create Question 
function createQuestion(userId, roomId, setId, type) {
    // Redirects the user to a page for creating and editing a new question based on the provided parameters.
    window.location = "/Question/CreateThenEditQuestion?userId=" + userId + "&roomId=" + roomId +
        "&setId=" + setId + "&type=" + type; 
}

//Edit Question
function redCircle(element, optionId) {
    // Toggles the appearance of a red circle or an empty circle for an option. Used in the context of
    // editing a multiple - choice question.
    var isCorrect = false; 
    if ($(element).attr('class') == "red-circle") {
        $(element).removeClass("red-circle");
        $(element).addClass("empty-circle");
    } else {
        $(element).removeClass("empty-circle");
        $(element).addClass("red-circle");
        isCorrect = true;
    }
    var datastring = { optionId: optionId, isCorrect: isCorrect };

    $.ajax({
        url: "/Option/MakeOptionCorrect",
        method: "POST",
        data: datastring
    });
}

function chooseButton(element, questionId) {
    // Toggles the selection of buttons (graded/anonymous) and updates their appearance.
    // Sends an AJAX request to update the corresponding question property.
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
    // Creates a new option for a question and adds it to the UI. Sends an AJAX request to create
    // the option on the server.
    var datastring = { questionId: questionId };

    $.ajax({
        url: "/Option/CreateNewOption",
        method: "POST",
        data: datastring
    }).done((result) => {
        addOption(result, questionType);
    });
}

function addOption(id, questionType) {
    // Adds an option to the UI for editing a question. Updates the layout of the question editing grid.
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
    // Removes an option from the UI for editing a question. Sends an AJAX request to delete
    // the option from the server.Updates the layout of the question editing grid.
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

function saveQuestionEdits(userId, roomId, setId, questionId, type) {
    // Saves the edits made to a question, including the question name. Sends an AJAX request to update the
    // question name on the server.If the question type is not short answer, it proceeds to save the option
    // names as well.
    var questionName = $("#txtarea-edit-question").val(); 
    var datastring = { questionId: questionId, questionName: questionName };

    $.ajax({
        url: "/Question/ChangeQuestionName",
        method: "POST",
        data: datastring
    }).done(() => {
        if (type != "SA") {
            saveOptionNames(userId, roomId, setId);
        } else {
            openEditSet(userId, roomId, setId); 
        }
    });

}

function saveOptionNames(userId, roomId, setId) {
    // Saves the edits made to option names for a multiple - choice question. Sends an AJAX request to
    // update each option name on the server.
    var elements = $("#edit-question-grid").children(".question-name-bar"); 

    for (var i = 0; i < elements.length; i++) {
        var optionId = $(elements[i]).attr("id").substring(4);
        var optionName = $(elements[i]).val();
        console.log(elements[i]);

        var datastring = { optionId: optionId, optionName: optionName };

        $.ajax({
            url: "/Option/ChangeOptionName",
            method: "POST",
            data: datastring
        }).done(() => {
            openEditSet(userId, roomId, setId);
        });
    }
}