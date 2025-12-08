var connectionUserCount = new signalR.HubConnectionBuilder().withUrl("/hubs/userCount").build();

connectionUserCount.on("updateTotalViews", (value) => {
    var newCountSpan = document.getElementById("totalViewsCounter");
    newCountSpan.innerText = value.toString();
});

connectionUserCount.on("updateTotalUsers", (value) => {
    var newCountSpan = document.getElementById("totalUsersCounter");
    newCountSpan.innerText = value.toString();
});

function newWindowLoadedOnClient() {
    connectionUserCount.send("NewWindowLoaded");
}

function fulfilled() {
    console.log("Connection to User Hub Succesful")
    newWindowLoadedOnClient();
}

function rejected() {

}

connectionUserCount.start().then(fulfilled, rejected)