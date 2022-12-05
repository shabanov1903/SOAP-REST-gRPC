using ClinicServiceNamespace;
using static ClinicServiceNamespace.ClinicService;

namespace ClinicService.Client.Clients.Impl
{
    internal class ClientPBClient : IProtobufClient<CreateClientRequest>
    {
        private readonly ClinicServiceClient _client;
        public ClientPBClient(ClinicServiceClient client) {
            _client = client;
        }

        public void Create(CreateClientRequest item)
        {
            var response = _client.CreateClinet(item);

            if (response.ErrCode == 0)
            {
                Console.WriteLine($"Client #{response.ClientId} created successfully.");
            }
            else
            {
                Console.WriteLine($"Create client error.\nErrorCode: {response.ErrCode}\nErrorMessage: {response.ErrMessage}");
            }
        }

        public void GetAll()
        {
            var response = _client.GetClients(new GetClientsRequest());

            if (response.ErrCode == 0)
            {
                Console.WriteLine($"Objects of type: {typeof(GetClientsRequest)}");

                foreach (var client in response.Clients)
                {
                    Console.WriteLine($"#{client.ClientId} {client.Document} {client.Surname} {client.FirstName} {client.Patronymic}");
                }
            }
            else
            {
                Console.WriteLine($"Get clients error.\nErrorCode: {response.ErrCode}\nErrorMessage: {response.ErrMessage}");
            }
        }
    }
}
