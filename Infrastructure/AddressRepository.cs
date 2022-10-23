using System;
using Core.Domain;
using Core.DomainServices;

namespace Infrastructure
{
    public class AddressRepository: IAddressRepository
    {
        private BoardgamesContext _context;

        public AddressRepository(BoardgamesContext context)
        {
            this._context = context;
        }

        public async Task CreateAddress(Address address)
        {
            _context.Add(address);
            await _context.SaveChangesAsync();
        }
    }
}

