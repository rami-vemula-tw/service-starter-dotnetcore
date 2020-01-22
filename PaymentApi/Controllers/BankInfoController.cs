using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using PaymentApi.Models; 

using PaymentApi.Services;

namespace PaymentApi.Controllers {     
[Route("api/[controller]")]     
[ApiController]     
public class BankInfoController : ControllerBase     
{        
private readonly BankInfoService _bankInfoService;

public BankInfoController(BankInfoService bankInfoService, 
IOptions<MyAppSettings> myAppSettings, IOptionsMonitor<MyOptions> optionsAccessor)         
{
    _bankInfoService = bankInfoService;
}

 [HttpPost]
public async Task<ActionResult<BankInfo>> PostBankInfo(BankInfo bankInfo){
            
    BankInfo newBankInfo =  await _bankInfoService.CreateBankInfo(bankInfo); 
    if(newBankInfo==null){
        return BadRequest("{One or more Accounts not valid}"); 
    } 
    return CreatedAtAction(nameof(GetById), new { id = newBankInfo.BankCode }, newBankInfo);

}  

[HttpGet] 
public ActionResult<List<BankInfo>> GetAll() 
{     
    return _bankInfoService.GetAll(); 
} 
 
[HttpGet("{id}", Name = "GetBankInfo")] 
public ActionResult<BankInfo> GetById(string id) 
{    
    var item = _bankInfoService.GetById(id);    
    if (item == null)    
    {         
    return NotFound();     
    }     
    return item; 
}  
}

}