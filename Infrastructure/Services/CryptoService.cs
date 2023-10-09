using Application.Abstracts.Common.Interfaces;
using Application.Extensions;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;

public class CryptoService : ICryptoService
{
    private readonly CryptoServiceOptions options;

    public CryptoService(IOptions<CryptoServiceOptions> options)
    {
        this.options = options.Value;
    }

    public string ToMd5(string value)
    {
        return value.ToMd5(options.SaltKey);
    }

    public string Encrypt(string value, bool appliedUrlEncode = false)   //123
    {
        return value.Encrypt(options.SymmetricKey, appliedUrlEncode);
    }

    public string Decrypt(string value)   //123
    {
        return value.Decrypt(options.SymmetricKey);
    }
}


public class CryptoServiceOptions
{
    public string SaltKey { get; set; } = null!;
    public string SymmetricKey { get; set; } = null!;
}
