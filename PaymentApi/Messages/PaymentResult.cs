using PaymentApi.Models;

namespace PaymentApi.Messages{
    public class PaymentResult{

        public class ResultCodes 
        {
            public static readonly string Success="Success";
            public static readonly string  InvalidInput="InvalidInput";
            public static readonly  string TransactionError="TransactionError";
            public static readonly string SystemError="SystemError";
            public static readonly string InProgress="InProgress";
        }

        public class StatusCodes 
        {
            public static readonly  string InvalidAccount="InvalidAccount";
            public static readonly string InvalidBeneficieryAccount="InvalidBeneficiaryAccount";
            public static readonly string InvalidPayeeAccount="InvalidPayeeAccount";
            public static readonly string InsufficientPayeeBalance="InsufficientPayeeBalance";
            public static  readonly string BankTransactionError="BankTransactionError";

            public static readonly string TransactionInitiated = "TransactionInitiated";

            public static readonly string PayeeDebited = "PayeeDebited";

            public static readonly string BeneficiaryCredited = "BeneficiaryCredited";

            public static readonly string AmountExceedLimit = "AmountExceedLimit";
        }
         public string Result {get;set;}

         public string Status {get;set;}

         public Payment PaymentRecord {get;set;} 

         public PaymentResult(string result, string status, Payment paymentRecord){
             Result = result;
             Status = status;
             PaymentRecord = PaymentRecord;

             Result = ResultCodes.InProgress; 
         }
         public  PaymentResult(){
             Result = ResultCodes.InProgress;

         }

         public void SetResult(string result, string status, Payment paymentRecord){
             Result = result;
             Status = status;
             PaymentRecord = paymentRecord;
         }
    }
}