function openUserDashboard(id) {
    window.location = "/Dashboard/UserDashboard?userId=" + id;
}

function returnToUserDashboard(id) {
    window.location = "/Dashboard/UserDashboard?userId=" + id; 
}

function openRoomDashboard(userId, roomId, roomName) {
    window.location = "/Dashboard/RoomDashboard?userId=" + userId +
        "&roomId=" + roomId + "&roomName=" + roomName;
}

function returnToRoomDashboard(userId, roomId) {
    window.location = "/Dashboard/RoomDashboard?userId=" + userId + "&roomId=" + roomId;
}

function openEditProfile(userId) {
    window.location = "/Members/EditProfile?userId=" + userId; 
}

function joinRoom(userId) {
    window.location = "/Room/JoinRoom?userId=" + userId;
}

function createRoom(userId) {
    window.location = "/Room/CreateRoom?userId=" + userId;
} 

function openStudentAnswerPage(userId, roomId) {
    window.location = "/Question/AnswerQuestion?userId=" + userId + "&roomId=" + roomId;
}

function editSet(userId, roomId, setId, setName) {
    window.location = "/Set/EditSet?userId=" + userId + "&roomId=" + roomId + "&setId=" + setId + "&setName=" + setName; 
}

function createSet(userId, roomId) {
    window.location = "/Set/CreateSet?userId=" + userId + "&roomId=" + roomId;
}

function roomMembers(userId, roomId) {
    window.location = "/Members/MemberList?userId=" + userId + "&roomId=" + roomId; 
}

function openSetResponses(userId, roomId, setId) {
    window.location = "/Set/SetResponses?userId=" + userId + "&roomId=" + roomId + "&setId=" + setId;
}