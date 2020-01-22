using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

using PaymentApi.Models; 
using PaymentApi.Messages;
using PaymentApi.Services;
using PaymentApi.Exceptions;


namespace PaymentApi.Controllers {     
[Route("api/[controller]")]     
[ApiController]     
public class PaymentController : ControllerBase     
{        
private readonly PaymentService _paymentService;

public PaymentController(PaymentService paymentService, 
IOptions<MyAppSettings> myAppSettings, IOptionsMonitor<MyOptions> optionsAccessor)         
{
    _paymentService = paymentService;
}

 [HttpPost]
// public async Task<ActionResult<Payment>> PostPayment(PaymentRequest paymentRequest){
    //ERRORS as return values style
            
//              PaymentResult paymentResult =  await _paymentService.MakePayment(paymentRequest); 

//             if(paymentResult.Result == PaymentResult.ResultCodes.Success){
//                 return CreatedAtAction(nameof(GetById), new { id = paymentResult.PaymentRecord .Id }, paymentResult.PaymentRecord);
//             }
//             if(paymentResult.Result==PaymentResult.ResultCodes.InvalidInput){
//                 return BadRequest(paymentResult.Status); 
//             }
//             if(paymentResult.Result==PaymentResult.ResultCodes.TransactionError){
//                 return StatusCode(StatusCodes.Status424FailedDependency, paymentResult.Status);
//             }
//             return StatusCode(StatusCodes.Status500InternalServerError);
// }  

public async Task<ActionResult<Payment>> PostPayment(PaymentRequest paymentRequest){
            //ERRORS as exceptions style - make the calls and catch specific exceptions to process
    try{
    Payment payment =  await _paymentService.DoPayment(paymentRequest); 
    return CreatedAtAction(nameof(GetById), new { id = payment.Id }, payment);
    }
    catch(ValidationException vex){
        //return 400
        return BadRequest(vex.Message + " - " + vex.Details); 
    }
    catch(DependencyException dex){
        return StatusCode(StatusCodes.Status422UnprocessableEntity, dex.Message + "-" + JsonSerializer.Serialize(dex.Details));
    }
    catch(ProcessingException pex){
        return StatusCode(StatusCodes.Status422UnprocessableEntity, pex.Message + "-" + JsonSerializer.Serialize(pex.Details));
    }
    catch(Exception ex){
        return StatusCode(StatusCodes.Status500InternalServerError, JsonSerializer.Serialize(ex));
    }
            
}  

[HttpGet] 
public ActionResult<List<Payment>> GetAll() 
{     
    return _paymentService.GetAll(); 
} 
 
[HttpGet("{id}", Name = "GetPayment")] 
public ActionResult<Payment> GetById(long id) 
{    
    var item = _paymentService.GetById(id);    
    if (item == null)    
    {         
    return NotFound();     
    }     
    return item; 
}  

}
}