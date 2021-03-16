using Payout.Lib;
using Payout.Lib.Models;
using Payout.Lib.Notifications;
using Payout.Lib.Services;
using System.Text.Json;
using Xunit;

namespace payout_tests
{
    public class NotificationTest
    {
        [Fact]
        public void BankTransferNotificationTest()
        {          
            var apiKey = new ApiKey { Key = "00706602-bcb6-48e1-939b-b3f02626f2e0", Secret = "zXqWzZMCaa7F7GiT64Vnd7Yy0CoqaaHpUySxHfSfE9AF8QBtToo0WYO5y-BTwl0L" };
            var signatureService = new SignatureService { ApiKey = apiKey };

            string json = "{\"data\":{\"amount\":20078,\"currency\":\"EUR\",\"customer\":{\"email\":\"test@payout.one\",\"first_name\":\"Lukáš\",\"last_name\":\"Tester\"},\"external_id\":\"1390\",\"id\":477738,\"idempotency_key\":null,\"metadata\":null,\"object\":\"checkout\",\"payment\":null,\"redirect_url\":\"https://localhost\",\"status\":\"processing\"},\"external_id\":\"1390\",\"nonce\":\"TXRYWklDTjdCZHBQTno2Nw==\",\"object\":\"webhook\",\"signature\":\"9b63299479d97da9ac8bf5fb49d7767fbfffe2a44af1c5397f933e17c75ca56a\",\"type\":\"bank_transfer.in_transit\"}";
            var notification = JsonSerializer.Deserialize<BaseNotification>(json);
            var checkout = notification.TryParse<Checkout>();

            Assert.True(notification.ExternalId == checkout.ExternalId);
            Assert.True(notification.Signature == notification.CalculateSignature(signatureService));
        }

        [Fact]
        public void PaymentNotificationTest()
        {           
            var apiKey = new ApiKey { Key = "00706602-bcb6-48e1-939b-b3f02626f2e0", Secret = "zXqWzZMCaa7F7GiT64Vnd7Yy0CoqaaHpUySxHfSfE9AF8QBtToo0WYO5y-BTwl0L" };
            var signatureService = new SignatureService { ApiKey = apiKey };

            string json = "{\"data\":{\"amount\":1499,\"currency\":\"EUR\",\"customer\":{\"email\":\"test@payout.one\",\"first_name\":\"Lukáš\",\"last_name\":\"Tester\"},\"external_id\":\"5333\",\"id\":477760,\"idempotency_key\":null,\"metadata\":null,\"object\":\"checkout\",\"payment\":{\"created_at\":1587455451,\"failure_reason\":\"\",\"fee\":65,\"net\":1434,\"object\":\"payment\",\"payment_method\":\"PayU\",\"status\":\"successful\"},\"redirect_url\":\"https://localhost\",\"status\":\"succeeded\"},\"external_id\":\"5333\",\"nonce\":\"Z0FidXlCSVUxcUxsbEVwag\",\"object\":\"webhook\",\"signature\":\"43a0ac2b9005ba1b4305cf00f7ac3d84db7c461dd84f00e58868565dd4f71efe\",\"type\":\"checkout.succeeded\"}";
            var notification = JsonSerializer.Deserialize<BaseNotification>(json);
            var checkout = notification.TryParse<Checkout>();

            Assert.True(notification.ExternalId == checkout.ExternalId);
            Assert.True(notification.Signature == notification.CalculateSignature(signatureService));
        }
    }
}