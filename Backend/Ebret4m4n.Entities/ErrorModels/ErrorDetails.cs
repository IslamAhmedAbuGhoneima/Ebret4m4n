﻿using System.Text.Json;

namespace Ebret4m4n.Entities.ErrorModels;

public class ErrorDetails
{
    public int StatusCode { get; set; }

    public string? Message { get; set; }

    public override string ToString() => JsonSerializer.Serialize(this);
}
