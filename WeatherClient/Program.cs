using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherClient
{
    class Program
    {
        
        static void Main(string[] args)
        {
            using (var hubConnection = new HubConnection("https://localhost:44390/SubscribeWeather"))
            {
                IHubProxy subscribeHubProxy = hubConnection.CreateHubProxy("SubscribeHub");
                
                subscribeHubProxy.On<WeatherObservation>("WeatherUpdate",
                    w => Console.WriteLine(
                        $"WeatherUpdate: Pressure: {w.AirPressure}, Humidity {w.Humidity}, Temperature {w.TemperatureC}, Date {w.Date}"));
            }
        }
    }
}
