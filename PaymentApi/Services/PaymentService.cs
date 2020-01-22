using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using PaymentApi.Messages;
using PaymentApi.Models;
using PaymentApi.Adapters;
using PaymentApi.Exceptions; 



namespace PaymentApi.Services{

public class PaymentService{

private readonly PaymentContext _context;   
private readonly BankClient _bankClient;

public PaymentService(PaymentContext paymentContext, BankClient bankClient){
    _context = paymentContext;
    _bankClient = bankClient;

    //Temporary code to be removed - adding a dummy entry to database just to check the APIs starting with GET API
    // if (_context.Payments.Count() == 0)             
    // {                 
    //     _context.Payments.Add(new Payment(  "DummyBeneficiary","AC001","CITY001"
    //                     ,"DummyPayee","BC010", "STD002", 10000, PaymentStatus.InProgress));                 
    //     _context.SaveChanges();             
    // }    
    //END temporary code
}

//Following 2 functions - DoPayment and MakePayment do the same functionality - in 2 different styles
public async Task<Payment> DoPayment(PaymentRequest paymentRequest){
    //style -> RETURN errors as exceptions

    //validate the accounts 
    //these calls themselves throw exceptions if the accounts are not valid
    await _bankClient.ValidateAccount(paymentRequest.payee.accountNumber, paymentRequest.payee.ifscCode);
    await _bankClient.ValidateAccount(paymentRequest.beneficiary.accountNumber, paymentRequest.beneficiary.ifscCode);

    //a hard coded sample check representing a biz logic
    if(paymentRequest.amount > 1000000){
     throw new ProcessingException("Amount exceeded permitted limit", null, paymentRequest);
    }
    
     //create payment entry with initial status "InProgress"
    Payment payment = new Payment(paymentRequest.beneficiary.name, paymentRequest.beneficiary.accountNumber, paymentRequest.beneficiary.ifscCode,
                paymentRequest.payee.name, paymentRequest.payee.accountNumber, paymentRequest.payee.ifscCode, paymentRequest.amount, PaymentStatus.InProgress);
                _context.Payments.Add(payment); 
                await _context.SaveChangesAsync();

    //TODO - do the transaction {
    //1. debit payee
   
    //2. credit beneficiary
    
    //3. update status in database
    payment._status = PaymentStatus.Completed;
    _context.Payments.Update(payment); 
    await _context.SaveChangesAsync();
    //END TODO - do the  transaction
    //}

    return payment;
}
public async Task<PaymentResult> MakePayment(PaymentRequest paymentRequest){
//style -> RETURN errors as part of return value
    PaymentResult result = new PaymentResult();
    
    Payment payment = null;
    //validate the accounts first
    //these calls returning bool on validation
    bool isValidAccount = await _bankClient.CheckAccount(paymentRequest.payee.accountNumber, paymentRequest.payee.ifscCode); 
    if(!isValidAccount){
        result.SetResult(PaymentResult.ResultCodes.InvalidInput, PaymentResult.StatusCodes.InvalidPayeeAccount, null);
        return result;
    }
    isValidAccount = await _bankClient.CheckAccount(paymentRequest.beneficiary.accountNumber, paymentRequest.beneficiary.ifscCode);
    if(!isValidAccount){
        result.SetResult(PaymentResult.ResultCodes.InvalidInput, PaymentResult.StatusCodes.InvalidBeneficieryAccount, null);
        return result;
    }
   
   //a hard coded sample check representing a biz logic
    if(paymentRequest.amount > 1000000){
        result.SetResult(PaymentResult.ResultCodes.TransactionError, PaymentResult.StatusCodes.AmountExceedLimit,null);
        return result;
    }

    //create payment entry with initial status "InProgress" 
    payment = new Payment(paymentRequest.beneficiary.name, paymentRequest.beneficiary.accountNumber, paymentRequest.beneficiary.ifscCode,
                paymentRequest.payee.name, paymentRequest.payee.accountNumber, paymentRequest.payee.ifscCode, paymentRequest.amount, PaymentStatus.InProgress);
        
                _context.Payments.Add(payment); 
                await _context.SaveChangesAsync();
    result.SetResult(PaymentResult.ResultCodes.InProgress, PaymentResult.StatusCodes.TransactionInitiated, payment);
    //do the transaction
    //debit payee
    result.SetResult(PaymentResult.ResultCodes.InProgress, PaymentResult.StatusCodes.PayeeDebited, payment);
    //credit beneficiary
    result.SetResult(PaymentResult.ResultCodes.InProgress, PaymentResult.StatusCodes.BeneficiaryCredited, payment);
        
    //update status in database
    payment._status = PaymentStatus.Completed;
    _context.Payments.Update(payment); 
    await _context.SaveChangesAsync();
    result.SetResult(PaymentResult.ResultCodes.Success, PaymentResult.StatusCodes.TransactionInitiated, payment);
  
    //return the payment result
    return result;
}

public List<Payment> GetAll() 
{     
    return _context.Payments.ToList(); 
}  

public Payment GetById(long id) 
{    
    return _context.Payments.Find(id);     
}  

}

}