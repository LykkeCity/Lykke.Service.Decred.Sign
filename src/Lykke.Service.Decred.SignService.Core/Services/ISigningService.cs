namespace Lykke.Service.Decred.SignService.Services
{
    public interface ISigningService
    {
        string SignRawTransaction(string[] privateKeys, byte[] rawTxBytes);
    }
}
