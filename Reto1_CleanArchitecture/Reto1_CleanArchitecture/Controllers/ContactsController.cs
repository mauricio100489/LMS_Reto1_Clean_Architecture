using Microsoft.AspNetCore.Mvc;
using Reto1_CleanArchitecture.Application.DTOs;
using Reto1_CleanArchitecture.Domain.Interfaces;
using Reto1_CleanArchitecture.Domain.Models;

namespace Reto1_CleanArchitecture.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController(IContactsService contactsService) : ControllerBase
    {
        private readonly IContactsService _contactsService = contactsService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var contacts = await _contactsService.GetAllContactsAsync();

            return Ok(contacts);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddContactsDto contact)
        {
            if (contact == null)
                return BadRequest("La informacion del contacto es requerida!");

            Contacts newContact = new()
            {
                Name = contact.Name,
                Phone = contact.Phone,
                Email = contact.Email,
                Company = contact.Company,
                Address = contact.Address,
                Notes = contact.Notes,
                Status = 1,
                CreatedBy = contact.CreatedBy,
                CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            _ = await _contactsService.AddContactAsync(newContact);

            return Ok(newContact);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateContactDto contact)
        {
            if (contact == null)
                return BadRequest("La informacion del contacto es requerida!");
            
            var infoContact = await _contactsService.GetContactById(contact.ContactId);
            
            if (infoContact == null)
                return NotFound($"El contacto con Id {contact.ContactId} no fue encontrado!");
            
            infoContact.Name = contact.Name;
            infoContact.Phone = contact.Phone;
            infoContact.Email = contact.Email;
            infoContact.Company = contact.Company;
            infoContact.Address = contact.Address;
            infoContact.Notes = contact.Notes;
            infoContact.UpdatedBy = contact.UpdatedBy;
            infoContact.UpdatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            await _contactsService.UpdateContact(infoContact);
            return Ok(infoContact);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteContactDto contact)
        {
            if (contact == null)
                return BadRequest("La informacion del contacto es requerida!");

            var infoContact = await _contactsService.GetContactById(contact.ContactId);

            if (infoContact == null)
                return NotFound($"El contacto con Id {contact.ContactId} no fue encontrado!");

            infoContact.Status = 0;
            infoContact.StatusChangedBy = contact.StatusChangedBy;
            infoContact.StatusChangedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            await _contactsService.DeleteContact(infoContact);

            return Ok(infoContact);
        }
    }
}
