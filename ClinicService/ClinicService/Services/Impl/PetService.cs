using Grpc.Core;
using ClinicService.Data;
using static ClinicServiceNamespace.PetService;
using ClinicServiceNamespace;
using Google.Protobuf.WellKnownTypes;

namespace PetService.Services.Impl
{
    public class PetService : PetServiceBase
    {

        private readonly ClinicServiceDbContext _dbContext;

        public PetService(ClinicServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override Task<CreatePetResponse> CreatePet(CreatePetRequest request, ServerCallContext context)
        {
            try
            {
                var pet = new ClinicService.Data.Pet
                {
                    ClientId = request.ClientId,
                    Name = request.Name,
                    Birthday = request.Birthday.ToDateTime()
                };
                _dbContext.Pets.Add(pet);
                _dbContext.SaveChanges();

                var response = new CreatePetResponse
                {
                    PetId = pet.Id,
                    ErrCode = 0,
                    ErrMessage = ""
                };

                return Task.FromResult(response);
            }
            catch(Exception e)
            {
                var response = new CreatePetResponse
                {
                    ErrCode = 1001,
                    ErrMessage = "Internal server error."
                };

                return Task.FromResult(response);
            }
        }

        public override Task<GetPetsResponse> GetPets(GetPetsRequest request, ServerCallContext context)
        {
            try
            {
                var response = new GetPetsResponse();
                var pets = _dbContext.Pets.Select(pet => new ClinicServiceNamespace.Pet
                {
                    PetId = pet.Id,
                    ClientId = pet.ClientId,
                    Name = pet.Name,
                    Birthday = pet.Birthday.ToTimestamp()
                }).ToList();
                response.Pets.AddRange(pets);
                return Task.FromResult(response);
            }
            catch(Exception e)
            {
                var response = new GetPetsResponse
                {
                    ErrCode = 1002,
                    ErrMessage = "Internal server error."
                };

                return Task.FromResult(response);
            }
        }

    }
}
