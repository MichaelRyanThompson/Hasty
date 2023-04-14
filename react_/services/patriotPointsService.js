import axios from 'axios';
import { onGlobalError, onGlobalSuccess, API_HOST_PREFIX } from './serviceHelpers';

const endpoint = `${API_HOST_PREFIX}/api/patriotpoints`;

const getByUserIdTotals = (id) => {
  const config = {
    method: 'GET',
    url: `${endpoint}/totals/user/${id}`,
    crossdomain: true,
    headers: { 'Content-Type': 'application/json' },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const getByUserIdPaginated = (pageIndex, pageSize, userId) => {
  const config = {
    method: 'GET',
    url: `${endpoint}/user/${userId}?pageIndex=${pageIndex}&pageSize=${pageSize}`,
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

const sourceAdd = (payload) => {
  const config = {
    method: 'POST',
    url: `${endpoint}/source`,
    data: payload,
    crossdomain: true,
    headers: { 'Content-Type': 'application/json' },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const sourceUpdate = (payload, id) => {
  const config = {
    method: 'PUT',
    url: `${endpoint}/source/${id}`,
    data: payload,
    crossdomain: true,
    headers: { 'Content-Type': 'application/json' },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const updateIsDeleted = (payload, id) => {
  const config = {
    method: 'PUT',
    url: `${endpoint}/delete/${id}`,
    data: payload,
    crossdomain: true,
    headers: { 'Content-Type': 'application/json' },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const updateIsExpired = (payload, id) => {
  const config = {
    method: 'PUT',
    url: `${endpoint}/expired/${id}`,
    data: payload,
    crossdomain: true,
    headers: { 'Content-Type': 'application/json' },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const appointmentService = {
  getByUserIdTotals,
  getByUserIdPaginated,
  add,
  sourceAdd,
  sourceUpdate,
  updateIsDeleted,
  updateIsExpired,
};

export default appointmentService;
