﻿namespace Ebret4m4n.Repository.Configuration;

public class EmailSettings
{
    public string SmtpServer { get; set; } = null!;

    public int SmtpPort { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}
