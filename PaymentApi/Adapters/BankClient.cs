
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;

using PaymentApi.Messages;
using System.Net;
using System.IO;

using PaymentApi.Services;
using PaymentApi.Models;
using PaymentApi.Exceptions;

namespace PaymentApi.Adapters{

public class BankClient{

     private  readonly HttpClient _client = null;

     private readonly BankInfoService _bankInfoService = null;

     public BankClient(Services.BankInfoService bankInfoService){
         _bankInfoService = bankInfoService;
         
         var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback=HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
          _client = new HttpClient(httpClientHandler);
     }
     public  async Task<bool> CheckAccount(string accountNumber, string ifscCode)
         {
             JsonSerializerOptions jOptions =  new JsonSerializerOptions();
             jOptions.PropertyNameCaseInsensitive = true;

            try{
                BankInfo bankInfo = _bankInfoService.GetBankInfo(ifscCode);
  
                var streamTask = _client.GetStreamAsync(bankInfo.AccountAPIUrl + accountNumber); 
                var account = await JsonSerializer.DeserializeAsync<Account>(await streamTask, jOptions);
                if(account!=null){
                    Console.WriteLine(account.AccountNumber + ", " + account.IfscCode); 
                    return true;
                }
                else return false;
            }
            catch(Exception ex){
                 return false;
            }
     
         }

         public  async Task ValidateAccount(string accountNumber, string ifscCode)
         {
            JsonSerializerOptions jOptions =  new JsonSerializerOptions();
            jOptions.PropertyNameCaseInsensitive = true;
            bool isAccountValid = false;
            BankInfo bankInfo = _bankInfoService.GetBankInfo(ifscCode);
            if(bankInfo==null){
                throw new DependencyException("Could not find bank info", null, ifscCode);
            }
            HttpResponseMessage responseMessage = null;
            try{
            responseMessage = await _client.GetAsync(bankInfo.AccountAPIUrl + accountNumber);
            responseMessage.EnsureSuccessStatusCode();
            var responseStreamTask = responseMessage.Content.ReadAsStreamAsync();   
            var account = await JsonSerializer.DeserializeAsync<Account>(await responseStreamTask, jOptions);
            //below - another direct way to directly get the stream - but in this case tricky to get the http status code access in case of exceptions
            // var streamTask = _client.GetStreamAsync(bankInfo.AccountAPIUrl + accountNumber); 
            // var account = await JsonSerializer.DeserializeAsync<Account>(await streamTask, jOptions);
            if(account!=null){
                Console.WriteLine(account.AccountNumber + ", " + account.IfscCode); 
                if(account.AccountNumber==accountNumber) isAccountValid=true;
            }
            }
            catch(HttpRequestException httpReqEx){
                if(responseMessage !=null && responseMessage.StatusCode == HttpStatusCode.NotFound){
                    isAccountValid = false;
                }
                else throw new DependencyException("Failure when invoking ValidateAccount service for ", httpReqEx, ifscCode+"/"+"accountNumber") ;
            }
            if(!isAccountValid) throw new ValidationException(PaymentResult.StatusCodes.InvalidAccount, ifscCode+"/"+"accountNumber");
         }
}
}