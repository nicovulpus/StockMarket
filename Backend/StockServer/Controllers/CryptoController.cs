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
        var key = _rsa.GetPublicKey();

        return Ok(new
        {
            Modulus = Convert.ToBase64String(key.Modulus),
            Exponent = Convert.ToBase64String(key.Exponent)
        });
    }
}
