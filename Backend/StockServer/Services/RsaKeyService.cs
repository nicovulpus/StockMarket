using System.Security.Cryptography;

public class RsaKeyService
{
    private readonly RSA _rsa;

    public RsaKeyService()
    {
        _rsa = RSA.Create(2048);
    }

    public RSAParameters GetPublicKey() => _rsa.ExportParameters(false);
    public RSA GetPrivateKey() => _rsa;
    public byte[] Decrypt(byte[] encryptedData, RSAEncryptionPadding padding)
{
    return _rsa.Decrypt(encryptedData, padding);
}

}

