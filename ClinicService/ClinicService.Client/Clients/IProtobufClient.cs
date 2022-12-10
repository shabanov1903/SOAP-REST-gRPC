using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ClinicServiceNamespace.ClinicService;

namespace ClinicService.Client.Clients
{
    internal interface IProtobufClient<Request>
        where Request : class
    {
        void Create(Request item);
        void GetAll();
    }
}
