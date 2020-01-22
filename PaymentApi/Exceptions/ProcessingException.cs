using System;

namespace PaymentApi.Exceptions{
public class ProcessingException: Exception{
    public object Details{get;set;}
    public ProcessingException(string message, Exception innerException, object details): base(message, innerException){
        Details = details;
    }
}

}