using ClinicService.Client.Clients;
using ClinicService.Client.Clients.Impl;
using ClinicServiceNamespace;
using ClinicServiceProtos;
using Grpc.Core;
using Grpc.Net.Client;
using static ClinicServiceNamespace.ClinicService;
using static ClinicServiceProtos.AuthenticateService;

using var channel = GrpcChannel.ForAddress("https://localhost:5101");

AuthenticateServiceClient authenticateServiceClient = new AuthenticateServiceClient(channel);

var authenticationResponse = authenticateServiceClient.Login(new AuthenticationRequest
{
    UserName = "shabanov1903@gmail.com",
    Password = "admin"
});

if (authenticationResponse.Status != 0)
{
    Console.WriteLine("Authentication error.");
    Console.ReadKey();
    return;
}

Console.WriteLine($"Session token: {authenticationResponse.SessionContext.SessionToken}");

var callCredentials = CallCredentials.FromInterceptor((c, m) =>
{
    m.Add("Authorization",
        $"Bearer {authenticationResponse.SessionContext.SessionToken}");
    return Task.CompletedTask;
});

var protectedChannel = GrpcChannel.ForAddress("https://localhost:5101", new GrpcChannelOptions
{
    Credentials = ChannelCredentials.Create(new SslCredentials(), callCredentials)
});

ClinicServiceClient clinicServiceClient = new(protectedChannel);

IProtobufClient<CreateClientRequest> clientPBClient =
    new ClientPBClient(clinicServiceClient);

clientPBClient.GetAll();

Console.ReadKey();
