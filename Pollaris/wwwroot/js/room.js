//JoinRoom 
function joinRoomSubmit(userId) {
    // Submits the entered room code and redirects the user to join the corresponding room.
    // Sends a request to the server to join the room with the provided user ID and room code.
    roomCode = $("#join-room-code").val();
    window.location = "/Room/JoinRoomSubmit?userId=" + userId + "&roomCode=" + roomCode; 
}

//Create Room
function createRoomSubmit(userId) {
    // Submits the entered room name and redirects the user to create a new room.
    // Sends a request to the server to create a new room with the provided user ID and room name.
    roomName = $("#name").val();
    window.location = "/Room/CreateRoomSubmit?userId=" + userId + "&roomName=" + roomName;
}

function copyClipboard() {
    // Copies the room code to the clipboard.
    var roomCode = $("#code").val();
    navigator.clipboard.writeText(roomCode);
}