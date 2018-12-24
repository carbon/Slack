using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Carbon.Json;

namespace Slack
{
    public class SlackClient
    {
        private readonly HttpClient httpClient = new HttpClient();

        private readonly string token;
        private readonly string subdomain;

        public SlackClient(string subdomain, string token)
        {
            this.subdomain = subdomain ?? throw new ArgumentNullException(nameof(subdomain));
            this.token = token ?? throw new ArgumentNullException(nameof(token));
        }

        public async Task<string> PostMessageAsync(SlackMessage message)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var json = JsonObject.FromObject(message);

            var content = new FormUrlEncodedContent(new Dictionary<string, string> {
                { "payload", json.ToString() }
            });

            var url = $"https://{subdomain}.slack.com/services/hooks/incoming-webhook?token=" + token;

            using (var response = await httpClient.PostAsync(url, content).ConfigureAwait(false))
            {
                return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
        }
    }
}