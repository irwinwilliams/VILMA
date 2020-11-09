// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.5.0

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Bot.Builder.Community.Middleware.AzureAdAuthentication;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VMManagement;

namespace bot_VILMA_costinfo.Bots
{
    public class AzureAgentBot : ActivityHandler
    {
        private readonly ILogger logger;

        public static AzureCredentialInfo AzureCredentialInfo { get; private set; }

        public AzureAgentBot(ILoggerFactory loggerFactory)
        {
            if (loggerFactory == null)
            {
                throw new System.ArgumentNullException(nameof(loggerFactory));
            }

            logger = loggerFactory.CreateLogger<AzureAgentBot>();
            logger.LogTrace("Turn start.");
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, 
            CancellationToken cancellationToken)
        {
            
            //var result = vmStateManager.FindCost(turnContext.Activity.Text);
            var authToken = (ConversationAuthToken)turnContext.TurnState["authToken"];
            var vmStateManager = new VMStateManager()
            {
                AzureSubscriptionId = "7bead0ea-f504-43de-98a6-2ae64577e131", 
                AzureTenantId = "7b5c2477-f717-48cd-aa28-24cee4e464dc",
                AccessToken = authToken.AccessToken
            };
            var res = vmStateManager.SetState("tester");

            //string c = "";
            //try
            //{
            //    using (var client = new HttpClient())
            //    {
            //        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers
            //            .AuthenticationHeaderValue("Bearer", authToken.AccessToken);

            //        //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue { }
            //        //$headers = @{ "Authorization" = "bearer " +  $tokenInfo.access_token}

            //        c = await client.GetStringAsync("https://management.azure.com/subscriptions?api-version=2019-06-01");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex);
            //}

            await turnContext.SendActivityAsync(MessageFactory.Text($"Echo: {authToken.Id}"), cancellationToken);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Hello and welcome!"), cancellationToken);
                }
            }
        }

        internal static void SetCredentials(AzureCredentialInfo credentialInfo)
        {
            AzureCredentialInfo = credentialInfo;
        }
    }
}
