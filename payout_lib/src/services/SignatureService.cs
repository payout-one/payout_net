using System;
using System.Security.Cryptography;
using System.Text;
using Payout.Lib.Base;
using Payout.Lib.Interfaces;
using Payout.Lib.Models;
using Payout.Lib.Notifications;

namespace Payout.Lib.Services
{
    public class SignatureService : IPayoutSignature
    {
        public ApiKey ApiKey { get; set; }

        public string SignResponse(BaseSignedResponse response)
        {
            var signature = this.PrepareSignature(response.signatureParams());
            return Hash(signature);
        }

        public string SignRequest(BaseSignedRequest request)
        {
            var signature = this.PrepareSignature(request.signatureParams());
            return Hash(signature);
        }

        public string SignNotification(BaseNotification notification)
        {
            var signature = this.PrepareSignature(notification.signatureParams());
            return Hash(signature);
        }

        private string PrepareSignature(object[] signatureParams)
        {
            var joined = string.Join("|", signatureParams);
            var secret = $"{joined}|{this.ApiKey.Secret}";

            return secret;
        }

        private string Hash(string signature)
        {
            var message = Encoding.UTF8.GetBytes(signature);
            var hex = String.Empty;

            var hashString = new SHA256Managed();
            var hashValue = hashString.ComputeHash(message);

            foreach (var x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }

            return hex;
        }
    }
}