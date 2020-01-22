using System;

namespace PaymentApi.Exceptions{
public class DependencyException: Exception{
    public object Details{get;set;}
    public DependencyException(string message, Exception innerException, object details): base(message, innerException){
        Details = details;
    }
}

}