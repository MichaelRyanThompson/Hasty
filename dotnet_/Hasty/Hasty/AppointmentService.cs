using Domain;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;


namespace Services
{
    public class AppointmentService : IAppointmentService
    {
        IDataProvider _data = null;
        static IBaseUserMapper _baseUserMapper = null;
        ILocationMapper _locationMapper = null;
        public AppointmentService(
            IDataProvider data,
            IBaseUserMapper baseUserMapper,
            ILocationMapper locationMapper)
        {
            _data = data;
            _baseUserMapper = baseUserMapper;
            _locationMapper = locationMapper;
        }

        public Appointment Get(int id)
        {
            string procName = "[dbo].[Appointment_SelectById]";
            Appointment appointment = null;
            _data.ExecuteCmd(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@Id", id);
            },
            singleRecordMapper: delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                appointment = MapSingleAppointment(reader, ref startingIndex);
            });
            return appointment;
        }

        public Paged<Appointment> GetByUserIdPaginated(int pageIndex, int pageSize, int userId)
        {
            Paged<Appointment> pagedList = null;
            List<Appointment> list = null;
            int totalCount = 0;
            string procName = "[dbo].[Appointments_SelectByUserId_Paginated]";

            _data.ExecuteCmd(procName, inputParamMapper: delegate (SqlParameterCollection col)

            {
                col.AddWithValue("@PageIndex", pageIndex);
                col.AddWithValue("@PageSize", pageSize);
                col.AddWithValue("@UserId", userId);
            },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    int startingIndex = 0;
                    Appointment aListing = MapSingleAppointment(reader, ref startingIndex);
                    totalCount = reader.GetSafeInt32(startingIndex++);

                    if (list == null)
                    {
                        list = new List<Appointment>();
                    }
                    list.Add(aListing);
                }
                );
            if (list != null)
            {
                pagedList = new Paged<Appointment>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }

        public AppointmentConfirmation GetAppointmentConfirmation(int userId, int listingId)
        {
            string procName = "[dbo].[AppointmentConfirmation]";

            AppointmentConfirmation appointment = null;

            _data.ExecuteCmd(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@UserId", userId);
                col.AddWithValue("@ListingId", listingId);
            },
            singleRecordMapper: delegate (IDataReader reader, short set)
            {
                if (appointment == null)
                {
                    appointment = new AppointmentConfirmation();
                }

                int startingIndex = 0;
                switch (set)
                {
                    case 0:

                        appointment.User = new UserProfile();
                        appointment.User.Id = reader.GetSafeInt32(startingIndex++);
                        appointment.User.FirstName = reader.GetSafeString(startingIndex++);
                        appointment.User.LastName = reader.GetSafeString(startingIndex++);
                        appointment.User.Mi = reader.GetSafeString(startingIndex++);
                        appointment.User.AvatarUrl = reader.GetSafeString(startingIndex++);
                        appointment.User.Email = reader.GetSafeString(startingIndex++);
                        appointment.User.Roles = reader.DeserializeObject<List<LookUp>>(startingIndex++);

                        break;
                    case 1:

                        appointment.Property = new Listing();
                        appointment.Property.HousingType = new LookUp();
                        appointment.Property.AccessType = new LookUp();
                        appointment.Property.ListingServices = new List<LookUp>();
                        appointment.Property.ListingAmenities = new List<LookUp>();
                        appointment.Property.Images = new List<Image>();

                        appointment.Property.Id = reader.GetSafeInt32(startingIndex++);
                        appointment.Property.InternalName = reader.GetSafeString(startingIndex++);
                        appointment.Property.Title = reader.GetSafeString(startingIndex++);
                        appointment.Property.ShortDescription = reader.GetSafeString(startingIndex++);
                        appointment.Property.Description = reader.GetSafeString(startingIndex++);
                        appointment.Property.BedRooms = reader.GetSafeInt16(startingIndex++);
                        appointment.Property.Baths = reader.GetSafeFloat(startingIndex++);
                        appointment.Property.HousingType.Id = reader.GetSafeInt32(startingIndex++);
                        appointment.Property.HousingType.Name = reader.GetSafeString(startingIndex++);
                        appointment.Property.AccessType.Id = reader.GetSafeInt32(startingIndex++);
                        appointment.Property.AccessType.Name = reader.GetSafeString(startingIndex++);

                        appointment.Property.ListingServices = reader.DeserializeObject<List<LookUp>>(startingIndex++);
                        appointment.Property.ListingAmenities = reader.DeserializeObject<List<LookUp>>(startingIndex++);
                        appointment.Property.GuestCapacity = reader.GetSafeInt16(startingIndex++);
                        appointment.Property.CostPerNight = reader.GetSafeInt32(startingIndex++);
                        appointment.Property.CostPerWeek = reader.GetSafeInt32(startingIndex++);

                        appointment.Property.CheckInTime = reader.GetSafeTimeSpan(startingIndex++);
                        appointment.Property.CheckOutTime = reader.GetSafeTimeSpan(startingIndex++);
                        appointment.Property.DaysAvailable = reader.GetSafeInt32(startingIndex++);

                        appointment.Property.Location = _locationMapper.MapLocation(reader, ref startingIndex);

                        appointment.Property.HasVerifiedOwnerShip = reader.GetSafeBool(startingIndex++);
                        appointment.Property.IsActive = reader.GetSafeBool(startingIndex++);
                        appointment.Property.Images = reader.DeserializeObject<List<Image>>(startingIndex++);


                        appointment.Property.CreatedBy = reader.GetSafeInt32(startingIndex++);
                        appointment.Property.DateCreated = reader.GetSafeDateTime(startingIndex++);
                        appointment.Property.DateModified = reader.GetSafeDateTime(startingIndex++);
                        break;
                    case 2:

                        appointment.ListingOwner = new UserProfile();
                        appointment.ListingOwner.Id = reader.GetSafeInt32(startingIndex++);
                        appointment.ListingOwner.FirstName = reader.GetSafeString(startingIndex++);
                        appointment.ListingOwner.LastName = reader.GetSafeString(startingIndex++);
                        appointment.ListingOwner.Mi = reader.GetSafeString(startingIndex++);
                        appointment.ListingOwner.AvatarUrl = reader.GetSafeString(startingIndex++);
                        appointment.ListingOwner.Email = reader.GetSafeString(startingIndex++);
                        appointment.ListingOwner.Roles = reader.DeserializeObject<List<LookUp>>(startingIndex++);
                        break;

                    default:
                        break;
                }

            });
            return appointment;
        }

        public int Add(AppointmentAddRequest model, int userId)
        {
            int id = 0;
            string procName = "[dbo].[Appointment_Insert]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@UserId", userId);
                AddCommonParams(model, col);
                SqlParameter idOut = new SqlParameter("@AppointmentId", SqlDbType.Int);
                idOut.Direction = ParameterDirection.Output;
                col.Add(idOut);
            },
            returnParameters: delegate (SqlParameterCollection returnCol)
            {
                object oId = returnCol["@AppointmentId"].Value;
                int.TryParse(oId.ToString(), out id);
            });
            return id;
        }

        public void Update(AppointmentUpdateRequest model, int userId)
        {
            string procName = "[dbo].[Appointment_Update]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@AppointmentId", model.Id);
                col.AddWithValue("@UserId", userId);
                AddCommonParams(model, col);
            },
            returnParameters: null);
        }

        public void UpdateIsConfirmed(int id, int modifiedBy)
        {
            string procName = "[dbo].[Appointment_Update_IsConfirmed]";

            _data.ExecuteNonQuery(procName
                , inputParamMapper: delegate (SqlParameterCollection col)
                {
                    col.AddWithValue("@AppointmentId", id);
                    col.AddWithValue("@ModifiedBy", modifiedBy);
                }, returnParameters: null);
        }

        public void UpdateIsCanceled(int id, int modifiedBy)
        {
            string procName = "[dbo].[Appointment_Update_IsCanceled]";

            _data.ExecuteNonQuery(procName
                , inputParamMapper: delegate (SqlParameterCollection col)
                {
                    col.AddWithValue("@AppointmentId", id);
                    col.AddWithValue("@ModifiedBy", modifiedBy);
                }, returnParameters: null);
        }

        private static Appointment MapSingleAppointment(IDataReader reader, ref int startingIndex)
        {
            Appointment appointment = new Appointment();
            BaseUser createdBy = new BaseUser();
            BaseUser modifiedBy = new BaseUser();

            appointment.Id = reader.GetSafeInt32(startingIndex++);
            appointment.Phone = reader.GetSafeString(startingIndex++);
            appointment.StartDateTime = reader.GetSafeDateTime(startingIndex++);
            appointment.Time = reader.GetSafeTimeSpan(startingIndex++);
            appointment.IsConfirmed = reader.GetSafeBool(startingIndex++);
            appointment.IsCanceled = reader.GetSafeBool(startingIndex++);
            appointment.DateCreated = reader.GetSafeDateTime(startingIndex++);
            appointment.DateModified = reader.GetSafeDateTime(startingIndex++);

            createdBy = _baseUserMapper.MapBaseUser(reader, ref startingIndex);
            appointment.CreatedBy = createdBy;

            modifiedBy = _baseUserMapper.MapBaseUser(reader, ref startingIndex);
            appointment.ModifiedBy = modifiedBy;

            appointment.Listing = new Listing();
            appointment.Listing.Id = reader.GetSafeInt32(startingIndex++);
            appointment.Listing.InternalName = reader.GetSafeString(startingIndex++);
            appointment.Listing.Title = reader.GetSafeString(startingIndex++);
            appointment.Listing.ShortDescription = reader.GetSafeString(startingIndex++);
            appointment.Listing.Description = reader.GetSafeString(startingIndex++);
            appointment.Listing.BedRooms = reader.GetSafeInt16(startingIndex++);
            appointment.Listing.Baths = reader.GetSafeFloat(startingIndex++);
            appointment.Listing.HousingType = new LookUp();
            appointment.Listing.HousingType.Id = reader.GetSafeInt32(startingIndex++);
            appointment.Listing.HousingType.Name = reader.GetSafeString(startingIndex++);
            appointment.Listing.AccessType = new LookUp();
            appointment.Listing.AccessType.Id = reader.GetSafeInt32(startingIndex++);
            appointment.Listing.AccessType.Name = reader.GetSafeString(startingIndex++);
            appointment.Listing.ListingServices = new List<LookUp>();
            appointment.Listing.ListingServices = reader.DeserializeObject<List<LookUp>>(startingIndex++);
            appointment.Listing.ListingAmenities = new List<LookUp>();
            appointment.Listing.ListingAmenities = reader.DeserializeObject<List<LookUp>>(startingIndex++);
            appointment.Listing.GuestCapacity = reader.GetSafeInt16(startingIndex++);
            appointment.Listing.CostPerNight = reader.GetSafeInt32(startingIndex++);
            appointment.Listing.CostPerWeek = reader.GetSafeInt32(startingIndex++);
            appointment.Listing.CheckInTime = reader.GetSafeTimeSpan(startingIndex++);
            appointment.Listing.CheckOutTime = reader.GetSafeTimeSpan(startingIndex++);
            appointment.Listing.DaysAvailable = reader.GetSafeInt32(startingIndex++);
            appointment.Listing.HasVerifiedOwnerShip = reader.GetSafeBool(startingIndex++);
            appointment.Listing.IsActive = reader.GetSafeBool(startingIndex++);
            appointment.Listing.CreatedBy = reader.GetSafeInt32(startingIndex++);
            appointment.Listing.DateCreated = reader.GetSafeDateTime(startingIndex++);


            return appointment;
        }

        private static void AddCommonParams(AppointmentAddRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@ListingId", model.ListingId);
            col.AddWithValue("@Phone", model.Phone);
            col.AddWithValue("@StartDateTime", model.StartDateTime);
            col.AddWithValue("@Time", model.Time);
        }
    }
}
