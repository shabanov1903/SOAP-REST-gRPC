using ClinicServiceNamespace;
using static ClinicServiceNamespace.PetService;

namespace ClinicService.Client.Clients.Impl
{
    internal class PetPBClient : IProtobufClient<CreatePetRequest>
    {
        private readonly PetServiceClient _client;
        public PetPBClient(PetServiceClient client) {
            _client = client;
        }

        public void Create(CreatePetRequest item)
        {
            var response = _client.CreatePet(item);

            if (response.ErrCode == 0)
            {
                Console.WriteLine($"Pet #{response.PetId} created successfully.");
            }
            else
            {
                Console.WriteLine($"Create pet error.\nErrorCode: {response.ErrCode}\nErrorMessage: {response.ErrMessage}");
            }
        }

        public void GetAll()
        {
            var response = _client.GetPets(new GetPetsRequest());

            if (response.ErrCode == 0)
            {
                Console.WriteLine($"Objects of type: {typeof(GetPetsRequest)}");

                foreach (var pet in response.Pets)
                {
                    Console.WriteLine($"#{pet.PetId} {pet.ClientId} {pet.Name} {pet.Birthday}");
                }
            }
            else
            {
                Console.WriteLine($"Get pets error.\nErrorCode: {response.ErrCode}\nErrorMessage: {response.ErrMessage}");
            }
        }
    }
}
