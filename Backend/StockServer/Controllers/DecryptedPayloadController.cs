using System.Text.Json.Serialization;

public class DecryptedPayload
{
    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }
}
