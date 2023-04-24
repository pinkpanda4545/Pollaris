// called when user pressed JoinRoom 
function joinRoomSubmit(userId) {
    roomCode = $("#join-room-code").val();
    window.location = "/Room/JoinRoomSubmit?userId=" + userId + "&roomCode=" + roomCode; 
}

function joinRoomExit(userId) {
    window.location = "/Dashboard/UserDashboard?userId=" + userId;
}

function createRoomSubmit(userId) {
    roomName = $("#name").val();
    roomCode = $("#code").val();
    window.location = "/Room/CreateRoomSubmit?userId=" + userId + "&roomName=" + roomName + "&roomCode=" + roomCode;
}

function exitCreateRoom(userId) {
    window.location = "/Dashboard/UserDashboard?userId=" + userId;
}

function copyClipboard() {
    var roomCode = $("#code").val();
    navigator.clipboard.writeText(roomCode);
}