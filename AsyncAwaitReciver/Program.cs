using System;
using System.Threading.Tasks;
using System.Messaging;

namespace AsyncAwaitReciver
{
    class Program
    {
        static void Main(string[] args)
        {
            Task t = Process();
            while (true)
            {
                Print("Some working...");
                System.Threading.Thread.Sleep(1000);
            }
        }

        static async Task OnMessageArrival(IAsyncResult ar)
        {
            MessageQueue mq = (MessageQueue)ar;
            try
            {
                Message msg = mq.EndReceive(ar);
                Console.WriteLine(msg.Body.ToString());
            }
            catch
            {
                Console.WriteLine("TimeOut!");
            }
            finally
            {
                mq.BeginReceive(TimeSpan.FromSeconds(5), mq);
                await OnMessageArrival(ar);
            }
        }

        static void OnReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            MessageQueue mq = (MessageQueue)sender;
            try
            {
                Message msg = mq.EndReceive(e.AsyncResult);
                Console.WriteLine(msg.Body.ToString());
            }
            catch (MessageQueueException mqError)
            {
                Console.WriteLine(mqError.Message);
            }
            finally
            {
                mq.BeginReceive(TimeSpan.FromSeconds(5));
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
            IAsyncResult ar = mq.BeginReceive(TimeSpan.FromSeconds(5), mq);
            await OnMessageArrival(ar);
        }

        static void Print(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
