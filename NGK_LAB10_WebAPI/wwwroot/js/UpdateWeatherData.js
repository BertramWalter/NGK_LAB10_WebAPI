"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/SH").build();

connection.on("WeatherUpdate", function (temp, id) {
    var msg = "Weatherstation id: " + id + " Temp: " + temp;

    console.log(msg);
});
