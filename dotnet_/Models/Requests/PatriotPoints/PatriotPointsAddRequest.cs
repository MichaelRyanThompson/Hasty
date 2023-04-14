using System;
using System.ComponentModel.DataAnnotations;

namespace Hasty.Models.Requests.PatriotPoints
{
    public class PatriotPointsAddRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int SourceId { get; set; }
    }
}
