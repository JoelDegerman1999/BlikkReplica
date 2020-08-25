namespace BlikkBasicReplica.API.Webhooks.Models
{
    public class WebhookSubscription
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public string EventName { get; set; }
        public string TargetUrl { get; set; }

    }


}
