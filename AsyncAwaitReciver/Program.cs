﻿using System;
using System.Threading.Tasks;
using System.Messaging;
using Newtonsoft.Json;

namespace AsyncReciver
{
    class Program
    {
        static string name = "Reciever";
        static MessageQueue SenderQ = new MessageQueue(@".\private$\MyPrivateSenderQ");
        static MessageQueue RecieverQ = new MessageQueue(@".\private$\MyPrivateRecieverQ");

        static void Main(string[] args)
        {
            while (true)
            {
                Message msg = Recieve(SenderQ).Result;

                var _A = (HelpLayer.LoopedGraph.Model.A)msg.Body;

                HelpLayer.Messaging.Handler.Send(
                HelpLayer.LoopedGraph.Handler.SwopC(_A), RecieverQ);


            }
        }

        public static async Task Send(object o, MessageQueue mq)
        {
            try
            {
                HelpLayer.Messaging.Handler.Send(o, mq);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"({name})Exception when sending!\n    {ex.Message}");
            }

            await Recieve(RecieverQ);
        }

        public static async Task<Message> Recieve(MessageQueue mq)
        {
            Message res = null;
            try
            {
                res = 
                    await HelpLayer.Messaging.Handler.Recieve(mq);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"({name})Exception when recieving!\n    {ex.Message}");
            }
            Console.WriteLine(HelpLayer.LoopedGraph.Handler.Serialize(res.Body));
            return res;
        }
    }
}