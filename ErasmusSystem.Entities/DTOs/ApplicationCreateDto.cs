using System;
using System.ComponentModel.DataAnnotations;

namespace ErasmusSystem.Entities.DTOs
{
    public class ApplicationCreateDto
    {
        [Required(ErrorMessage = "Kullanıcı ID zorunludur.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Dönem bilgisi zorunludur.")]
        [MaxLength(20)]
        public string Term { get; set; } // Örn: "2026-Bahar"
    }
}