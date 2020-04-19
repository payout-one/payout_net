using Payout.Lib;
using Payout.Lib.Base;
using Payout.Lib.Models;
using Payout.Lib.Notifications;
using Payout.Lib.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace payout_tests
{
    public class NotificationTest
    {
        [Fact]
        public void BankTransferNotificationTest()
        {
            var apiKey = new ApiKey { Key = Constants.KEY, Secret = Constants.SECRET };
            var signatureService = new SignatureService { ApiKey = apiKey };

            string json = "{\"data\":{\"amount\":20078,\"currency\":\"EUR\",\"customer\":{\"email\":\"test@payout.one\",\"first_name\":\"Lukáš\",\"last_name\":\"Tester\"},\"external_id\":\"1390\",\"id\":477738,\"idempotency_key\":null,\"metadata\":null,\"object\":\"checkout\",\"payment\":null,\"redirect_url\":\"https://localhost\",\"status\":\"processing\"},\"external_id\":\"1390\",\"nonce\":\"TXRYWklDTjdCZHBQTno2Nw==\",\"object\":\"webhook\",\"signature\":\"47db1d8f17fb01b387fa42e3cfa0d3fba63b943f25bbbcec66d9242c17f7d9ec\",\"type\":\"bank_transfer.in_transit\"}";
            var notification = JsonSerializer.Deserialize<BaseNotification>(json);
            var checkout = notification.TryParse<Checkout>();

            Assert.True(notification.ExternalId == checkout.ExternalId);
            Assert.True(notification.Signature == notification.CalculateSignature(signatureService));
        }

        [Fact]
        public void PaymentNotificationTest()
        {
            var apiKey = new ApiKey { Key = Constants.KEY, Secret = Constants.SECRET };
            var signatureService = new SignatureService { ApiKey = apiKey };

            string json = "{\"data\":{\"amount\":1499,\"currency\":\"EUR\",\"customer\":{\"email\":\"test@payout.one\",\"first_name\":\"Lukáš\",\"last_name\":\"Tester\"},\"external_id\":\"5333\",\"id\":477760,\"idempotency_key\":null,\"metadata\":null,\"object\":\"checkout\",\"payment\":{\"created_at\":1587455451,\"failure_reason\":\"\",\"fee\":65,\"net\":1434,\"object\":\"payment\",\"payment_method\":\"PayU\",\"status\":\"successful\"},\"redirect_url\":\"https://localhost\",\"status\":\"succeeded\"},\"external_id\":\"5333\",\"nonce\":\"Z0FidXlCSVUxcUxsbEVwag\",\"object\":\"webhook\",\"signature\":\"3a0da88bfa085f1825bf91a5ed2b5c7c7627c0d3fd3da0f3c2c1daa815b867f5\",\"type\":\"checkout.succeeded\"}";
            var notification = JsonSerializer.Deserialize<BaseNotification>(json);
            var checkout = notification.TryParse<Checkout>();

            Assert.True(notification.ExternalId == checkout.ExternalId);
            Assert.True(notification.Signature == notification.CalculateSignature(signatureService));
        }
    }
}