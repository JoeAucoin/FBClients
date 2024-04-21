using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace GIBS.Modules.FBClients.Components
{
    public class TwilioSMS
    {
        private string accountSid;
        private string authToken;
        private string twilioPhoneNumber;

        public TwilioSMS(string accountSid, string authToken, string twilioPhoneNumber)
        {
            this.accountSid = accountSid;
            this.authToken = authToken;
            this.twilioPhoneNumber = twilioPhoneNumber;
        }

        public void SendSMS(string toPhoneNumber, string message)
        {
            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(
                new PhoneNumber(toPhoneNumber))
            {
                From = new PhoneNumber(twilioPhoneNumber),
                Body = message
            };

            var messageResponse = MessageResource.Create(messageOptions);

            // You can handle response here if needed
            Console.WriteLine($"Message sent with SID: {messageResponse.Sid}");
        }
    }
}