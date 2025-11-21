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

            //TransactionItem Mappings
            CreateMap<TransactionItem, TransactionItemDto>();

            // Transaction Mappings
            CreateMap<Transaction, TransactionDto>();
            CreateMap<TransactionRequestDto, Transaction>();

            // Payment Mappings
            CreateMap<Payment, PaymentDto>();
            CreateMap<PaymentRequestDto, Payment>();

            // BarOrder Mappings
            CreateMap<BarOrder, BarOrderDto>();

            // BarOrderDetail Mappings
            CreateMap<BarOrderDetail, BarOrderDetailDto>();
            CreateMap<BarOrderDetailCreateRequestDto, BarOrderDetail>();
            CreateMap<BarOrderDetailUpdateRequestDto, BarOrderDetail>();

            // Key Mappings
            CreateMap<Key, KeyDto>();
            CreateMap<KeyRequestDto, Key>();

            // AccessCard Mappings
            CreateMap<AccessCard, AccessCardDto>();
            CreateMap<AccessCardRequestDto, AccessCard>();

            // Parking Mappings
            CreateMap<Parking, ParkingDto>();
            CreateMap<ParkingRequestDto, Parking>();

            // EntranceTransaction Mappings
            CreateMap<EntranceTransaction, EntranceTransactionDto>();
            CreateMap<EntranceTransactionRequestDto, EntranceTransaction>();

            // EntrnaceAccessCard Mapppings
            CreateMap<EntranceAccessCard, EntranceAccessCardDto>();
            CreateMap<EntranceAccessCardRequestDto, EntranceAccessCard>();
        }
    }
}
