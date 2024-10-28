using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using FornecedorApi.Data;
using FornecedorApi.Models;
using FornecedorApi.Requests;
using FornecedorApi.Responses;

namespace FornecedorApi.Controllers;

[Route("api/fornecedores")]
[ApiController]
public class FornecedorController(FornecedorContext context) : ControllerBase
{
    private readonly FornecedorContext _context = context;

    [HttpGet]
    public async Task<Response<List<Fornecedor>>> ListarFornecedores()
    {
        var resposta = new Response<List<Fornecedor>>();

        try
        {
            var fornecedores = await _context.Fornecedores.ToListAsync();

            resposta.Dados = fornecedores; 
            resposta.Mensagem = "OK";
        }
        catch (Exception ex)
        {
            resposta.Status = false;
            resposta.Mensagem = ex.Message;
        }

        return resposta;
    }

    [HttpGet("{id}")]
    public async Task<Response<Fornecedor>> BuscarFornecedorPorId(int id)
    {
        var resposta = new Response<Fornecedor>();

        try
        {
            var fornecedor = await _context.Fornecedores.FirstOrDefaultAsync(f => f.Id == id);

            if (fornecedor == null)
            {
                resposta.Status = false;
                resposta.Mensagem = "Fornecedor não localizado";
            }
            else
            {
                resposta.Dados = fornecedor; 
                resposta.Mensagem = "OK";
            }
        }
        catch (Exception ex)
        {
            resposta.Status = false;
            resposta.Mensagem = ex.Message;
        }

        return resposta;
    }

    [HttpPost]
    public async Task<Response<Fornecedor>> CriarFornecedor(CriarFornecedorRequest req)
    {
        var resposta = new Response<Fornecedor>();

        if (!ModelState.IsValid)
        {
            foreach (var item in ModelState)
            {
                if (item.Value.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    resposta.Status = false;
                    resposta.Mensagem = item.Value.Errors.First().ErrorMessage;

                    return resposta;
                }
            }
        }

        try
        {
            var fornecedor = new Fornecedor {
                Nome = req.Nome,
                Email = req.Email
            };

            _context.Fornecedores.Add(fornecedor);

            await _context.SaveChangesAsync();

            resposta.Dados = fornecedor;
            resposta.Mensagem = "Fornecedor criado com sucesso";
        }
        catch (Exception ex)
        {
            resposta.Status = false;
            resposta.Mensagem = ex.Message;
        }

        return resposta;
    }

    [HttpPut("{id}")]
    public async Task<Response<Fornecedor>> EditarFornecedor(int id, EditarFornecedorRequest req)
    {
        var resposta = new Response<Fornecedor>();

        if (!ModelState.IsValid)
        {
            foreach (var item in ModelState)
            {
                if (item.Value.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    resposta.Status = false;
                    resposta.Mensagem = item.Value.Errors.First().ErrorMessage;

                    return resposta;
                }
            }
        }

        if (id != req.Id)
        {
            resposta.Status = false;
            resposta.Mensagem = "Id inválido";

            return resposta;
        }

        try
        {
            var fornecedor = await _context.Fornecedores.FirstOrDefaultAsync(f => f.Id == id);

            if (fornecedor == null)
            {
                resposta.Status = false;
                resposta.Mensagem = "Fornecedor não localizado";
            }
            else
            {
                fornecedor.Nome = req.Nome;
                fornecedor.Email = req.Email;

                _context.Entry(fornecedor).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                resposta.Dados = fornecedor;
                resposta.Mensagem = "Fornecedor editado com sucesso";
            }
        }
        catch (Exception ex)
        {
            resposta.Status = false;
            resposta.Mensagem = ex.Message;
        }

        return resposta;
    }

    [HttpDelete("{id}")]
    public async Task<Response<Fornecedor>> ExcluirFornecedor(int id)
    {
        var resposta = new Response<Fornecedor>();

        try
        {
            var fornecedor = await _context.Fornecedores.FirstOrDefaultAsync(f => f.Id == id);

            if (fornecedor == null)
            {
                resposta.Status = false;
                resposta.Mensagem = "Fornecedor não localizado";
            }
            else
            {
                _context.Fornecedores.Remove(fornecedor);

                await _context.SaveChangesAsync();

                resposta.Mensagem = "Fornecedor excluído com sucesso";
            }
        }
        catch (Exception ex)
        {
            resposta.Status = false;
            resposta.Mensagem = ex.Message;
        }

        return resposta;
    }
}
