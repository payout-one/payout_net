
using Newtonsoft.Json;
using Payout.Lib.Models;
using Payout.Lib.Requests;
using Payout.Lib.Services;
using System;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ConsoleApp48
{
    class Program
    {
        static async Task Main(string[] args)
        {

            //await GetBalance();
            // await GetPaymentsMethods();
            //var checkout =await GetCheckoutById(480060);
            //await RefungPayment(480052, 4585, "EUR", "0441590a-f051-4309-8d8d-942c31e6e136");
             long id = await CreateCheckout();
            await GetCheckoutById(id);
            //await GetCheckouts();
            // await GetTokenInfo();

            //await GetWithdrawals();
            //long id = await CreateWithdrawal();
            //await GetWithdrawalById(id);
        }
        private static async Task<long> CreateCheckout()
        {

            var key = "00706602-bcb6-48e1-939b-b3f02626f2e0";
            var secret = "zXqWzZMCaa7F7GiT64Vnd7Yy0CoqaaHpUySxHfSfE9AF8QBtToo0WYO5y-BTwl0L";

            var host = "sandbox.payout.one";

            var apiKey = new ApiKey { Key = key, Secret = secret, Host = host };
            var clientService = new ClientService(apiKey);

            var token = await clientService.GetToken();
            Console.WriteLine("tokenOK");
            var tokenstring = JsonConvert.SerializeObject(token);
            Console.WriteLine(tokenstring);



            try
            {
                List<Product> products = new List<Product>();

                var product1 = new Product { Name = "product 1", Quantity = 1, UnitPrice = 12 };
                var product2 = new Product { Name = "product 1", Quantity = 1, UnitPrice = 12 };

                products.Add(product1);
                products.Add(product2);

                var request = new CreateCheckoutRequest
                {
                    Amount = 99,
                    BillingAddress = new Address
                    {
                        AddressLine1 = "Floriánska 6",
  
                        City = "Kosice",
                        CountryCode = "SK",
                        Name = "Dominik Sebak",
                        PostalCode = "BL92883"
                    },
                    Currency = "EUR",
                    Customer = new Customer
                    {
                        Email = "dominiksebak@gmail.com",
                        FirstName = "Janko",
                        LastName = "Pokusny",
                        Phone = "4555hgjk21j-123456789"
                    },
                    ExternalId = Guid.NewGuid().ToString(),
                    IdempotencyKey = Guid.NewGuid().ToString(),
                    Nonce = Guid.NewGuid().ToString(),
                    Products = products,
                    RedirectUrl = "https://payout.one/payment/redirect",
                };
                var requeststring = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                //Console.WriteLine("deletedTokendOK");
                Console.WriteLine(requeststring);

                var checkout = await clientService.CreateCheckout(request);
                var json = JsonConvert.SerializeObject(checkout);


                Console.WriteLine(json.ToString());
                return checkout.Id;


            }
            catch (Exception ex)
            {
                Console.WriteLine("chyba");
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                throw;
            }

        }


        private static async Task<long> CreateWithdrawal()
        {

            var key = "00706602-bcb6-48e1-939b-b3f02626f2e0";
            var secret = "zXqWzZMCaa7F7GiT64Vnd7Yy0CoqaaHpUySxHfSfE9AF8QBtToo0WYO5y-BTwl0L";

            var host = "sandbox.payout.one";

            var apiKey = new ApiKey { Key = key, Secret = secret, Host = host };
            var clientService = new ClientService(apiKey);

            var token = await clientService.GetToken();
            Console.WriteLine("tokenOK");
            var tokenstring = JsonConvert.SerializeObject(token);
            Console.WriteLine(tokenstring);



            try
            {
               
                var request = new CreateWithdrawalRequest
                {
                    Amount = 9,

                    Currency = "EUR",
                    Customer = new Customer
                    {
                        Email = "dominiksebak@gmail.com",
                        FirstName = "Dominik",
                        LastName = "Sebak",
                        Phone = "42541-123456520"
                    },
                    ExternalId = Guid.NewGuid().ToString(),
                    IdempotencyKey = Guid.NewGuid().ToString(),
                    Nonce = Guid.NewGuid().ToString(),
                    Iban= "SK6807200002891987426353"

                };
                var requeststring = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                //Console.WriteLine("deletedTokendOK");
                Console.WriteLine(requeststring);

                var checkout = await clientService.CreateWithdrawal(request);
                var json = JsonConvert.SerializeObject(checkout);


                Console.WriteLine(json.ToString());
                return checkout.Id;


            }
            catch (Exception ex)
            {
                Console.WriteLine("chyba");
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                throw;
            }

        }

        private static async Task<(long, decimal, string)> GetCheckoutById(long id)
        {

            var key = "00706602-bcb6-48e1-939b-b3f02626f2e0";
            var secret = "zXqWzZMCaa7F7GiT64Vnd7Yy0CoqaaHpUySxHfSfE9AF8QBtToo0WYO5y-BTwl0L";

            var host = "sandbox.payout.one";

            var apiKey = new ApiKey { Key = key, Secret = secret, Host = host };
            var clientService = new ClientService(apiKey);

            var token = await clientService.GetToken();
            Console.WriteLine("tokenOK");
            var tokenstring = JsonConvert.SerializeObject(token);
            Console.WriteLine(tokenstring);



            try
            {


                var checkout = await clientService.GetCheckout(new GetCheckoutRequest { Id = id });
                var json = JsonConvert.SerializeObject(checkout);

                return (checkout.Id, checkout.Amount, checkout.Currency);
                //Console.WriteLine(json.ToString());
                //Console.ReadKey();


            }
            catch (Exception ex)
            {
                Console.WriteLine("chyba");
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                throw;
            }

        }


        private static async Task GetTokenInfo()
        {

            var key = "00706602-bcb6-48e1-939b-b3f02626f2e0";
            var secret = "zXqWzZMCaa7F7GiT64Vnd7Yy0CoqaaHpUySxHfSfE9AF8QBtToo0WYO5y-BTwl0L";

            var host = "sandbox.payout.one";

            var apiKey = new ApiKey { Key = key, Secret = secret, Host = host };
            var clientService = new ClientService(apiKey);

            var token = await clientService.GetToken();
            Console.WriteLine("tokenOK");
            var tokenstring = JsonConvert.SerializeObject(token);
            Console.WriteLine(tokenstring);



            try
            {


                var status = await clientService.GetTokenStatus(new GetTokenStatusRequest { Token = tokenstring });
                var json = JsonConvert.SerializeObject(status);


                Console.WriteLine(json.ToString());
                Console.ReadKey();


            }
            catch (Exception ex)
            {
                Console.WriteLine("chyba");
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                throw;
            }

        }

        private static async Task GetCheckouts()
        {

            var key = "00706602-bcb6-48e1-939b-b3f02626f2e0";
            var secret = "zXqWzZMCaa7F7GiT64Vnd7Yy0CoqaaHpUySxHfSfE9AF8QBtToo0WYO5y-BTwl0L";

            var host = "sandbox.payout.one";

            var apiKey = new ApiKey { Key = key, Secret = secret, Host = host };
            var clientService = new ClientService(apiKey);

            var token = await clientService.GetToken();
            Console.WriteLine("tokenOK");
            var tokenstring = JsonConvert.SerializeObject(token);
            Console.WriteLine(tokenstring);



            try
            {


                var checkouts = await clientService.GetCheckouts(new GetCheckoutsRequest {Limit = 50, Offset=0 });
                var json = JsonConvert.SerializeObject(checkouts);


                Console.WriteLine(json.ToString());
                Console.ReadKey();


            }
            catch (Exception ex)
            {
                Console.WriteLine("chyba");
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                throw;
            }

        }

        private static async Task RefungPayment(long id, int ammount, string cur, string externalId )
        {

            var key = "00706602-bcb6-48e1-939b-b3f02626f2e0";
            var secret = "zXqWzZMCaa7F7GiT64Vnd7Yy0CoqaaHpUySxHfSfE9AF8QBtToo0WYO5y-BTwl0L";

            var host = "sandbox.payout.one";

            var apiKey = new ApiKey { Key = key, Secret = secret, Host = host };
            var clientService = new ClientService(apiKey);

            var token = await clientService.GetToken();
            Console.WriteLine("tokenOK");
            var tokenstring = JsonConvert.SerializeObject(token);
            Console.WriteLine(tokenstring);



            try
            {


                var refund = await clientService.RefundPayment(new RefundPaymentRequest 
                {  CheckoutId = id
                ,Currency = cur
                ,Amount=ammount
                ,ExternalId = externalId
                ,Iban= "SK6807200002891987426353"
                , StatementDescriptor = "test"
                , Nonce = "ZUc0Mk9sVXZDOXNsdklzMQ" });
                var json = JsonConvert.SerializeObject(refund);


                Console.WriteLine(json.ToString());
                Console.ReadKey();


            }
            catch (Exception ex)
            {
                Console.WriteLine("chyba");
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                throw;
            }

        }

        private static async Task GetPaymentsMethods()
        {

            var key = "00706602-bcb6-48e1-939b-b3f02626f2e0";
            var secret = "zXqWzZMCaa7F7GiT64Vnd7Yy0CoqaaHpUySxHfSfE9AF8QBtToo0WYO5y-BTwl0L";

            var host = "sandbox.payout.one";

            var apiKey = new ApiKey { Key = key, Secret = secret, Host = host };
            var clientService = new ClientService(apiKey);

            var token = await clientService.GetToken();
            Console.WriteLine("tokenOK");
            var tokenstring = JsonConvert.SerializeObject(token);
            Console.WriteLine(tokenstring);



            try
            {


                var methods = await clientService.GetPaymentMethods(new GetPaymentMethodsRequest());
                var json = JsonConvert.SerializeObject(methods);


                Console.WriteLine(json.ToString());
                Console.ReadKey();


            }
            catch (Exception ex)
            {
                Console.WriteLine("chyba");
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                throw;
            }

        }


        private static async Task GetBalance()
        {

            var key = "00706602-bcb6-48e1-939b-b3f02626f2e0";
            var secret = "zXqWzZMCaa7F7GiT64Vnd7Yy0CoqaaHpUySxHfSfE9AF8QBtToo0WYO5y-BTwl0L";

            var host = "sandbox.payout.one";

            var apiKey = new ApiKey { Key = key, Secret = secret, Host = host };
            var clientService = new ClientService(apiKey);

            var token = await clientService.GetToken();
            Console.WriteLine("tokenOK");
            var tokenstring = JsonConvert.SerializeObject(token);
            Console.WriteLine(tokenstring);



            try
            {


                var balance = await clientService.GetBalance(new GetBalanceRequest());
                var json = JsonConvert.SerializeObject(balance);


                Console.WriteLine(json.ToString());
                Console.ReadKey();


            }
            catch (Exception ex)
            {
                Console.WriteLine("chyba");
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                throw;
            }

        }


        private static async Task GetWithdrawals()
        {

            var key = "00706602-bcb6-48e1-939b-b3f02626f2e0";
            var secret = "zXqWzZMCaa7F7GiT64Vnd7Yy0CoqaaHpUySxHfSfE9AF8QBtToo0WYO5y-BTwl0L";

            var host = "sandbox.payout.one";

            var apiKey = new ApiKey { Key = key, Secret = secret, Host = host };
            var clientService = new ClientService(apiKey);

            var token = await clientService.GetToken();
            Console.WriteLine("tokenOK");
            var tokenstring = JsonConvert.SerializeObject(token);
            Console.WriteLine(tokenstring);



            try
            {


                var withdrawals = await clientService.GetWithdrawals(new GetWithdrawalsRequest { Limit = 50, Offset = 0 });
                var json = JsonConvert.SerializeObject(withdrawals);


                Console.WriteLine(json.ToString());
                Console.ReadKey();


            }
            catch (Exception ex)
            {
                Console.WriteLine("chyba");
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                throw;
            }

        }

        private static async Task GetWithdrawalById(long id)
        {

            var key = "00706602-bcb6-48e1-939b-b3f02626f2e0";
            var secret = "zXqWzZMCaa7F7GiT64Vnd7Yy0CoqaaHpUySxHfSfE9AF8QBtToo0WYO5y-BTwl0L";

            var host = "sandbox.payout.one";

            var apiKey = new ApiKey { Key = key, Secret = secret, Host = host };
            var clientService = new ClientService(apiKey);

            var token = await clientService.GetToken();
            Console.WriteLine("tokenOK");
            var tokenstring = JsonConvert.SerializeObject(token);
            Console.WriteLine(tokenstring);



            try
            {


                var withdrawal = await clientService.GetWithdrawal(new GetWithdrawalRequest { Id =id });
                var json = JsonConvert.SerializeObject(withdrawal);

                //if (withdrawal.Status == Payout.Lib.WithdrawalStatus.Pending)
                //    Console.WriteLine("je pending");

                Console.WriteLine(json.ToString());
                Console.ReadKey();


            }
            catch (Exception ex)
            {
                Console.WriteLine("chyba");
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                throw;
            }

        }

    }
}
