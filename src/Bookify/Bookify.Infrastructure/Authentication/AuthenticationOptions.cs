namespace Bookify.Infrastructure.Authentication;

public sealed class AuthenticationOptions
{
    public string Audience { get; init; } = string.Empty;

    public string MetadataUrl { get; init; } = string.Empty;

    public bool ReqireHttpsMetadata { get; init; }

    public string Issuer { get; set; } = string.Empty;
}