using System;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.AspNetCore.SignalR.Client;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            HubConnection connection;

            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44390//Subscriber")
                .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            while (true)
            {
                if (Console.ReadKey().Equals("c"))
                {

                }


            }
        }


    }
}
