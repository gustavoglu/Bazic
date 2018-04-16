using System;

namespace Bazic.Domain.Core.Entitys
{
    public abstract class EntityBase
    {
        protected EntityBase()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string CriadoPor { get; set; }
        public DateTime? CriadoEm { get; set; }
        public string AtualizadoPor { get; set; }
        public DateTime? AtualizadoEm { get; set; }
        public string DeletadoPor { get; set; }
        public DateTime? DeletadoEm { get; set; }
        public bool Deletado { get; set; } = false;
    }
}
