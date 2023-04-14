import React from "react";
import { useFormik } from "formik";
import "./appointmentform.css";
import { Button, Form } from "react-bootstrap";
import { appointmentSchema } from "../../schemas/appointmentSchema";
import ReactDatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import setHours from "date-fns/setHours";
import setMinutes from "date-fns/setMinutes";
import PropTypes from "prop-types";
import { useParams } from "react-router";
import { useNavigate } from "react-router-dom";
import appointmentService from "../../services/appointmentService";
import Swal from "sweetalert2";
import { formatDateInput, convertTime12to24 } from "../../utils/dateFormater";

function AppointmentForm(props) {
  const listing = useParams();
  const navigate = useNavigate();
  const onFormSubmit = (values, resetForm) => {
    !props.currentUser.id ? navigate("/register") : addAppointment(values);
    resetForm(formik.initialValues);
  };

  const formik = useFormik({
    enableReinitialize: true,
    initialValues: {
      listingId: listing.id,
      phone: "",
      startDateTime: null,
      time: null,
      userId: props.currentUser.id,
    },
    onSubmit: onFormSubmit,
    validationSchema: appointmentSchema,
    validateOnChange: false,
  });

  const addAppointment = (values) => {
    values.startDateTime = formatDateInput(values.startDateTime);
    values.time = convertTime12to24(values.time);
    appointmentService.add(values).then(addSuccess).catch(addError);
  };

  const addSuccess = () => {
    Swal.fire({
      icon: "success",
      title: "Appointment Scheduled!",
      showConfirmButton: false,
      timer: 1500,
    });
  };

  const addError = () =>
    Swal.fire(
      "Uh-oh..",
      "There was an error adding this appointment.",
      "error"
    );

  return (
    <React.Fragment>
      <Form onSubmit={formik.handleSubmit}>
        <div className="mt-1 mb-1 d-flex justify-content-center w-100">
          <div
            className="bg-white border-0 px-1 py-2"
            style={{
              borderRadius: "12px",
              boxShadow: "rgba(0, 0, 0, 0.12) 0px 6px 16px",
            }}
          >
            <div className="card-body">
              <h2 className="card-title mb-3 d-flex flex-column text-center font-weight-bold">
                Schedule an Appointment
              </h2>
              <h4 className="information mt-4 d-flex flex-column text-center">
                Choose a date/time below that works the best with your schedule.
                Verify your email and phone number (mobile).
              </h4>
              <div className="row">
                <div className="col-sm-6 g-2">
                  <label htmlFor="startDateTime" />
                  <ReactDatePicker
                    className="form-control appointmentform-control"
                    name="startDateTime"
                    dateFormat="MM/dd/yyyy"
                    minDate={new Date()}
                    hideAddon={true}
                    value={formik.values.startDateTime}
                    onChange={(date) => {
                      let dateString = date.toLocaleDateString("en-IT");
                      formik.setFieldValue("startDateTime", dateString);
                    }}
                    placeholderText="Select Date"
                  />
                  {formik.touched.startDateTime &&
                  formik.errors.startDateTime ? (
                    <span className="appointmentform-error">
                      Please Select a Date
                    </span>
                  ) : null}
                </div>
                <div className="col-sm-6 g-2">
                  <label htmlFor="time" />
                  <ReactDatePicker
                    className="form-control appointmentform-control"
                    value={formik.values.time}
                    name="time"
                    dateFormat="h:mm"
                    onChange={(time) => {
                      let timeString = time.toLocaleTimeString("en-IT", {
                        timeStyle: "short",
                      });
                      formik.setFieldValue("time", timeString);
                    }}
                    showTimeSelect
                    showTimeSelectOnly
                    timeIntervals={30}
                    minTime={setHours(setMinutes(new Date(), 0), 9)}
                    maxTime={setHours(setMinutes(new Date(), 0), 21)}
                    timeCaption="Time"
                    hideAddon={true}
                    placeholderText="Select Time"
                  />
                  {formik.touched.time && formik.errors.time ? (
                    <span className="appointmentform-error">
                      Please Select a Time
                    </span>
                  ) : null}
                </div>
              </div>
              <div className="row">
                <div className="col-sm-6 g-2">
                  <div className="">
                    <div className="input-group">
                      {" "}
                      <input
                        className="form-control appointmentform-control"
                        type="text"
                        placeholder="Full Name"
                        name="fullName"
                        id="fullName"
                        value={
                          !props.currentUser.firstName &&
                          !props.currentUser.lastName
                            ? null
                            : props.currentUser.firstName +
                              " " +
                              props.currentUser.lastName
                        }
                      />{" "}
                    </div>
                  </div>
                </div>
                <div className="col-sm-6 g-2">
                  <div className="">
                    <div className="input-group">
                      {" "}
                      <input
                        className="form-control appointmentform-control"
                        type="text"
                        placeholder="(201) 555-0123"
                        name="phone"
                        id="phone"
                        value={formik.values.phone}
                        onChange={formik.handleChange}
                      />
                      {formik.touched.phone && formik.errors.phone ? (
                        <span className="appointmentform-error">
                          Please Enter A Phone Number
                        </span>
                      ) : null}{" "}
                    </div>
                  </div>
                </div>
              </div>
              <div className="row">
                <div className="col-sm-12 g-2">
                  <div className="">
                    <div className="input-group">
                      {" "}
                      <input
                        className="form-control appointmentform-control"
                        type="text"
                        name="email"
                        id="email"
                        placeholder="Email"
                        value={
                          !props.currentUser.email
                            ? null
                            : props.currentUser.email
                        }
                      />{" "}
                    </div>
                  </div>
                </div>
              </div>
              <div className=" d-flex flex-column text-center px-5 mt-3 mb-3">
                {" "}
                <small className="appointmentform-agree-text">
                  By scheduling this appointment you agree to the
                </small>{" "}
                <a href="/terms" className="appointmentform-terms">
                  Terms &amp; Conditions
                </a>{" "}
              </div>{" "}
              <div className="row">
                <div className="col-12">
                  <Button
                    className="btn btn-primary w-100"
                    style={{ borderRadius: "10px", height: "50px" }}
                    type="submit"
                  >
                    Confirm
                  </Button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </Form>
    </React.Fragment>
  );
}

AppointmentForm.propTypes = {
  currentUser: PropTypes.shape({
    id: PropTypes.number.isRequired,
    firstName: PropTypes.string.isRequired,
    lastName: PropTypes.string.isRequired,
    email: PropTypes.string.isRequired,
  }),
};

export default AppointmentForm;
