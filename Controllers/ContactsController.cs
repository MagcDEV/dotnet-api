using dotnet_api.Data;
using dotnet_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactsController : Controller
{
    private readonly ContactsAPIDbContext _contactsAPIDbContext;

    public ContactsController(ContactsAPIDbContext contactsAPIDbContext)
    {
        _contactsAPIDbContext = contactsAPIDbContext;
    }
    [HttpGet]
    public IActionResult GetContacts()
    {
        return Ok(_contactsAPIDbContext.Contacts.ToList());
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetContact(Guid id)
    {
        var contact = _contactsAPIDbContext.Contacts.FirstOrDefault(c => c.Id == id);
        if (contact == null)
        {
            return NotFound();
        }
        return Ok(contact);
    }

    [HttpPost]
    public IActionResult AddContact(AddContactRequest newContact)
    {
        var contact = new Contact()
        {
            Id = Guid.NewGuid(),
            FullName = newContact.FullName,
            Email = newContact.Email,
            Phone = newContact.Phone,
            Address = newContact.Address,
        };

        _contactsAPIDbContext.Contacts.Add(contact);
        _contactsAPIDbContext.SaveChanges();
        return Ok(contact);
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult UpdateContact(AddContactRequest updateContact, Guid id)
    {
        var contact = _contactsAPIDbContext.Contacts.FirstOrDefault(c => c.Id == id);
        if (contact == null)
        {
            return NotFound();
        }

        contact.FullName = updateContact.FullName;
        contact.Email = updateContact.Email;
        contact.Phone = updateContact.Phone;
        contact.Address = updateContact.Address;

        _contactsAPIDbContext.Contacts.Update(contact);
        _contactsAPIDbContext.SaveChanges();

        return Ok(contact);
    }

    [HttpDelete]
    public IActionResult DeleteContact(Guid id)
    {
        var contact = _contactsAPIDbContext.Contacts.FirstOrDefault(c => c.Id == id);
        if (contact == null)
        {
            return NotFound();
        }

        _contactsAPIDbContext.Contacts.Remove(contact);
        _contactsAPIDbContext.SaveChanges();

        return Ok();
    }

}

