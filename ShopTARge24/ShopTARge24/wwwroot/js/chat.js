var connectionChat = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/chat")
    .build();

document.getElementById("sendButton").disabled = true;

connectionChat.on("ReceiveMessage", (user, message, time) => {
    var msg = document.createElement("div");
    msg.innerHTML = `<strong>${user}</strong> (${time}): ${message}`;
    document.getElementById("messagesList").appendChild(msg);
});

connectionChat.start().then(() => {
    console.log("Connected to chat");
    document.getElementById("sendButton").disabled = false;
});

document.getElementById("sendButton").addEventListener("click", () => {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    var time = new Date().toLocaleString();

    connectionChat.send("SendMessage", user, message, time);

    document.getElementById("messageInput").value = "";
});
