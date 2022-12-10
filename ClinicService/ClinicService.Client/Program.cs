using ClinicService.Client.Clients;
using ClinicService.Client.Clients.Impl;
using ClinicServiceNamespace;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using static ClinicServiceNamespace.ClinicService;
using static ClinicServiceNamespace.ConsultationService;
using static ClinicServiceNamespace.PetService;

AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

using var channel = GrpcChannel.ForAddress("http://localhost:5001");
ClinicServiceClient clinicServiceClient = new (channel);
PetServiceClient petServiceClient = new (channel);
ConsultationServiceClient consultationServiceClient = new (channel);

IProtobufClient<CreateClientRequest> clientPBClient =
    new ClientPBClient(clinicServiceClient);
IProtobufClient<CreatePetRequest> petPBClient =
    new PetPBClient(petServiceClient);
IProtobufClient<CreateConsultationRequest> consultationPBClient =
    new ConsultationPBClient(consultationServiceClient);

for (int i = 0; i < 10; i++)
{
    var client = new CreateClientRequest
    {
        Document = Guid.NewGuid().ToString(),
        Surname = "Шабанов",
        FirstName = "Данил",
        Patronymic = "Валерьевич"
    };
    clientPBClient.Create(client);
}
clientPBClient.GetAll();

for (int i = 0; i < 10; i++)
{
    var pet = new CreatePetRequest
    {
        ClientId = i + 1000,
        Name = "Кот Барсик",
        Birthday = DateTime.UtcNow.ToTimestamp()
    };
    petPBClient.Create(pet);
}
petPBClient.GetAll();

for (int i = 0; i < 10; i++)
{
    var consultation = new CreateConsultationRequest
    {
        ClientId = i + 1000,
        PetId = i + 10,
        ConsultationDate = DateTime.UtcNow.AddDays(10).ToTimestamp(),
        Description = "Первичный осмотр"
    };
    consultationPBClient.Create(consultation);
}
consultationPBClient.GetAll();

Console.ReadKey();