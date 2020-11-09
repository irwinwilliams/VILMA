using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.Fluent;
using System;
using System.Linq;
using Microsoft.Rest;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace VMManagement
{
    public class VMStateManager
    {
        public VMStateManager()
        {

        }
        public VMStateManager(AzureCredentialInfo info)
        {
            AzureClientId = info.AzureClientId;
            AzureClientKey = info.AzureClientKey;
            AzureTenantId = info.AzureTenantId;
            AzureSubscriptionId = info.AzureSubscriptionId;
        }

        public string VirtualMachineName { get; set; }
        public string AzureClientId { get; set; }
        public string AzureClientKey { get; set; }
        public string AzureTenantId { get; set; }
        public string AzureSubscriptionId { get; set; }
        public string AccessToken { get; set; }

        public Tuple<string,string> SetState(string state)
        {
            var vmName = VirtualMachineName;
            var client = AzureClientId;
            var key = AzureClientKey;
            var tenant = AzureTenantId;

            IAzure azure = GetAzureFromToken();

            var allVMs = azure.VirtualMachines.List();
            var vmCurrent = (from vm in allVMs where vm.Name == vmName select vm).First();
            switch (state)
            {
                case "on":
                    vmCurrent.Start();
                    break;
                case "off":
                    vmCurrent.PowerOff();
                    break;
                default:
                    break;
            }

            var message = new Tuple<string, string>(vmCurrent
                .GetPrimaryNetworkInterface().PrimaryIPConfiguration.GetPublicIPAddress().IPAddress
                , vmCurrent.PowerState.Value);

            return message;
        }

        private IAzure GetAzureFromToken()
        {
            TokenCredentials tokenCredentials = new TokenCredentials(AccessToken);
            var azureCredentials = new AzureCredentials(
                tokenCredentials,
                tokenCredentials,
                AzureTenantId,
                AzureEnvironment.AzureGlobalCloud);
            var restClient = RestClient
                .Configure()
                .WithEnvironment(AzureEnvironment.AzureGlobalCloud)
                .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                .WithCredentials(azureCredentials)
                .Build();

            var azure = Azure
                .Authenticate(restClient, AzureTenantId)
                .WithSubscription(AzureSubscriptionId);
            return azure;
        }

        /// <summary>
        /// Rate card api: https://medium.com/@dmaas/how-to-query-the-azure-rate-card-api-for-cloud-pricing-complete-step-by-step-guide-4498f8b75c2c
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string FindCost(string text)
        {
            var creds = new AzureCredentialsFactory()
               .FromServicePrincipal(AzureClientId, AzureClientKey, AzureTenantId, 
               AzureEnvironment.AzureGlobalCloud);

            var subscriptionId = AzureSubscriptionId;
            var azure = Microsoft.Azure.Management.Fluent.Azure.Authenticate(creds)
                .WithSubscription(AzureSubscriptionId);
            var allVMs = azure.VirtualMachines.List();
            var vmCurrent = (from vm in allVMs where vm.Name == VirtualMachineName select vm).First();
            var name =  allVMs.FirstOrDefault().Name;
            return name;
            
        }
    }
}
