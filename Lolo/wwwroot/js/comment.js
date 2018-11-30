"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/commentHub").build();

connection.on("ReceiveComment", function (user, content, postId) {
    var p = document.createElement("p");
    p.textContent = user + ": " + content;
    document.getElementById(postId).appendChild(p);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});
