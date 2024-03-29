﻿using DesafioTarefas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioTarefas.Infra.Contexts.Mappings
{
    public class TarefaMap : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.HasMany(c => c.Comentarios)
                .WithOne(c => c.Tarefa)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Historico)
                .WithOne(c => c.Tarefa)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
