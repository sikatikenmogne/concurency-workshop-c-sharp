using System;

namespace concurency_workshop.NS_CLIENT
{
    public class CLclient
    {
        
        private string _name;
        public CLclient(NS_SERVER.CLserver server, string name)
        {
            _name = name;
            
            server.MessageReceived += Server_MessageReceived;

            Console.WriteLine($"Initialisation du client {_name}...ok");

        }   
        
        private void Server_MessageReceived(object sender, string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"Message recu par {_name} -- > {message}");
            Console.ResetColor();
        }
    }
}