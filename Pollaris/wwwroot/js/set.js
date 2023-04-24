function exitToRoomDashboard(userId, roomId) {
    window.location = "/Dashboard/RoomDashboard?userId=" + userId + "&roomId=" + roomId;
}

function cancelSetEdits() {
    //refresh page (delete any unsaved changes)
}

function saveSetEdits(userId, roomId, setId) {
    //NEED MORE INFO AS PARAMS!
    window.location = "/Set/SaveSetEdits?userId=" + userId + "&roomId=" + roomId + "&setId=" + setId;
}

function openCreateQuestion(userId, roomId, setId) {
    window.location = "/Question/CreateQuestion?userId=" + userId + "&roomId=" + roomId + "&setId=" + setId;
}

function exitAndChangeStatus(userId, roomId, setId) {
    //CHANGE STATUS FROM LAUNCH TO CONTINUE
    window.location = "/Dashboard/RoomDashboard?userId=" + userId + "&roomId=" + roomId;
}

function changeQuestionAndReset(userId, roomId, setId, questionId) {
    window.location = "/Set/ChangeQuestionAndReset?userId=" + userId + "&roomId=" + roomId + "&setId=" + setId + "&questionId=" + questionId;
}

function finishSetAndClose(userId, roomId, setId) {
    //CHANGE STATUS FROM CONTINUE TO RESET
    window.location = "/Dashboard/RoomDashboard?userId=" + userId + "&roomId=" + roomId;
}