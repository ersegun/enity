# Introduction 

The intent of this application is to test your knowledge about .net and specifically .net core. We have intentionally not been using any third party libraries but if you want to add one, you're welcome to. 

# Solution structure
You should now have received a copy containing a Visual Studio solution with two projects and the structure looks like this

bluestep-code-test.sln

| **api**  |     
|:-------------|
| Contracts (contains the response object used by the controller) |
| Controllers  (contains the controller for invoking the services) | 
| Models       (this contains the models used in the services) | 
| Services     (interfaces for the services) |  
| appsettings.json |
| Program.cs |
| Startup.cs |

| **apitests**  | |     
|:-------------|:----------- |
| Mocks    (returns mocked data for the tests) | |
| MockAccountService.cs | MockExchangeRateService.cs |
| ConversionServiceTest.cs    (this is the test suite for the first assignment) |  |
| TransactionServiceTest.cs   (this is the test suite for the last assignment) |   |

We would like you to implement the services (interfaces) in the api/services folder and make the application use those implementations.
You should not need to modify the models, contracts, tests etc but you may do so if you want to refactor.

The only thing apart from implementing the interfaces is to change the unit tests so that they also are using the new implementations.

# The test

The test consist of three parts 
1. Make sure that the ConversionServiceTests passes
2. Make sure that you can run the application
3. Make sure that the TransactionServiceTests passes

**Focus on one assignment at a time.**

The application will be given an account with transactions. Each transaction contains the balance of the account at the moment of the transaction. Using an exchange rate, the application will recalculate the current balance of the account, as well as the balance for each transactions, to another currency.

In the second task, when the app starts, you should be able to browse to https://localhost:5001/account?currency=EUR in order to get the account balance in Euro. 

Finally, in the last and third assignment you will do a more advanced calculation on the transactions for the account. You will be asked to find between which two dates you have the highest positive change. 


# 1. Making the ConversionServiceTests pass
We have provided you with the test ConversionServiceTest and your assignment is to make all the tests pass by implementing the logic required in the interface IConversionService.
The data for the service is provided by mock objects and the results (asserts) are given.

So, this test will pass if you implement the interface IConversionService correctly. For the test, the data for account and exchangerates is provided by the mock objects in the test (those are located in the apitest/mocks folder).
As you will see, the mocks returns services based on the interfaces IAccountService and IExchangeRateService. 


# 2. Making the application run.
In step 1 you've hopefully made the tests pass. This means that your implementation of the IConversionService interface is working properly. In this second assignment we want you to fetch data from external sources instead.
There are two endpoints, one for account data and one for exchange rate data
1. https://bstpdevelopertestfiles.z1.web.core.windows.net/account.json - returns a json containing the account
2. https://bstpdevelopertestfiles.z1.web.core.windows.net/currencies.json - returns a json containing the exchange rates

As stated previously, when the application runs you should be able to browse to https://localhost:5001/account?currency=EUR in order to get the accounts (and it's transactions) balance in Euro. 

In the first assignment you completed the IConversionService and if you now implement the IAccountService and IExchangeRateService interfaces so that you fetch data from the url's provided above, hands that data over to the IConversionService, you should be able to run the application using these new implementations.

!**Note** The controller also takes the ITransactionService interface as parameter but the implementation of that service should be solved in the third assignment so just implement a dummy version for now that just returns null values.


# 3. Making the TransactionServiceTests pass
In this assignment you will have to implement the ITransactionService.GetHighestPositiveBalanceChange. This method takes a list of transactions and your job is to create an algorithm that figures out between which two transactions we have the highest positive change in the balance.

In the first and second assignment you created implementations for the interfaces IAccountService and IExchangeRateService. You also created a dummy implementation of the ITransactionService that just returned null values.
Now, in this assignment, after implementing the ITransactionService interface correctly and with proper values, the tests in TransactionServiceTests should pass. 

Also, when the implementation if your ITransactionService is done, you should be able to run the application once more but with extended information (the return values from GetHighestPositiveBalanceChange) shown in the output.

So, given the transactions for an account, you should be able to calculate between **which two dates** you have the **higest positive change**

For example, given the data below, the highest positive change in balance is between the 12th and the 14th of October, balance going from -4000 to +5000. Given this example, the method should return 

** 2012-10-12, 2012-10-14, 9000 **

| Date       | Balance     |
|------------|-------------|
| 2012-10-08 |   -5000,00  |
| 2012-10-09 |   2000,00   |
| 2012-10-10 |   0,00      |
| 2012-10-11 |   2000,00   |
| 2012-10-12 |   -4000,00  |
| 2012-10-13 |   0,00      |
| 2012-10-14 |   5000,00   |
| 2012-10-15 |   1000,00   |
| 2012-10-16 |   6000,00   |

