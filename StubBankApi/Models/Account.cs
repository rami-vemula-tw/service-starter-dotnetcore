using System.ComponentModel.DataAnnotations;

namespace StubBankApi.Models{
    public class Account{
        [Key]
        public string AccountNumber {get;set;}
        public string IfscCode{get;set;}

        public Account(string accountNumber, string ifscCode)
        {
            AccountNumber = accountNumber;
            IfscCode = ifscCode;
        }
        public Account()
        {

        }
        
    }
}