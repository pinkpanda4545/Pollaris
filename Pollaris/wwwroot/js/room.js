//JoinRoom 
function joinRoomSubmit(userId) {
    roomCode = $("#join-room-code").val();
    window.location = "/Room/JoinRoomSubmit?userId=" + userId + "&roomCode=" + roomCode; 
}

//Create Room
function createRoomSubmit(userId) {
    roomName = $("#name").val();
    window.location = "/Room/CreateRoomSubmit?userId=" + userId + "&roomName=" + roomName;
}

function copyClipboard() {
    var roomCode = $("#code").val();
    navigator.clipboard.writeText(roomCode);
}