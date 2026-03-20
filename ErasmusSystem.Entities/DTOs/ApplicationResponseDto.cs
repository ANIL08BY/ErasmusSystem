using System;

namespace ErasmusSystem.Entities.DTOs
{
    public class ApplicationResponseDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Term { get; set; }
        public string Status { get; set; }
        public decimal? TotalScore { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}