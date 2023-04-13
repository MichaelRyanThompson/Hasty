using Hasty.Models.Domain.Listings;
using System;


namespace Hasty.Models.Domain.PatriotPoints
{
    public class PatriotPointsSource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PointsAwarded { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsExpired { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
}
