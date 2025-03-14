﻿namespace Ebret4m4n.Entities.ConfigurationModels;

public class JwtConfiguration
{
    public string Section { get; set; } = "JwtSettings";

    public string? Issuer { get; set; }

    public string? Audience { get; set; }

    public string? Expires { get; set; }
}
