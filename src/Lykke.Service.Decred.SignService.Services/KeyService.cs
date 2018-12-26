using Lykke.Service.BlockchainApi.Contract.Wallets;
using Lykke.Service.Decred.SignService.Core.Services;
using NDecred.Common;

namespace Lykke.Service.Decred.SignService.Services
{
    /// <summary>
    /// Creates keys that can be used in transactions.
    /// </summary>
    public class KeyService : IKeyService
    {
        private readonly Network _network;
        private readonly ISecurityService _securityService;

        public KeyService(ISecurityService securityService, Network network)
        {
            _securityService = securityService;
            _network = network;
        }

        public WalletResponse Create()
        {
            byte[] privateKey;

            do
            {
                // Sometimes security service generates private key, with length != 32
                // So, we repeat private key generation, until it meet length requirements
                privateKey = _securityService.NewPrivateKey();
                
            } while (privateKey.Length != 32);
            
            var publicKey = _securityService.GetPublicKey(privateKey, true);

            return new WalletResponse
            {
                PrivateKey = GetWif(privateKey),
                PublicAddress = GetPublicAddress(publicKey)
            };
        }

        private string GetWif(byte[] privateKey)
        {
            return Wif.Serialize(_network, ECDSAType.ECTypeSecp256k1, false, privateKey);
        }

        private string GetPublicAddress(byte[] publicKey)
        {
            var prefix = _network.AddressPrefix.PayToPublicKeyHash;
            var pubKeyHash = HashUtil.Ripemd160(HashUtil.Blake256(publicKey));
            return new Base58Check().Encode(prefix, pubKeyHash, false);
        }
    }
}
