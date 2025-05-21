import { useEffect, useState } from 'react';
import { getPermissions } from '../services/permissionService';
import {
  Table,
  TableHead,
  TableRow,
  TableCell,
  TableBody
} from '@mui/material';

export default function PermissionList() {
  const [permissions, setPermissions] = useState([]);

  useEffect(() => {
    getPermissions().then((response) => setPermissions(response.data));
  }, []);

  return (
    <Table>
      <TableHead>
        <TableRow>
          <TableCell>Name</TableCell>
          <TableCell>LastName</TableCell>
          <TableCell>Type</TableCell>
          <TableCell>Date</TableCell>
        </TableRow>
      </TableHead>
      <TableBody>
        {permissions.map((p: any) => (
          <TableRow key={p.id}>
            <TableCell>{p.employeeName}</TableCell>
            <TableCell>{p.employeeLastName}</TableCell>
            <TableCell>{p.permissionTypeDescription || p.permissionTypeId}</TableCell>
            <TableCell>{p.permissionDate?.split('T')[0]}</TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  );
}
