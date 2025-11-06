using AutoMapper;
using Backend_ZS.API.Models.Domain;
using Backend_ZS.API.Models.DTO;

namespace Backend_ZS.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Client Mappings
            CreateMap<Client, ClientDto>();
            CreateMap<ClientRequestDto, Client>();

            // BarProduct Mappings
            CreateMap<BarProduct, BarProductDto>();
            CreateMap<BarProductRequestDto, BarProduct>();

            // Transaction Mappings
            CreateMap<Transaction, TransactionDto>();
            CreateMap<TransactionRequestDto, Transaction>();

            // Payment Mappings
            CreateMap<Payment, PaymentDto>();
            CreateMap<PaymentRequestDto, Payment>();

            // BarOrder Mappings
            CreateMap<BarOrder, BarOrderDto>();
            CreateMap<BarOrderRequestDto, BarOrder>();

            // BarOrderDetail Mappings
            CreateMap<BarOrderDetail, BarOrderDetailDto>();
            CreateMap<BarOrderDetailCreateRequestDto, BarOrderDetail>();
            CreateMap<BarOrderDetailUpdateRequestDto, BarOrderDetail>();
        }
    }
}
