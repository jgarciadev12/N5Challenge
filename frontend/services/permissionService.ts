import axios from 'axios';

const API_BASE = 'http://localhost:5000/api';

export const getPermissions = () => axios.get(API_BASE +'/permissions');
export const getPermissionById = (id: number) => axios.get(`${API_BASE}/permissions/${id}`);
export const getPermissionTypes = () => axios.get(`${API_BASE}/permissiontypes`);
export const modifyPermission = (id: number, data: any) =>
  axios.put(`${API_BASE}/permissions/${id}`, data);

export const createPermission = (data: {
  employeeName: string;
  employeeLastName: string;
  permissionTypeId: number;
  permissionDate: string;
}) => axios.post(API_BASE + '/permissions', data);