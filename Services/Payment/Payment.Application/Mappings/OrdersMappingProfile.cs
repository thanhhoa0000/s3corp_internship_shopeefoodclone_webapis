namespace ShopeeFoodClone.WebApi.Payment.Application.Mappings;

public class PaymentMappingProfile : Profile
{
    public PaymentMappingProfile()
    {
        CreateMap<Transaction, TransactionDto>().ReverseMap();
    }    
}
