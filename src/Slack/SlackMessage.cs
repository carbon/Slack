using System.Runtime.Serialization;

namespace Slack
{
    public class SlackMessage
    {
        [DataMember(Name = "channel")]
        public string Channel { get; set; }

        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }
    }
}