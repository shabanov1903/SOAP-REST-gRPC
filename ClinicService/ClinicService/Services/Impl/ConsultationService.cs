using Grpc.Core;
using ClinicService.Data;
using static ClinicServiceNamespace.ConsultationService;
using ClinicServiceNamespace;
using Google.Protobuf.WellKnownTypes;

namespace ConsultationService.Services.Impl
{
    public class ConsultationService : ConsultationServiceBase
    {

        private readonly ClinicServiceDbContext _dbContext;

        public ConsultationService(ClinicServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override Task<CreateConsultationResponse> CreateConsultation(CreateConsultationRequest request, ServerCallContext context)
        {
            try
            {
                var consultation = new ClinicService.Data.Consultation
                {
                    ClientId = request.ClientId,
                    PetId = request.PetId,
                    ConsultationDate = request.ConsultationDate.ToDateTime(),
                    Description= request.Description
                };
                _dbContext.Consultations.Add(consultation);
                _dbContext.SaveChanges();

                var response = new CreateConsultationResponse
                {
                    ConsultationId = consultation.Id,
                    ErrCode = 0,
                    ErrMessage = ""
                };

                return Task.FromResult(response);
            }
            catch(Exception e)
            {
                var response = new CreateConsultationResponse
                {
                    ErrCode = 1001,
                    ErrMessage = "Internal server error."
                };

                return Task.FromResult(response);
            }
        }

        public override Task<GetConsultationsResponse> GetConsultations(GetConsultationsRequest request, ServerCallContext context)
        {
            try
            {
                var response = new GetConsultationsResponse();

                var consultations = _dbContext.Consultations.Select(consultation => new ClinicServiceNamespace.Consultation
                {
                    ConsultationId = consultation.Id,
                    ClientId = consultation.ClientId,
                    PetId = consultation.PetId,
                    ConsultationDate = consultation.ConsultationDate.ToTimestamp(),
                    Description = consultation.Description
                }).ToList();
                response.Consultations.AddRange(consultations);
                return Task.FromResult(response);
            }
            catch(Exception e)
            {
                var response = new GetConsultationsResponse
                {
                    ErrCode = 1002,
                    ErrMessage = "Internal server error."
                };

                return Task.FromResult(response);
            }
        }

    }
}
