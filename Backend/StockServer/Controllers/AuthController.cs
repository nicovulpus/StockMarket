using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Text.Json;
using System.Security.Cryptography;
using BCrypt.Net; // ✅ Add this if not already

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly RsaKeyService _rsa;

    public AuthController(RsaKeyService rsa)
    {
        _rsa = rsa;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterDto user)
    {
        try
        {
            // 🔓 1. Decrypt
            byte[] encryptedBytes = Convert.FromBase64String(user.Encrypted);
            byte[] decryptedBytes = _rsa.Decrypt(encryptedBytes, RSAEncryptionPadding.OaepSHA256);
            string decryptedJson = Encoding.UTF8.GetString(decryptedBytes);

            var payload = JsonSerializer.Deserialize<DecryptedPayload>(decryptedJson);

            // 🧂 2. Hash password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(payload.Password);

            Console.WriteLine("✅ Email: " + payload.Email);
            Console.WriteLine("🔐 Hashed password: " + hashedPassword);

            // ✅ 3. (Optional) Store to database here...

            return Ok(new { message = "User registered successfully!" });
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ Decryption error: " + ex.Message);
            return BadRequest(new { error = "Invalid payload" });
        }
    }
}
