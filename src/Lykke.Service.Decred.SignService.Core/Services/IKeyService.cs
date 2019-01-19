using Lykke.Service.BlockchainApi.Contract.Wallets;

namespace Lykke.Service.Decred.SignService.Core.Services
{
    public interface IKeyService
    {
        WalletResponse Create();
    }
}
