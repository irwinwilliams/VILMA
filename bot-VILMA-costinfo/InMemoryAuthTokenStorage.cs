using Bot.Builder.Community.Middleware.AzureAdAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bot_VILMA_costinfo
{
    public class InMemoryAuthTokenStorage : IAuthTokenStorage
    {
        private static readonly Dictionary<string, ConversationAuthToken> InMemoryDictionary 
            = new Dictionary<string, ConversationAuthToken>();

        public ConversationAuthToken LoadConfiguration(string id)
        {
            ConversationAuthToken result = null;
            if (InMemoryDictionary.ContainsKey(id))
            {
                result = InMemoryDictionary[id];
            }

            return result;
        }

        public void SaveConfiguration(ConversationAuthToken token)
        {
            if (!InMemoryDictionary.ContainsKey(token.Id))
                InMemoryDictionary.Add(token.Id, token);
            else
                InMemoryDictionary[token.Id] = token;
        }
    }
}
