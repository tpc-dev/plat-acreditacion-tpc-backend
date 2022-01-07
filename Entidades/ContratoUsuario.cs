﻿using System.ComponentModel.DataAnnotations;

namespace PlatAcreditacionTPCBackend.Entidades
{
    public class ContratoUsuario
    {
        [Required]
        public int ContratoId { get; set; }
        public Contrato Contrato { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
    }
}
