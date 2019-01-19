using System.Linq;
using Lykke.Service.BlockchainApi.Contract.Transactions;
using Lykke.Service.Decred.SignService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NDecred.Common;
using Xunit;

namespace Lykke.Service.Decred.SignService.Services.Tests
{
    public class SignControllerTests
    {
        [Fact]
        public void SignRawTransaction_GivenUnsignedTx_ReturnsCorrectlySignedTx()
        {
            var privateKeys = new[] {"private_key1", "private_key2"};
            var request = new SignTransactionRequest()
            {
                PrivateKeys = privateKeys,
                TransactionContext = "00000000"
            };

            var txBytes = HexUtil.ToByteArray(request.TransactionContext);

            var mockSigningService = new Mock<ISigningService>();
            mockSigningService.Setup(m => m.SignRawTransaction(It.IsAny<string[]>(), txBytes))
                .Returns("signed_tx");

            var signController = new SignController(mockSigningService.Object);

            var result = signController.Sign(request) as ObjectResult;
            var response = result.Value as SignedTransactionResponse;
            Assert.Equal("signed_tx", response.SignedTransaction);
        }
    }
}
