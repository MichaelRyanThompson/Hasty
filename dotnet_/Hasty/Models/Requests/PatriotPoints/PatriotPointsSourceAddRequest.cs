using System;
using System.ComponentModel.DataAnnotations;

namespace Hasty.Models.Requests.PatriotPoints
{
    public class PatriotPointsSourceAddRequest
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int PointsAwarded { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 2)]
        public string Description { get; set; }
        [StringLength(255, MinimumLength = 2)]
        public string ImageUrl { get; set; }

    }
}
