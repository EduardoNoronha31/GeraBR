using GeraBR.Application.UseCases.ValidateCpf;
using GeraBR.Api.Requests;
using GeraBR.Application.UseCases.GenerateCpf;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GeraBR.Api.Controllers;

[ApiController]
[Route("cpf")]
public class CpfController : ControllerBase
{
    private readonly ValidateCpfUseCase _validateCpfUseCase;
    private readonly GenerateCpfUseCase _generateCpfUseCase;

    public CpfController(ValidateCpfUseCase validateCpfUseCase, GenerateCpfUseCase generateCpfUseCase)
    {
        _validateCpfUseCase = validateCpfUseCase;
        _generateCpfUseCase = generateCpfUseCase;
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


    [SwaggerOperation(
        Summary = "It generates a valid CPF",
        Description = "It is only for testing purposes."
    )]
    [HttpPost("generate")]
    [ProducesResponseType(typeof(GenerateCpfOutput), 200)]
    [ProducesResponseType(400)]
    public IActionResult Generate()
    {
        var result = _generateCpfUseCase.Execute();
        return Ok(result);
    }
}