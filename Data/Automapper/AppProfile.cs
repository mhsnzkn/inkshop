using AutoMapper;
using Data.Constants;
using Data.Dtos;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Automapper
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<OrderAddDto, Order>();
            CreateMap<Order, OrderAddDto>()
                .ForMember(a => a.TypeCoverUp, s => s.MapFrom(o => o.Type.Contains(OrderTypeString.CoverUp)))
                .ForMember(a => a.TypeFreeHand, s => s.MapFrom(o => o.Type.Contains(OrderTypeString.Freehand)))
                .ForMember(a => a.TypeRefresh, s => s.MapFrom(o => o.Type.Contains(OrderTypeString.Refresh)))
                .ForMember(a => a.TypeTouchUp, s => s.MapFrom(o => o.Type.Contains(OrderTypeString.TouchUp)));

            CreateMap<Order, TransferTableDto>()
                .ForMember(a => a.OfficeName, s => s.MapFrom(o => o.Office.Name))
                .ForMember(a => a.CustomerCountryName, s => s.MapFrom(o => o.CustomerCountry.Name))
                .ForMember(a => a.CustomerFullName, s => s.MapFrom(o => o.CustomerName + " " + o.CustomerSurname))
                .ForMember(a => a.CustomerAddress, s => s.MapFrom(o => o.CustomerHotel+ " - " + o.CustomerRoomNumber))                ;

            CreateMap<Order, OrderTableDto>()
                .ForMember(a => a.OfficeName, s => s.MapFrom(o => o.Office.Name))
                .ForMember(a => a.CurrencyName, s => s.MapFrom(o => o.Currency.ShortName))
                .ForMember(a => a.CustomerCountryName, s => s.MapFrom(o => o.CustomerCountry.Name))
                .ForMember(a => a.CustomerFullName, s => s.MapFrom(o => o.CustomerName + " " + o.CustomerSurname))
                .ForMember(a => a.OrderTypeName, s => s.MapFrom(o => o.OrderType.Name));

            CreateMap<Order, ReservationTableDto>()
                .ForMember(a => a.OfficeName, s => s.MapFrom(o => o.Office.Name))
                .ForMember(a => a.CurrencyName, s => s.MapFrom(o => o.Currency.ShortName))
                .ForMember(a => a.CustomerCountryName, s => s.MapFrom(o => o.CustomerCountry.Name))
                .ForMember(a => a.CustomerFullName, s => s.MapFrom(o => o.CustomerName + " " + o.CustomerSurname))
                .ForMember(a => a.OrderTypeName, s => s.MapFrom(o => o.OrderType.Name))
                .ForMember(a => a.PersonnelName, s => s.MapFrom(o => 
                    o.OrderPersonnel.Where(d => d.Job == OrderPersonnelJob.Artist).FirstOrDefault().Personnel.Name +" "+ 
                    o.OrderPersonnel.Where(d => d.Job == OrderPersonnelJob.Artist).FirstOrDefault().Personnel.Surname));

            CreateMap<Order, VaultIncomeDto>()
                .ForMember(a => a.OfficeName, s => s.MapFrom(o => o.Office.Name))
                .ForMember(a => a.CurrencyName, s => s.MapFrom(o => o.Currency.ShortName))
                .ForMember(a => a.CustomerFullName, s => s.MapFrom(o => o.CustomerName + " " + o.CustomerSurname))
                .ForMember(a => a.Price, s => s.MapFrom(o => o.IsPaymentDone ? o.Price - o.Deposit : o.Deposit))
                .ForMember(a => a.Description, s => s.MapFrom(o => o.IsPaymentDone ? "Rest" : "Depozito"));

            CreateMap<OrderPersonnel, VaultExpenseDto>()
                .ForMember(a => a.OfficeName, s => s.MapFrom(o => o.Order.Office.Name))
                .ForMember(a => a.CurrencyName, s => s.MapFrom(o => o.Order.Currency.ShortName))
                .ForMember(a => a.OrderId, s => s.MapFrom(o => o.Order.Id))
                .ForMember(a => a.PersonnelFullName, s => s.MapFrom(o => o.Personnel.Name + " " + o.Personnel.Surname))
                .ForMember(a => a.PersonnelJob, s => s.MapFrom(o => o.Personnel.job))
                .ForMember(a => a.ApproveDate, s => s.MapFrom(o => o.Order.ApproveDate))
                .ForMember(a => a.IsCreditCard, s => s.MapFrom(o => o.Order.IsCreditCard))
                .ForMember(a => a.IsPaymentDone, s => s.MapFrom(o => o.Order.IsPaymentDone));

            CreateMap<ReservationDto, Order>();
            CreateMap<Order, ReservationDto>()
                .ForMember(a => a.ArtistId, s => s.MapFrom(o => o.OrderPersonnel.Where(d => d.Job == OrderPersonnelJob.Artist).FirstOrDefault().PersonnelId))
                .ForMember(a => a.InfoMenId, s => s.MapFrom(o => o.OrderPersonnel.Where(d => d.Job == OrderPersonnelJob.Info).FirstOrDefault().PersonnelId))
                .ForMember(a => a.MiddleMenId, s => s.MapFrom(o => o.OrderPersonnel.Where(d => d.Job == OrderPersonnelJob.Hanut).FirstOrDefault().PersonnelId));

            CreateMap<TransferDto, Order>().ReverseMap();

            CreateMap<ApplicationUser, UserTableDto>()
                .ForMember(a => a.PersonnelName, o => o.MapFrom(a => a.Personnel.Name + " " + a.Personnel.Surname));

            CreateMap<ApplicationUser, UserEditDto>()
                .ForMember(a => a.Password, o => o.Ignore())
                .ForMember(a => a.ConfirmPassword, o => o.Ignore());

            CreateMap<Personnel, PersonnelTableDto>()
                .ForMember(a => a.CategoryName, o => o.MapFrom(s => s.Category.ToString()));
        }
    }
}
