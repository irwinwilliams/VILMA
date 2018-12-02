using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using System.Dynamic;
using function_shelly3;
using Newtonsoft.Json.Linq;

namespace Shelly.Bot
{
    public static class GoogleActions
    {
        private static string botId = "bot-vilma";

        [FunctionName("GoogleActions")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [Queue("changevmstate")] out string state,

            ILogger log)
        {
            state = null;

            try
            {
                string requestBody = new StreamReader(req.Body).ReadToEnd();
                var data = JsonConvert.DeserializeObject<Request>(requestBody);
                log.LogInformation(requestBody);
                var query= data.queryResult.queryText;

                dynamic actionResult = new ExpandoObject();
                string result = string.Empty;

                if (data.queryResult.intent.displayName == "ChangeVMState")
                {
                    state = data.queryResult.parameters.vmstate;
                    result = $"{state} is going to be applied, Irwin";

                    actionResult.fulfillmentText = result;
                }
                
                actionResult.source = botId;

                var output = JsonConvert.SerializeObject(actionResult, 
                    Formatting.Indented, new AtAtJsonConverter(typeof(ExpandoObject)));

                return output != null
                    ? (ActionResult)new JsonResult(actionResult)
                    : new BadRequestObjectResult("ERROR: "+output);
            }
            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);
                return new BadRequestObjectResult("Badness");
            }
        }
    }
}


//VMStateManager.SetState(state);



////https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-howto-direct-line?view=azure-bot-service-4.0&tabs=cscreatebot%2Ccsclientapp%2Ccsrunclient#create-the-console-client-app
//// Create a new Direct Line client.
//var client = new DirectLineClient(directLineSecret);
//// Start the conversation.
//Conversation conversation = null;
//if (!Conversations.ContainsKey(data.session))
//{
//    conversation = await client.Conversations.StartConversationAsync();
//    Conversations.Add(data.session, conversation.ConversationId);
//}
//else
//    conversation = await client.Conversations.ReconnectToConversationAsync(Conversations[data.session]);

//// Create a message activity with the text the user entered.
//Activity userMessage = new Activity
//{
//    From = new ChannelAccount(fromUser),
//    Text = data.queryResult.queryText,
//    Type = ActivityTypes.Message
//};

//// Send the message activity to the bot.
//await client.Conversations.PostActivityAsync(conversation.ConversationId, userMessage);
//string watermark = null;
//// Retrieve the activity set from the bot.
//var activitySet = await client.Conversations.GetActivitiesAsync(conversation.ConversationId, watermark);
//watermark = activitySet?.Watermark;

//// Extract the activies sent from our bot.
//var activities = from x in activitySet.Activities
//                 where x.From.Id == botId
//                 select x;
//var result = "";
//// Analyze each activity in the activity set.
//foreach (Activity activity in activities)
//{
//    // Display the text of the activity.
//    result = activity.Text;
//}