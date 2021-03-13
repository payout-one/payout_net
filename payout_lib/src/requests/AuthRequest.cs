using Payout.Lib.Base;
using Payout.Lib.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Payout.Lib.Requests
{
    public class AuthRequest : BaseRequest
    {
        public ApiKey ApiKey { get; set; }

        public override HttpRequestMessage Request(string host)
        {
            return new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"https://{host}/api/v1/authorize"),
                Content = new StringContent(JsonSerializer.Serialize(this.ApiKey), Encoding.UTF8, "application/json")
            };
        }
    }
}
