using System;
using System.Threading.Tasks;
using System.Messaging;
using Newtonsoft.Json;

namespace AsyncSender
{
    class Program
    {
        static string name = "Sender";
        static MessageQueue SenderQ = new MessageQueue(@".\private$\MyPrivateSenderQ");
        static MessageQueue RecieverQ = new MessageQueue(@".\private$\MyPrivateRecieverQ");

        static void Main(string[] args)
        {
            while (true)
            {
                string cmd = Console.ReadLine();
                switch (cmd)
                {
                    case "q":
                        return;
                    case "s":
                        Send(HelpLayer.LoopedGraph.Handler.InitA("AAA"), SenderQ);
                        break;
                }

            }
        }

        public static void Send(object o, MessageQueue mq)
        {
            try
            {
                HelpLayer.Messaging.Handler.Send(o, mq);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"({name})Exception when sending!\n    {ex.Message}");
            }

            Recieve(RecieverQ);
        }

        public static async Task Recieve(MessageQueue mq)
        {
            try
            {
                Message msg = await HelpLayer.Messaging.Handler.Recieve(mq);
                
                Console.WriteLine(HelpLayer.LoopedGraph.Handler.Serialize(msg.Body));
            }
            catch(Exception ex)
            {
                Console.WriteLine($"({name})Exception when recieving!\n    {ex.Message}");
            }
        }
    }
}
