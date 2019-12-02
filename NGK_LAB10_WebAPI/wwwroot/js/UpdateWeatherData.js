"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/Subscriber").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("WeatherUpdate", function (temp, id) {
    var msg = "Weatherstation id: " + id + " Temp: " + temp;

    console.log(msg);
});
