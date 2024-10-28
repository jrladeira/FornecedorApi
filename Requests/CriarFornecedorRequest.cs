using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FornecedorApi.Requests;

public class CriarFornecedorRequest
{
    [Required(ErrorMessage = "O campo Nome é obrigatório")]
    public string Nome { get; set; } = null!;

    [Required(ErrorMessage = "O campo Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; } = null!;
}
