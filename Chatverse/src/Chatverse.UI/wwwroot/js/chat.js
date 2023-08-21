"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5273/chatHub", { withCredentials: true }) // API tarafındaki hub URL'si
    .build();
connection.start().then(function () {
    console.log("SignalR Hub'a baglandi")
}).catch(function (err) {
    return console.error(err.toString());
});