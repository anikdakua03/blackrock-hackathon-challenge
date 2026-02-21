using Blackrock_Hackathon.Interfaces;
using BlackRock_Hackathon.DTOs;
using BlackRock_Hackathon.Models;
using BlackRock_Hackathon.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlackRock_Hackathon.Controllers;

[ApiController]
[Route("/blackrock/challenge/v1/transactions")]
public sealed class TransactionsController : ControllerBase
{
    private readonly ILogger<TransactionBuilder> _logger;
    private readonly ITransactionBuilder _transactionBuilder;
    private readonly ITransactionValidator _transactionValidator;
    private readonly ITemporalConstraintsValidator _temporalConstraintsValidator;

    public TransactionsController(ILogger<TransactionBuilder> logger, ITransactionBuilder transactionBuilder, ITransactionValidator transactionValidator, ITemporalConstraintsValidator temporalConstraintsValidator)
    {
        _logger = logger;
        _transactionBuilder = transactionBuilder;
        _transactionValidator = transactionValidator;
        _temporalConstraintsValidator = temporalConstraintsValidator;
    }

    [HttpPost("parse")]
    public ActionResult<IEnumerable<Transaction>> ParseExpenses([FromBody] IEnumerable<ExpenseDTO> expenses)
    {
        IEnumerable<Transaction> transactions = _transactionBuilder.BuildFrom(expenses);

        return Ok(transactions);
    }


    [HttpPost("validator")]
    public ActionResult<TransactionValidatorResponse> ValidateTransactions([FromBody] TransactionValidatorRequest transactionValidatorRequest)
    {
        (List<Transaction> valids, List<InvalidTransaction> invalids) = _transactionValidator.Validate(transactionValidatorRequest);

        return Ok(new TransactionValidatorResponse(valids, invalids));
    }

    [HttpPost("filter")]
    public ActionResult<TransactionValidatorResponse> ValidateTransactionsWithFilter([FromBody] TransactionValidatorFilterRequest transactionValidatorFilterRequest)
    {
        (List<Transaction> valids, List<InvalidTransaction> invalids) = _temporalConstraintsValidator.ValidateWithFilter(transactionValidatorFilterRequest);

        return Ok(new TransactionValidatorResponse(valids, invalids));
    }
}
