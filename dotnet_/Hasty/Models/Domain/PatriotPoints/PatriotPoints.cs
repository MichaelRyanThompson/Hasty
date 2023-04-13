using Models.Domain.PatriotPoints;
using System;

namespace Hasty.Models.Domain.PatriotPoints
{
    public class PatriotPoints
    {
        public int Id { get; set; }
        public int SourceId { get; set; }
        public int Points { get; set; }
        public DateTime DateCreated { get; set; }
        public PatriotPointsSource PatriotPointsSource { get; set; }

    }
}
