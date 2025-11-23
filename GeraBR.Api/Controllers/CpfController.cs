using GeraBR.Application.UseCases.ValidateCpf;
using GeraBR.Api.Requests;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GeraBR.Api.Controllers;

[ApiController]
[Route("cpf")]
public class CpfController : ControllerBase
{
    private readonly ValidateCpfUseCase _validateCpfUseCase;

    public CpfController(ValidateCpfUseCase validateCpfUseCase)
    {
        _validateCpfUseCase = validateCpfUseCase;
    }

    [SwaggerOperation(
        Summary = "It validates the CPF",
        Description = "It receives a CPF (with or without a mask) and returns whether it is valid or not."
    )]
    [HttpPost("validate")]
    [ProducesResponseType(typeof(ValidateCpfOutput), 200)]
    [ProducesResponseType(400)]
    public IActionResult Validate([FromBody] ValidateCpfRequest request)
    {
        var result = _validateCpfUseCase.Execute(request.Cpf);
        return Ok(result);
    }
}