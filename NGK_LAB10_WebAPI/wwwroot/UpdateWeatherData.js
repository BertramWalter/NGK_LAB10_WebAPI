"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/SubscribeWeather").build();

connection.on("WeatherUpdate", function(WeatherObservation) {
    var msg =
})