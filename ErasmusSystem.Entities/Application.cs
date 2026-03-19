using System;
using System.ComponentModel.DataAnnotations;

namespace ErasmusSystem.Entities
{
    public class Application
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Term { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "DRAFT"; // DRAFT, SUBMITTED, APPROVED, REJECTED

        public decimal? TotalScore { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}