namespace Ebret4m4n.Entities.ConfigurationModels;

public class StripeSettings
{
    public string PublishableKey { get; set; } = string.Empty;

    public string SecretKey { get; set; } = string.Empty;

    public string WebhookSecret { get; set; } = string.Empty;
}
