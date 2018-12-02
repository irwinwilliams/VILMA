using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using VMManagement;
using Microsoft.Extensions.Configuration;

namespace function_shelly4azqueue
{
    public static class StateExecutorFunction
    {
        [FunctionName("StateExecutorFunction")]
        public static async Task Run([QueueTrigger("changevmstate")]string myQueueItem, ILogger log, ExecutionContext context)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();


            log.LogInformation($"Got this state request: {myQueueItem}");
            var vmStateManager = new VMStateManager
            {
                VirtualMachineName = config["VirtualMachineName"],
                AzureClientId = config["ClientId"],
                AzureClientKey = config["ClientKey"],
                AzureSubscriptionId = config["SubscriptionId"],
                AzureTenantId = config["TenantId"]

            };
            var result = vmStateManager.SetState(myQueueItem);

            await Alert(config, result);
        }

        private static async Task Alert(IConfigurationRoot config, Tuple<string, string> result)
        {
            var apiKey = config["EmailAPIKey"];
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress(config["EmailFrom"], config["EmailFromName"]));
            var recipients = new List<EmailAddress>
            {
                new EmailAddress(config["EmailTo"], config["EmailToName"])
            };
            msg.AddTos(recipients);

            if (result.Item2.Contains("running"))
            {
                msg.SetSubject("Your VM is ready!");
                var rdp = @"full address:s:"+result.Item1+@":3389
prompt for credentials:i:1
administrative session:i:1";
                msg.AddContent(MimeType.Text, rdp);
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(rdp);
                var base64enc =  System.Convert.ToBase64String(plainTextBytes);
                msg.AddAttachment("vm-connection.rdp", base64enc);
            }
            else
            {
                msg.SetSubject("Hey, VILMA turned off your VM");
                msg.AddContent(MimeType.Text, "The VM's off, bro");

            }

            await client.SendEmailAsync(msg);


        }
    }
}
