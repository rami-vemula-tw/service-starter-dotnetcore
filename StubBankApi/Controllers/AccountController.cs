using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using StubBankApi.Models; 

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace StubBankApi.Controllers {     
[Route("api/[controller]")]     
[ApiController]     
public class AccountController : ControllerBase     
{        
private readonly BankContext _context;   


public AccountController(BankContext context)         
{             _context = context; 
 if (_context.Accounts.Count() == 0)             
    {                 
        _context.Accounts.Add(new Account("AC001", "CITY002" ));                 
        _context.SaveChanges();             
    }                      
}

 [HttpPost]
public async Task<ActionResult<Account>> PostAccount(Account account)
{
    
            _context.Accounts.Add(account); 
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = account.AccountNumber }, account);
}  

[HttpGet] 
public ActionResult<List<Account>> GetAll() 
{     
    return _context.Accounts.ToList(); 
} 
 
[HttpGet("{id}", Name = "GetPayment")] 
public ActionResult<Account> GetById(string id) 
{    
    var item = _context.Accounts.Find(id);     
    if (item == null)    
    {         
    return NotFound();     
    }     
    return item; 
}  

}
}