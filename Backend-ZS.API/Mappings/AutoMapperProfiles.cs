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

            // TransactionItem Mappings
            CreateMap<TransactionItem, TransactionItemDto>();

            // Transaction Mappings (✅ incluir Keys y evitar sorpresas por convención)
            CreateMap<Transaction, TransactionDto>()
                .ForMember(d => d.Keys, opt => opt.MapFrom(s => s.Keys));
            CreateMap<TransactionRequestDto, Transaction>();

            // Payment Mappings
            CreateMap<Payment, PaymentDto>();
            CreateMap<PaymentRequestDto, Payment>();

            // BarOrder Mappings
            CreateMap<BarOrder, BarOrderDto>();
            CreateMap<BarOrderRequestDto, BarOrder>()
                .ForMember(d => d.TransactionType,
                    opt => opt.MapFrom(_ => "BarOrder"));

            // BarOrderDetail Mappings
            CreateMap<BarOrderDetail, BarOrderDetailDto>();
            CreateMap<BarOrderDetailCreateRequestDto, BarOrderDetail>();
            CreateMap<BarOrderDetailUpdateRequestDto, BarOrderDetail>();

            CreateMap<Key, KeyDto>();

            // Al mapear request -> domain:
            // - NO tocar Id ni KeyCode
            // - NO mapear nav prop Transaction (solo FK TransactionId)
            CreateMap<KeyRequestDto, Key>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.KeyCode, opt => opt.Ignore())
                .ForMember(d => d.Transaction, opt => opt.Ignore());

            // AccessCard Mappings
            CreateMap<AccessCard, AccessCardDto>();

            CreateMap<AccessCardRequestDto, AccessCard>()
                .ForMember(d => d.TransactionId,
                    opt => opt.MapFrom(s => s.TransactionId == Guid.Empty ? null : s.TransactionId))
                .ForMember(d => d.TransactionType,
                    opt => opt.MapFrom(_ => "AccessCard"));

            // Parking Mappings
            CreateMap<Parking, ParkingDto>();
            CreateMap<ParkingRequestDto, Parking>()
                .ForMember(d => d.TransactionType,
                    opt => opt.MapFrom(_ => "Parking"));

            // EntranceTransaction Mappings
            CreateMap<EntranceTransaction, EntranceTransactionDto>();
            CreateMap<EntranceTransactionRequestDto, EntranceTransaction>()
                .ForMember(d => d.TransactionType,
                    opt => opt.MapFrom(_ => "EntranceTransaction"));

            // EntranceAccessCard Mappings
            CreateMap<EntranceAccessCard, EntranceAccessCardDto>();
            CreateMap<EntranceAccessCardRequestDto, EntranceAccessCard>();

            // CashBox
            CreateMap<CashBox, CashBoxDto>();
        }
    }
}
