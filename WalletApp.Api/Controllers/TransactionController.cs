using Microsoft.AspNetCore.Mvc;
using WalletApp.Application.Interfaces;
using WalletApp.Application.Models;


namespace WalletApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetTransactionsList( string userId)
        {

            return Ok(await _transactionService.GetTransactionsAsync(userId));
        }

        [HttpGet("transaction/{id}")]
        public async Task<IActionResult> GetTransactionById( int id)
        {
            return Ok(await _transactionService.GetTransactionByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction(CreateTransactionModel model)
        {
            await _transactionService.CreateTransactionAsync(model);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction( int id)
        {
            await _transactionService.DeleteTransactionAsync(id);

            return Ok();
        }
    }
}
