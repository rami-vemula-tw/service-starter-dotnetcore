# service-starter-dotnetcore
This is a sample starter services incorporating best practices for writing services in asp.net core.
Uses PostgreSQL database.

This repo contains the following:  
-PaymentApi - the sample service with basic payment flow for demo purposes. It exposes a payment REST API 
that receives a payment request and performs actions to complete a payment transaction. 
It has a database that holds the payments info in a Payment table. It also has a BankInfo table that holds basic info on 
a Bank and the URLs it provides for validating accounts and making payments. 

The PaymentApi takes connectionString from appsettings.json. It is also coded to take connectionString or any config
from environment variables or commandline. 

-StubBankApi - a stub bank service for the payment service to call for validating accounts and performing payment related calls.
It has a database which holds a list of accounts. It exposes an account service using which Payment service can validate 
accounts given for payment.  
