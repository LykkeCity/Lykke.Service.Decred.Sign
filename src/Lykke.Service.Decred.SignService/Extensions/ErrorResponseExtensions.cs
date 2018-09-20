using System.Linq;
using Lykke.Common.Api.Contract.Responses;
using Microsoft.AspNetCore.Mvc.ModelBinding;

// Reused from: https://github.com/LykkeCity/Lykke.Service.Bitcoin.Sign

namespace Lykke.Service.Decred.SignService
{
    public static class ErrorResponseExtensions
    {
        public static ErrorResponse AddModelStateErrors(this ErrorResponse errorResponse,
            ModelStateDictionary modelState)
        {
            foreach (var state in modelState)
            {
                var messages = state.Value.Errors
                    .Where(e => !string.IsNullOrWhiteSpace(e.ErrorMessage))
                    .Select(e => e.ErrorMessage)
                    .Concat(state.Value.Errors
                        .Where(e => string.IsNullOrWhiteSpace(e.ErrorMessage))
                        .Select(e => e.Exception.Message))
                    .ToList();

                errorResponse.ModelErrors.Add(state.Key, messages);
            }

            return errorResponse;
        }
    }
}
