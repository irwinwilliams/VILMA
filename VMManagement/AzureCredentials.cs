using System;
using System.Collections.Generic;
using System.Text;

namespace VMManagement
{
    public class AzureCredentialInfo
    {
        public AzureCredentialInfo()
        {

        }
        public AzureCredentialInfo(string azureClientId, string azureClientKey, string azureTenantId, string azureSubscriptionId)
        {
            AzureClientId = azureClientId;
            AzureClientKey = azureClientKey;
            AzureTenantId = azureTenantId;
            AzureSubscriptionId = azureSubscriptionId;
        }

        public string AzureClientId { get; private set; }
        public string AzureClientKey { get; private set; }
        public string AzureTenantId { get; private set; }
        public string AzureSubscriptionId { get; private set; }
    }
}
