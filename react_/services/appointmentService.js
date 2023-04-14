import axios from 'axios';
import { onGlobalError, onGlobalSuccess, API_HOST_PREFIX } from './serviceHelpers';

const endpoint = `${API_HOST_PREFIX}/api/appointments`;

const getById = (id) => {
  const config = {
    method: 'GET',
    url: `${endpoint}/${id}`,
    crossdomain: true,
    headers: { 'Content-Type': 'application/json' },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getByUserId = (pageIndex, pageSize, userId) => {
  const config = {
    method: 'GET',
    url: `${endpoint}/createdBy/${userId}?pageIndex=${pageIndex}&pageSize=${pageSize}`,
    crossdomain: true,
    headers: { 'Content-Type': 'application/json' },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const add = (payload) => {
  const config = {
    method: 'POST',
    url: endpoint,
    data: payload,
    crossdomain: true,
    headers: { 'Content-Type': 'application/json' },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const update = (payload, id) => {
  const config = {
    method: 'PUT',
    url: `${endpoint}/${id}`,
    data: payload,
    crossdomain: true,
    headers: { 'Content-Type': 'application/json' },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const updateIsConfirmed = (payload, id) => {
  const config = {
    method: 'PUT',
    url: `${endpoint}/confirmed/${id}`,
    data: payload,
    crossdomain: true,
    headers: { 'Content-Type': 'application/json' },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const updateIsCanceled = (payload, id) => {
  const config = {
    method: 'PUT',
    url: `${endpoint}/canceled/${id}`,
    data: payload,
    crossdomain: true,
    headers: { 'Content-Type': 'application/json' },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const appointmentService = {
  getById,
  getByUserId,
  add,
  update,
  updateIsConfirmed,
  updateIsCanceled,
};

export default appointmentService;
