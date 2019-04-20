using System;
using System.Threading.Tasks;
using System.Messaging;

namespace AsyncAwaitReciver
{
    class Program
    {
        static void Main(string[] args)
        {
            Process();
            while(true)
            {
                Print("Some working...");
                System.Threading.Thread.Sleep(1000);
            }
        }

        static async Task Process()
        {
            MessageQueue mq = new MessageQueue(@".\private$\MyPrivateQ");
            mq.Formatter = new XmlMessageFormatter(
                new String[] {
                    "System.String",
                    "System.Single"
                });
            try
            {
                Message msg = await Task.Factory.FromAsync<Message>(
                  mq.BeginReceive(),
                  mq.EndReceive);
                Print(msg.Body.ToString());
            }
            catch
            {
                Print("Timeout!");
            }
            await Process();
        }

        static void Print(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
