using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Security.Cryptography;

namespace Server.Security;

public class SecurityHandler
{
    private readonly RandomNumberGenerator RandomNumberGenerator = RandomNumberGenerator.Create();

    public readonly SymmetricSecurityKey AuthorizationSigningTokenKey;
    public readonly SigningCredentials AuthorizationSigningCredentials;
    
    public SecurityHandler()
    {
        AuthorizationSigningTokenKey = GenerateSymmetricKey(Config.AuthSecretLength);
        AuthorizationSigningCredentials = new(AuthorizationSigningTokenKey, SecurityAlgorithms.HmacSha256Signature);
    }

    
    private byte[] GenerateRandomBytes(int length)
    {
        byte[] randomBytes = new byte[length];
        RandomNumberGenerator.GetBytes(randomBytes);
        return randomBytes;
    }

    private SymmetricSecurityKey GenerateSymmetricKey(int length) => new(GenerateRandomBytes(length));
}
