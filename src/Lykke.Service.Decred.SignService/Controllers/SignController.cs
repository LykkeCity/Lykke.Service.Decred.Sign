using System;
using System.Linq;
using System.Net;
using Lykke.Common.Api.Contract.Responses;
using Lykke.Service.BlockchainApi.Contract.Transactions;
using Lykke.Service.Decred.SignService.Services;
using Microsoft.AspNetCore.Mvc;
using NDecred.Common;
using SignedTransactionResponse = Lykke.Service.Decred.SignService.Models.SignedTransactionResponse;

namespace Lykke.Service.Decred.SignService.Controllers
{
    [Route("api/[controller]")]
    public class SignController : Controller
    {
        private readonly ISigningService _signingService;

        public SignController(ISigningService signingService)
        {
            _signingService = signingService;
        }

        [HttpPost]
        public IActionResult Sign([FromBody] SignTransactionRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ErrorResponse.Create("ValidationError").AddModelStateErrors(ModelState));

            try
            {
                var txBytes = HexUtil.ToByteArray(request.TransactionContext);
                var result = _signingService.SignRawTransaction(request.PrivateKeys.ToArray(), txBytes);
                var response = new SignedTransactionResponse {
                    SignedTransaction = result
                };

                return Ok(response);
            }

            catch (Exception e)
            {
                return BadRequest(ErrorResponse.Create("SigningError").AddModelError("message", e.Message));
            }
        }
    }
}
