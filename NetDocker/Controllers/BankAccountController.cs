using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BankAccountController : Controller
{
    private readonly IBankAccountService _bankAccountService;

    public BankAccountController(IBankAccountService bankAccountService)
    {
        _bankAccountService = bankAccountService;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BankAccountDto>> Get(Guid id)
    {
        var account = await _bankAccountService.GetByIdAsync(id);
        if (account == null) return NotFound();
        return Ok(account);
    }

    [HttpGet]
    public async Task<ActionResult<List<BankAccountDto>>> GetAll()
    {
        var accounts = await _bankAccountService.GetAllAsync();
        return Ok(accounts);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Add([FromBody] BankAccountAddDto dto)
    {
        var id = await _bankAccountService.AddAsync(dto);
        if (id == null) return BadRequest();
        return CreatedAtAction(nameof(Get), new { id }, id);
    }
}
