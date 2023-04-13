using Hasty.Models;
using Hasty.Services.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Hasty.Data.Providers;
using Hasty.Data;
using Hasty.Models.Domain.PatriotPoints;
using Hasty.Models.Requests.PatriotPoints;
using Hasty.Models.Domain;


namespace Hasty.Services
{
    public class PatriotPointsService : IPatriotPointsService
    {

        IDataProvider _data = null;

        public PatriotPointsService(
            IDataProvider data)
        {
            _data = data;
        }

        public PatriotPointsTotals GetByUserIdRunningTotals(int userId)
        {
            string procName = "[dbo].[PatriotPoints_SelectByUserId_RunningTotals]";
            PatriotPointsTotals patriotPointsTotals = null;
            _data.ExecuteCmd(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@UserId", userId);
            },
            singleRecordMapper: delegate (IDataReader reader, short set)
            {
                patriotPointsTotals = new PatriotPointsTotals();
                int startingIndex = 0;
                patriotPointsTotals.TotalLifetimePoints = reader.GetSafeInt32(startingIndex++);
                patriotPointsTotals.TotalPointsRedeemed = reader.GetSafeInt32(startingIndex++);
                patriotPointsTotals.TotalAvailablePoints = reader.GetSafeInt32(startingIndex++);
            });
            return patriotPointsTotals;
        }

        public Paged<PatriotPoints> GetPatriotPointsByUserIdPaginated(int pageIndex, int pageSize, int userId)
        {
            Paged<PatriotPoints> pagedList = null;
            List<PatriotPoints> list = null;
            int totalCount = 0;
            string procName = "[dbo].[PatriotPoints_SelectByUserId_Paginated]";

            _data.ExecuteCmd(procName, inputParamMapper: delegate (SqlParameterCollection col)

            {
                col.AddWithValue("@PageIndex", pageIndex);
                col.AddWithValue("@PageSize", pageSize);
                col.AddWithValue("@UserId", userId);
            },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    int startingIndex = 0;
                    PatriotPoints aPatriotPoint = MapSinglePatriotPoint(reader, ref startingIndex);
                    if (totalCount == 0)
                    {
                        totalCount = reader.GetSafeInt32(startingIndex++);
                    }
                    if (list == null)
                    {
                        list = new List<PatriotPoints>();
                    }
                    list.Add(aPatriotPoint);
                });
            if (list != null)
            {
                pagedList = new Paged<PatriotPoints>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }

        public int AddPatriotPoints(PatriotPointsAddRequest model, int userId)
        {
            int id = 0;
            string procName = "[dbo].[PatriotPoints_Insert]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@UserId", model.UserId);
                col.AddWithValue("@SourceId", model.SourceId);
                SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                idOut.Direction = ParameterDirection.Output;
                col.Add(idOut);
            },
            returnParameters: delegate (SqlParameterCollection returnCol)
            {
                object oId = returnCol["@Id"].Value;
                int.TryParse(oId.ToString(), out id);
            });
            return id;
        }

        public int AddPatriotPointsSource(PatriotPointsSourceAddRequest model, int userId)
        {
            int id = 0;
            string procName = "[dbo].[PatriotPointsSource_Insert]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@UserId", userId);
                AddCommonPatriotPointsSourceParams(model, col);
                SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                idOut.Direction = ParameterDirection.Output;
                col.Add(idOut);
            },
            returnParameters: delegate (SqlParameterCollection returnCol)
            {
                object oId = returnCol["@Id"].Value;
                int.TryParse(oId.ToString(), out id);
            });
            return id;
        }

        public void PatriotPointsSourceUpdate(PatriotPointsSourceUpdateRequest model, int userId)
        {
            string procName = "[dbo].[PatriotPointsSource_Update]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@Id", model.Id);
                col.AddWithValue("@ModifiedBy", userId);
                AddCommonPatriotPointsSourceParams(model, col);
            },
            returnParameters: null);
        }

        public void PatriotPointsSourceUpdateIsDeleted(int id, int modifiedBy)
        {
            string procName = "[dbo].[PatriotPointsSource_Update_IsDeleted]";

            _data.ExecuteNonQuery(procName
                , inputParamMapper: delegate (SqlParameterCollection col)
                {
                    col.AddWithValue("@Id", id);
                    col.AddWithValue("@ModifiedBy", modifiedBy);
                }, returnParameters: null);
        }

        public void PatriotPointsSourceUpdateIsExpired(int id, int modifiedBy)
        {
            string procName = "[dbo].[PatriotPointsSource_Update_IsExpired]";

            _data.ExecuteNonQuery(procName
                , inputParamMapper: delegate (SqlParameterCollection col)
                {
                    col.AddWithValue("@Id", id);
                    col.AddWithValue("@ModifiedBy", modifiedBy);
                }, returnParameters: null);
        }

        private static PatriotPoints MapSinglePatriotPoint(IDataReader reader, ref int startingIndex)
        {
            PatriotPoints patriotPoints = new PatriotPoints();

            patriotPoints.Id = reader.GetSafeInt32(startingIndex++);
            patriotPoints.SourceId = reader.GetSafeInt32(startingIndex++);
            patriotPoints.Points = reader.GetSafeInt32(startingIndex++);
            patriotPoints.DateCreated = reader.GetSafeDateTime(startingIndex++);
            patriotPoints.PatriotPointsSource = new PatriotPointsSource();
            patriotPoints.PatriotPointsSource.Id = reader.GetSafeInt32(startingIndex++);
            patriotPoints.PatriotPointsSource.Name = reader.GetSafeString(startingIndex++);
            patriotPoints.PatriotPointsSource.PointsAwarded = reader.GetSafeInt32(startingIndex++);
            patriotPoints.PatriotPointsSource.Description = reader.GetSafeString(startingIndex++);
            patriotPoints.PatriotPointsSource.ImageUrl = reader.GetSafeString(startingIndex++);
            patriotPoints.PatriotPointsSource.IsExpired = reader.GetSafeBool(startingIndex++);
            patriotPoints.PatriotPointsSource.IsDeleted = reader.GetSafeBool(startingIndex++);
            patriotPoints.PatriotPointsSource.DateCreated = reader.GetSafeDateTime(startingIndex++);
            patriotPoints.PatriotPointsSource.DateModified = reader.GetSafeDateTime(startingIndex++);

            return patriotPoints;
        }

        private static void AddCommonPatriotPointsSourceParams(PatriotPointsSourceAddRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@Name", model.Name);
            col.AddWithValue("@PointsAwarded", model.PointsAwarded);
            col.AddWithValue("@Description", model.Description);
            col.AddWithValue("@ImageUrl", model.ImageUrl);
        }
    }
}
