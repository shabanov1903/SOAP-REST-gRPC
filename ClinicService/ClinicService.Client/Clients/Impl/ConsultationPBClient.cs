using ClinicServiceNamespace;
using static ClinicServiceNamespace.ConsultationService;

namespace ClinicService.Client.Clients.Impl
{
    internal class ConsultationPBClient : IProtobufClient<CreateConsultationRequest>
    {
        private readonly ConsultationServiceClient _client;
        public ConsultationPBClient(ConsultationServiceClient client) {
            _client = client;
        }

        public void Create(CreateConsultationRequest item)
        {
            var response = _client.CreateConsultation(item);

            if (response.ErrCode == 0)
            {
                Console.WriteLine($"Consultation #{response.ConsultationId} created successfully.");
            }
            else
            {
                Console.WriteLine($"Create consultation error.\nErrorCode: {response.ErrCode}\nErrorMessage: {response.ErrMessage}");
            }
        }

        public void GetAll()
        {
            var response = _client.GetConsultations(new GetConsultationsRequest());

            if (response.ErrCode == 0)
            {
                Console.WriteLine($"Objects of type: {typeof(GetConsultationsRequest)}");

                foreach (var consultation in response.Consultations)
                {
                    Console.WriteLine($"#{consultation.ConsultationId} {consultation.ClientId} {consultation.PetId} {consultation.Description} {consultation.ConsultationDate}");
                }
            }
            else
            {
                Console.WriteLine($"Get consultations error.\nErrorCode: {response.ErrCode}\nErrorMessage: {response.ErrMessage}");
            }
        }
    }
}
