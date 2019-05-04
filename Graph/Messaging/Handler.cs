using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace HelpLayer.Messaging
{
    public static class Handler
    {
        //    static MessageQueue SenderQ = new MessageQueue(@".\private$\MyPrivateSenderQ");
        //    static MessageQueue ReciverQ = new MessageQueue(@".\private$\MyPrivateRecieverQ");


        public static void Send(object o, MessageQueue mq)
        { 
            Message msg = new Message();
            msg.Label = o.GetType().AssemblyQualifiedName;
            msg.Formatter = new JsonMessageFormatter();
            msg.Body = o;

            mq.Send(msg);
        }

        public async static Task<Message> Recieve(MessageQueue mq)
        {
            mq.Formatter = new JsonMessageFormatter();

            Message msg =  await Task.Factory.FromAsync<Message>(
                mq.BeginReceive(),
                mq.EndReceive);

            //return msg.Body.ToString();
            return msg;
        }
    }
}
