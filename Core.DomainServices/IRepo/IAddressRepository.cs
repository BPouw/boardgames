using System;
using Core.Domain;
namespace Core.DomainServices
{
    public interface IAddressRepository
    {
        Task CreateAddress(Address address);
    }
}

