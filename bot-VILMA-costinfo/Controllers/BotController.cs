// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.5.0

using System.Threading.Tasks;
using bot_VILMA_costinfo.Bots;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Options;
using VMManagement;

namespace bot_VILMA_costinfo.Controllers
{
    // This ASP Controller is created to handle a request. Dependency Injection will provide the Adapter and IBot
    // implementation at runtime. Multiple different IBot implementations running at different endpoints can be
    // achieved by specifying a more specific type for the bot constructor argument.
    [Route("api/messages")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private readonly IBotFrameworkHttpAdapter Adapter;
        private readonly IBot Bot;
        private readonly IOptions<AzureCredentialInfo> credentialInfo;

        public BotController(IBotFrameworkHttpAdapter adapter, IBot bot, IOptions<AzureCredentialInfo> options)
        {
            Adapter = adapter;
            Bot = bot;
            credentialInfo = options;
        }

        [HttpPost]
        public async Task PostAsync()
        {
            // Delegate the processing of the HTTP POST to the adapter.
            // The adapter will invoke the bot.
            //CostFinderBot.SetCredentials(credentialInfo.Value);
            await Adapter.ProcessAsync(Request, Response, Bot);
        }
    }
}
