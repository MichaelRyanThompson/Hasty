import * as Yup from 'yup';

const appointmentSchema = Yup.object().shape({
  listingId: Yup.number().required('Required'),
  phone: Yup.string().max(50),
  startDateTime: Yup.date().required('Please select a Date'),
  time: Yup.string().required('Please select a Time'),
  userId: Yup.number().required('Required'),
});

export { appointmentSchema };
