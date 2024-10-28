using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FornecedorApi.Models;

[Table("Fornecedor")]
public partial class Fornecedor
{
    [Key]
    public int Id { get; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;
}
