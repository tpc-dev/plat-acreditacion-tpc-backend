﻿using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class EmpresaContrato
    {
        [Required]
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        [Required]
        public int ContratoId { get; set; }
        public Contrato Contrato { get; set; }
        [Required]
        public int EstadoAcreditacionId { get; set; }
        public EstadoAcreditacion EstadoAcreditacion { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
    }
}
