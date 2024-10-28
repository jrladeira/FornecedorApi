using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FornecedorApi.Responses;

public class Response<T>
{
    public T? Dados { get; set; }

    public bool Status { get; set; } = true;
    
    public string Mensagem { get; set; } = string.Empty;
}
