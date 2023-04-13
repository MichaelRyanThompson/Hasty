using Hasty.Models.Requests.PatriotPoints;
using System.ComponentModel.DataAnnotations;

namespace Hasty.Models.Requests.PatriotPoints
{
    public class PatriotPointsSourceUpdateRequest : PatriotPointsSourceAddRequest, IModelIdentifier
    {
        [Required]
        public int Id { get; set; }
    }
}
