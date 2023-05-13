function openUserDashboard(id) {
    // Redirects the user to the User Dashboard page for the specified user ID.
    window.location = "/Dashboard/UserDashboard?userId=" + id;
}

// UserDashboard
function openRoomDashboard(userId, roomId) {
    // Redirects the user to the Room Dashboard page for the specified user and room IDs.
    window.location = "/Dashboard/RoomDashboard?userId=" + userId + "&roomId=" + roomId;
}

function openEditProfile(userId) {
    // Redirects the user to the Edit Profile page for the specified user ID.
    window.location = "/Members/EditProfile?userId=" + userId;
}

function openJoinRoom(userId) {
    // Redirects the user to the Join Room page for the specified user ID.
    window.location = "/Room/JoinRoom?userId=" + userId;
}

function openCreateRoom(userId) {
    // Redirects the user to the Create Room page for the specified user ID.
    window.location = "/Room/CreateRoom?userId=" + userId;
}

function openAnswerQuestion(userId, roomId) {
    // Redirects the user to the Answer Question page for the specified user and room IDs.
    window.location = "/Question/AnswerQuestion?userId=" + userId + "&roomId=" + roomId;
}

// Room Dashboard
function openEditSet(userId, roomId, setId) {
    // Redirects the user to the Edit Set page for the specified user, room, and set IDs.
    window.location = "/Set/EditSet?userId=" + userId + "&roomId=" + roomId + "&setId=" + setId;
}

function createSet(userId, roomId) {
    // Redirects the user to the Create Set page for the specified user and room IDs.
    window.location = "/Set/CreateSet?userId=" + userId + "&roomId=" + roomId;
}

function openMemberList(userId, roomId) {
    // Redirects the user to the Member List page for the specified user and room IDs.
    window.location = "/Members/MemberList?userId=" + userId + "&roomId=" + roomId;
}

function launchSet(userId, roomId, setId) {
    // Redirects the user to the Set Responses page for the specified user, room, and set IDs,
    // with the newStatus parameter set to "C" (indicating a new status).
    window.location = "/Set/SetResponses?userId=" + userId + "&roomId=" + roomId + "&setId=" + setId + "&newStatus=C";
}

function continueSet(userId, roomId, setId) {
    // Redirects the user to the Set Responses page for the specified user, room, and set IDs,
    // with the newStatus parameter set to "C" (indicating continuing from a previous status).
    window.location = "/Set/SetResponses?userId=" + userId + "&roomId=" + roomId + "&setId=" + setId + "&newStatus=C";
}

function resetSet(userId, roomId, setId) {
    // Redirects the user to the Set Responses page for the specified user, room, and set IDs,
    // with the newStatus parameter set to "C" (indicating resetting the set status).
    window.location = "/Set/SetResponses?userId=" + userId + "&roomId=" + roomId + "&setId=" + setId + "&newStatus=C";
}

function exportCSV(userId, roomId, setId) {
    // Intended for completing the functionality of exporting to a CSV.
}
