using Microsoft.EntityFrameworkCore;
using Reto1_CleanArchitecture.Domain.Interfaces;
using Reto1_CleanArchitecture.Domain.Models;
using Reto1_CleanArchitecture.Infrastructure.Repository;

namespace Reto1_CleanArchitecture.Application.Services
{
    public class ContactsService : IContactsService
    {
        private readonly IRepositoryGeneric<Contacts> _contactsRepository;

        public ContactsService(IRepositoryGeneric<Contacts> _contactsRepository)
        {
            this._contactsRepository = _contactsRepository;
        }

        public async Task<Contacts> AddContactAsync(Contacts contact)
        {
            try
            {
                _contactsRepository.Create(contact);
                await _contactsRepository.SaveChangesAsync();

                return contact;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el contacto", ex);
            }
        }

        public async Task<Contacts> DeleteContact(Contacts contact)
        {
            try
            {
                _contactsRepository.Update(contact);
                await _contactsRepository.SaveChangesAsync();

                return contact;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el contacto", ex);
            }
        }

        public async Task<IEnumerable<Contacts>> GetAllContactsAsync()
        {
            try
            {
                return await _contactsRepository
                                .All()
                                .Where(c => c.Status == 1)
                                .ToListAsync();
            }
            catch(Exception ex)
            {
                throw new Exception("Error al obtener los contactos", ex);
            }
        }

        public async Task<Contacts?> GetContactById(long id)
        {
            try
            {
                return await _contactsRepository
                             .All()
                             .Where(c => c.ContactId == id && c.Status == 1)
                             .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw new Exception($"Error al buscar el contacto id: {id}", ex);
            }
        }

        public async Task<Contacts> UpdateContact(Contacts contact)
        {
            try
            {
                _contactsRepository.Update(contact);
                await _contactsRepository.SaveChangesAsync();

                return contact;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualiza la info del contacto", ex);
            }
        }
    }
}
