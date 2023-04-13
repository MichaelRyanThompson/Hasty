

using Hasty.Models.Domain.PatriotPoints;
using Hasty.Models;
using Hasty.Models.Requests.PatriotPoints;

namespace Hasty.Services.Interfaces
{
    public interface IPatriotPointsService
    {
        PatriotPointsTotals GetByUserIdRunningTotals(int userId);
        Paged<PatriotPoints> GetPatriotPointsByUserIdPaginated(int pageIndex, int pageSize, int userId);
        int AddPatriotPoints(PatriotPointsAddRequest model, int userId);
        int AddPatriotPointsSource(PatriotPointsSourceAddRequest model, int userId);
        void PatriotPointsSourceUpdate(PatriotPointsSourceUpdateRequest model, int userId);
        void PatriotPointsSourceUpdateIsDeleted(int id, int modifiedBy);
        void PatriotPointsSourceUpdateIsExpired(int id, int modifiedBy);
    }
}
