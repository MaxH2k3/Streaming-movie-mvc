// Connection to hub server
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/notification")
    .build();