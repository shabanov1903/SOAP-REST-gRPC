using PumpWCFService.Client.PumpServiceReference;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PumpWCFService.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InstanceContext instanceContext = new InstanceContext(new CallbackHandler());
            PumpServiceClient client = new PumpServiceClient(instanceContext);

            string path = Directory.GetCurrentDirectory();
            string file = $@"{path}\Script\Sample.script";
            client.UpdateAndCompileScript(file);
            client.RunScript();

            Console.WriteLine("Please, Enter to exit ...");
            Console.ReadKey(true);
            client.Close();
        }
    }
}
