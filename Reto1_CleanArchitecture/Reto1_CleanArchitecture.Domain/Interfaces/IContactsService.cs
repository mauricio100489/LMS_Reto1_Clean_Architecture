using Reto1_CleanArchitecture.Domain.Models;

namespace Reto1_CleanArchitecture.Domain.Interfaces
{
    public interface IContactsService
    {
        Task<IEnumerable<Contacts>> GetAllContactsAsync();
        Task<Contacts> GetContactById(long id);
        Task<Contacts> AddContactAsync(Contacts contact);
        Task<Contacts> UpdateContact(Contacts contact);
        Task<Contacts> DeleteContact(Contacts contact);
    }
}
