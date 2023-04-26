function openUserDashboard(id) {
    window.location = "/Dashboard/UserDashboard?userId=" + id;
}

//UserDashboard
function openRoomDashboard(userId, roomId) {
    window.location = "/Dashboard/RoomDashboard?userId=" + userId + "&roomId=" + roomId;
}

function openEditProfile(userId) {
    window.location = "/Members/EditProfile?userId=" + userId; 
}

function openJoinRoom(userId) {
    window.location = "/Room/JoinRoom?userId=" + userId;
}

function openCreateRoom(userId) {
    window.location = "/Room/CreateRoom?userId=" + userId;
} 

function openAnswerQuestion(userId, roomId) {
    window.location = "/Question/AnswerQuestion?userId=" + userId + "&roomId=" + roomId;
}

//Room Dashboard
function openEditSet(userId, roomId, setId) {
    window.location = "/Set/EditSet?userId=" + userId + "&roomId=" + roomId + "&setId=" + setId; 
}

function createSet(userId, roomId) {
    window.location = "/Set/CreateSet?userId=" + userId + "&roomId=" + roomId;
}

function openMemberList(userId, roomId) {
    window.location = "/Members/MemberList?userId=" + userId + "&roomId=" + roomId; 
}

function launchSet(userId, roomId, setId) {
    window.location = "/Set/LaunchSet?userId=" + userId + "&roomId=" + roomId + "&setId=" + setId;
}

function continueSet(userId, roomId, setId) {
    window.location = "/Set/ContinueSet?userId=" + userId + "&roomId=" + roomId + "&setId=" + setId;
}

function resetSet(userId, roomId, setId) {
    window.location = "/Set/ResetSet?userId=" + userId + "&roomId=" + roomId + "&setId=" + setId;
}

function exportCSV(userId, roomId, setId) {

}