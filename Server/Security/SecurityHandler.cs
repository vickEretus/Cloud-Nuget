using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Server.Security;

public static class SecurityHandler
{
    private static readonly RandomNumberGenerator RandomNumberGenerator = RandomNumberGenerator.Create();

    public static readonly SymmetricSecurityKey AuthorizationSigningTokenKey = GenerateSymmetricKey(Config.AuthSecretLength);
    public static readonly SigningCredentials AuthorizationSigningCredentials = new(AuthorizationSigningTokenKey, SecurityAlgorithms.HmacSha256Signature);

    private static byte[] GenerateRandomBytes(int length)
    {
        byte[] randomBytes = new byte[length];
        RandomNumberGenerator.GetBytes(randomBytes);
        return randomBytes;
    }

    private static SymmetricSecurityKey GenerateSymmetricKey(int length) => new(GenerateRandomBytes(length));
}
