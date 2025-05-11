using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Text.Json;
using System.Security.Cryptography;
using BCrypt.Net;

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
            Console.WriteLine(" Received encrypted: " + user.Encrypted);
            byte[] encryptedBytes = Convert.FromBase64String(user.Encrypted);
            byte[] decryptedBytes = _rsa.Decrypt(encryptedBytes, RSAEncryptionPadding.OaepSHA256);
            string decryptedJson = Encoding.UTF8.GetString(decryptedBytes);
            Console.WriteLine("Decrypted: " + decryptedJson);
            var payload = JsonSerializer.Deserialize<DecryptedPayload>(decryptedJson);
            Console.WriteLine($"Email:  {payload.Email} / Password:  {payload.Password}");

            
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(payload.Password);

            Console.WriteLine("Email: " + payload.Email);
            Console.WriteLine(" Hashed password: " + hashedPassword);

            

            return Ok(new { message = "User registered successfully!" });
        }
        catch (Exception ex)
        {
            Console.WriteLine("Decryption error: " + ex.Message);
            return BadRequest(new { error = "Invalid payload" });
        }
    }
}
