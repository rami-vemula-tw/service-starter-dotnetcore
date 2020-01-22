using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using PaymentApi.Messages;
using PaymentApi.Models;
using PaymentApi.Adapters;



namespace PaymentApi.Services{

public class BankInfoService{

private readonly BankInfoContext _context;   

public BankInfoService(BankInfoContext bankInfoContext){
    _context = bankInfoContext;
    //Temporary code - adding a dummy entry to database just to check the APIs starting with GET API
    // if (_context.BankInfo.Count() == 0)             
    // {                 
    //     _context.BankInfo.Add(new BankInfo("ICICI", "https://localhost:5052/api/account/", ""));                
    //     _context.SaveChanges();             
    // }    
}
public async Task<BankInfo> CreateBankInfo(BankInfo bankInfo){
    _context.BankInfo.Add(bankInfo); 
    await _context.SaveChangesAsync();
    return bankInfo;
}

public List<BankInfo> GetAll() 
{     
    return _context.BankInfo.ToList(); 
} 
 
public BankInfo GetById(string id) 
{    
    return _context.BankInfo.Find(id);     
}  

public BankInfo GetBankInfo(string ifscCode){
    string bankCode = ifscCode.Substring(0,5);
    return _context.BankInfo.Find(bankCode); 
}
}

}