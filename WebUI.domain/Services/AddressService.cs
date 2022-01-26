using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Interfaces.Repositories;
using OnlineBanking.Domain.UnitOfWork;
using WebUI.domain.Interfaces.Services;
using WebUI.domain.Models;

namespace WebUI.domain.Services
{
    public class AddressService : IAddressService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAddressRepository _addressRepo;

        public AddressService(IUnitOfWork unitOfWork, IAddressRepository addressRepo)
        {
            _unitOfWork = unitOfWork;
            _addressRepo = addressRepo;
        }
        public void Add(AddressViewModel model)
        {
            var address = new Address
            {
                PlotNo = model.PlotNo,
                StreetName = model.StreetName,
                City = model.City,
                State = model.State,
                Nationality = model.Nationality,
            };
            _addressRepo.Add(address);
            _unitOfWork.Commit();
        }
    }
}
