using System.Security.Cryptography;

public class RsaKeyService
{
    private readonly RSA _rsa;

    public RsaKeyService()
    {
        _rsa = RSA.Create(2048);
    }

    public RSAParameters GetPublicKey() => _rsa.ExportParameters(false);
    public byte[] Decrypt(byte[] data, RSAEncryptionPadding padding) => _rsa.Decrypt(data, padding);
}


