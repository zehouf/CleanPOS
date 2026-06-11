// src/CleanPOS.WebAPI/Controllers/SalesController.cs
namespace CleanPOS.WebAPI.Controllers;

using CleanPOS.Application.Sales.Commands.AddSaleLine;
using CleanPOS.Application.Sales.Commands.CancelSale;
using CleanPOS.Application.Sales.Commands.CompleteSale;
using CleanPOS.Application.Sales.Commands.CreateSale;
using CleanPOS.Application.Sales.Queries.GetDailyRevenue;
using CleanPOS.Application.Sales.Queries.GetSaleById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly IMediator _mediator;

    public SalesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetSaleByIdQuery(id), cancellationToken);
        return Ok(result);
    }

    [HttpGet("daily-revenue")]
    public async Task<IActionResult> GetDailyRevenue(
        [FromQuery] DateTime date,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetDailyRevenueQuery(date), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateSaleCommand command,
        CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpPost("{id:guid}/lines")]
    public async Task<IActionResult> AddLine(
        Guid id,
        AddSaleLineCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.SaleId)
            return BadRequest("SaleId mismatch.");

        var lineId = await _mediator.Send(command, cancellationToken);
        return Ok(lineId);
    }

    [HttpPost("{id:guid}/complete")]
    public async Task<IActionResult> Complete(
        Guid id,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new CompleteSaleCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpPost("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(
        Guid id,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new CancelSaleCommand(id), cancellationToken);
        return NoContent();
    }
}