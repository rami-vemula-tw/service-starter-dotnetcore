using System.ComponentModel.DataAnnotations;

namespace PaymentApi.Models{
    public class BankInfo{
        [Key]
        public string BankCode{get; set;}
        public string AccountAPIUrl{get;set;}
        public string PaymentAPIURL{get; set;}

        public BankInfo(string bankCode, string accountAPIUrl, string paymentAPiUrl){
            BankCode = bankCode;
            AccountAPIUrl = accountAPIUrl;
            PaymentAPIURL = paymentAPiUrl;
        }

        public BankInfo(){            
        }
    }

}