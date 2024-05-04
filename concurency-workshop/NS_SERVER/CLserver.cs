using System;

namespace concurency_workshop.NS_SERVER
{
    public class CLserver
    {
        public CLserver()
        {
            Console.WriteLine("Initialisation du serveur...ok");
        }
        
        // Declare a delegate for the event
        public delegate void MessageEventHandler(object sender, string message);

        // Declare the event using the delegate
        public event MessageEventHandler MessageReceived;
        
        private string _msg;
        public string Msg { 
            get { return _msg; }
            set
            {
                _msg = value;
                // Trigger the event when the message is set
                MessageReceived?.Invoke(this, _msg);    
            }
        } 
    }
}