using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XGame.Domain.Entities;

namespace XGame.Infra.Persistence.Map
{
    public class MapJogo : EntityTypeConfiguration<Jogo>
    {
        public MapJogo()
        {
            ToTable("Jogo");

            Property(p => p.Nome).HasMaxLength(100).IsRequired();
            Property(p => p.Descricao).HasMaxLength(255).IsRequired();
            Property(p => p.Produtora).HasMaxLength(50);
            Property(p => p.Distribuidora).HasMaxLength(50);
            Property(p => p.Genero).HasMaxLength(50);
            Property(p => p.Site).HasMaxLength(200);
            //Property(p => p.Email.Endereco).HasMaxLength(200).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("UK_JOGADOR_EMAIL") { IsUnique = true })).HasColumnName("Email");
            //Property(p => p.Nome.PrimeiroNome).HasMaxLength(50).IsRequired().HasColumnName("PrimeiroNome");
            //Property(p => p.Nome.UltimoNome).HasMaxLength(50).IsRequired().HasColumnName("UltimoNome");
            //Property(p => p.Senha).IsRequired();
            //Property(p => p.Status).IsRequired();
        }
    }
}


