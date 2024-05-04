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
        }   
        
        private void Server_MessageReceived(object sender, string message)
        {
            Console.WriteLine($"{_name} received message: {message}");
        }
    }
}