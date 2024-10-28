using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using FornecedorApi.Models;

namespace FornecedorApi.Data;

public partial class FornecedorContext : DbContext
{
    public FornecedorContext()
    {
    }

    public FornecedorContext(DbContextOptions<FornecedorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Fornecedor> Fornecedores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Fornecedor>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
