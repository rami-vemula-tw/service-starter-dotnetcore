namespace PaymentApi.Models{

    public class PaymentStatus{
        public static readonly string InProgress ="Initiated";
        public static readonly string Completed ="Completed"; 
        public static readonly string Failed ="Failed"; 
    }

     public class PaymentErrors{
        
        public static readonly  string InvalidAccount="InvalidAccount";
        public static readonly string InvalidBeneficieryAccount="InvalidBeneficiaryAccount";
        public static readonly string InvalidPayeeAccount="InvalidPayeeAccount";
        public static readonly string InsufficientPayeeBalance="InsufficientPayeeBalance";
        public static  readonly string BankTransactionError="BankTransactionError";
        public static readonly string AmountExceedLimit = "AmountExceedLimit";
    }
    
    public class Payment{
        public long Id { get; set; } 
        public string _beneficiaryName{get; set;}
        public string _beneficiaryAccountNumber{get; set;}
        public string _beneficiaryIfscCode{get; set;}
        public string _payeeName{get; set;}
        public string _payeeAccountNumber{get; set;}
        public string _payeeIfscCode{get; set;}
        public string _status{get;set;}
        public int _amount{get;set;}

        public Payment(string beneficiaryName, string beneficiaryAccountNumber, string beneficiaryIfscCode, 
        string payeeName, string payeeAccountNumber, string payeeIfscCode, int amount, string status){

            _beneficiaryName = beneficiaryName;
            _beneficiaryAccountNumber = beneficiaryAccountNumber;
            _beneficiaryIfscCode = beneficiaryIfscCode;

            _payeeName = payeeName;
            _payeeAccountNumber = payeeAccountNumber;
            _payeeIfscCode  = payeeIfscCode;

            _amount = amount; 
            _status = status;
        }
    }

}