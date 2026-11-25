using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Demo.Services;

namespace WebApi.Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly ITransactionProcessingService _transactionProcessingService_1;
        private readonly ITransactionProcessingService _transactionProcessingService_2;

        public TransferController(ITransactionProcessingService transactionProcessingService_1, ITransactionProcessingService transactionProcessingService_2)
        {
            _transactionProcessingService_1 = transactionProcessingService_1;
            _transactionProcessingService_2 = transactionProcessingService_2;
        }
        [HttpPost]
        public IActionResult Transfer(string transactionData)
        {
            Console.WriteLine( $"P1 = {_transactionProcessingService_1.GetHashCode()}");
            Console.WriteLine($"P2 = {_transactionProcessingService_2.GetHashCode()}");
            var result1 = _transactionProcessingService_1.ProcessTransaction(transactionData);
            var result2 = _transactionProcessingService_2.ProcessTransaction(transactionData);
            return Ok(new { result1 });
        }

    }
}
