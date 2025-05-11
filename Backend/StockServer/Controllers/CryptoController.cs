using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CryptoController : ControllerBase
{
    private readonly RsaKeyService _rsa;

    public CryptoController(RsaKeyService rsa)
    {
        _rsa = rsa;
    }

   [HttpGet("public-key")]
public IActionResult GetPublicKey()
{
    var parameters = _rsa.GetPublicKey();

    string Base64UrlEncode(byte[] input) =>
        Convert.ToBase64String(input)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');

    var jwk = new
    {
        kty = "RSA",
        n = Base64UrlEncode(parameters.Modulus),
        e = Base64UrlEncode(parameters.Exponent),
        alg = "RSA-OAEP-256",
        ext = true,
        key_ops = new[] { "encrypt" }
    };

    return Ok(jwk);
}

}
