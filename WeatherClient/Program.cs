using System;
using System.Threading.Tasks;
using System.Windows;


namespace WeatherClient
{
    class Program
    {
        
        static void Main(string[] args)
        {
            
            HubConnection connection;

            //using (var hubConnection = new HubConnection("https://localhost:44390/SubscribeWeather"))
            //{
            //    IHubProxy subscribeHubProxy = hubConnection.CreateHubProxy("SubscribeHub");

            //    subscribeHubProxy.On<WeatherObservation>("WeatherUpdate",
            //        w => Console.WriteLine(
            //            $"WeatherUpdate: Pressure: {w.AirPressure}, Humidity {w.Humidity}, Temperature {w.TemperatureC}, Date {w.Date}"));
            //}


            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:53353/ChatHub")
                .Build();
        }
    }
}
