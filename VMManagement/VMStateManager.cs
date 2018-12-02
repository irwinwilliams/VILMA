using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.Fluent;
using System;
using System.Linq;

namespace VMManagement
{
    public class VMStateManager
    {
        public string VirtualMachineName { get; set; }
        public string AzureClientId { get; set; }
        public string AzureClientKey { get; set; }
        public string AzureTenantId { get; set; }
        public string AzureSubscriptionId { get; set; }

        public Tuple<string,string> SetState(string state)
        {
            var vmName = VirtualMachineName;
            var client = AzureClientId;
            var key = AzureClientKey;
            var tenant = AzureTenantId;
            var creds = new AzureCredentialsFactory()
                .FromServicePrincipal(client, key, tenant, AzureEnvironment.AzureGlobalCloud);
            var subscriptionId = AzureSubscriptionId;
            var azure = Microsoft.Azure.Management.Fluent.Azure.Authenticate(creds).WithSubscription(subscriptionId);
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
                ,vmCurrent.PowerState.Value);

            return message;
        }
    }
}
