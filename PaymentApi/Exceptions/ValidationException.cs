using System;

namespace PaymentApi.Exceptions{
public class ValidationException: Exception{
    public object Details{get;set;}
    public ValidationException(string message, object details): base(message){
        Details = details;
    }
}

}