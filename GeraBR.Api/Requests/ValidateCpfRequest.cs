using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GeraBR.Api.Requests;

public class ValidateCpfRequest
{
    [Required] public string Cpf { get; set; }
}