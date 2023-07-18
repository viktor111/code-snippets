public class Signer
{
    private readonly RSA _privateKey;
    private readonly RSA _publicKey;


    /// <summary>
    /// Creates a new instance of the <see cref="Signer"/> class.
    /// </summary>
    /// <param name="privateKeyFilePath"></param>
    /// <param name="privateKeyPassword"></param>
    /// <param name="publicKeyFilePath"></param>
    /// <exception cref="BoricaException"></exception>
    public Signer(string privateKeyFilePath, string privateKeyPassword, string publicKeyFilePath)
    {
        Validate(privateKeyFilePath, privateKeyPassword, publicKeyFilePath);

        var pfx = new X509Certificate2(privateKeyFilePath, privateKeyPassword, X509KeyStorageFlags.Exportable);
        _privateKey = pfx.GetRSAPrivateKey() ?? throw new BoricaException(PrivateKeyNotFound);

        var certificate = new X509Certificate2(publicKeyFilePath);
        _publicKey = certificate.GetRSAPublicKey() ?? throw new BoricaException(PublicKeyNotFound);
    }

    /// <summary>
    /// Creates a new instance of the <see cref="Signer"/> class.
    /// </summary>
    /// <param name="privateKeyPfx"></param>
    /// <param name="publicKeyFilePath"></param>
    /// <exception cref="BoricaException"></exception>
    public Signer(X509Certificate2 privateKeyPfx, string publicKeyFilePath)
    {
        Validate(publicKeyFilePath);

        _privateKey = privateKeyPfx.GetRSAPrivateKey() ?? throw new BoricaException(PrivateKeyNotFound);

        var certificate = new X509Certificate2(publicKeyFilePath);
        _publicKey = certificate.GetRSAPublicKey() ?? throw new BoricaException(PublicKeyNotFound);
    }

    public Signer(X509Certificate2 privateKeyPfx, X509Certificate2 publicKey)
    {
        _privateKey = privateKeyPfx.GetRSAPrivateKey() ?? throw new BoricaException(PrivateKeyNotFound);

        _publicKey = publicKey.GetRSAPublicKey() ?? throw new BoricaException(PublicKeyNotFound);
    }

    /// <summary>
    /// Signs the data with the private key using SHA256 and Pkcs1 padding
    /// </summary>
    /// <param name="data"></param>
    /// <returns>The signed data</returns>
    public byte[] CreateSignature(byte[] data)
    {
        return _privateKey.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
    }

    /// <summary>
    /// Veryfies that the signature is correct using the public key with SHA256 and Pkcs1 padding
    /// </summary>
    /// <param name="messageData"></param>
    /// <param name="signature"></param>
    /// <returns>true if the verification passes and false if not</returns>
    public bool VerifySignature(byte[] messageData, byte[] signature)
    {
        return _publicKey.VerifyData(messageData, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
    }

    private void Validate(string privateKeyFilePath, string privateKeyPassword, string publicKeyFilePath)
    {
        if (string.IsNullOrWhiteSpace(privateKeyFilePath))
        {
            throw new BoricaException(PrivateKeyFilePathError);
        }
        if (string.IsNullOrWhiteSpace(privateKeyPassword))
        {
            throw new BoricaException(PrivateKeyPasswordError);
        }
        if (string.IsNullOrWhiteSpace(publicKeyFilePath))
        {
            throw new BoricaException(PublicKeyFilePathError);
        }
    }

    private void Validate(string publicKeyFilePath)
    {
        if (string.IsNullOrWhiteSpace(publicKeyFilePath))
        {
            throw new BoricaException(PublicKeyFilePathError);
        }
    }
}