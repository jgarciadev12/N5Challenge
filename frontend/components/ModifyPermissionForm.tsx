import { useEffect, useState } from 'react';
import {
  TextField,
  Button,
  Stack,
  MenuItem,
  Snackbar,
  Alert,
  CircularProgress,
  Typography,
} from '@mui/material';
import {
  getPermissionById,
  getPermissionTypes,
  modifyPermission,
} from '../services/permissionService';

interface Props {
  permissionId: number;
  onSuccess?: () => void;
}

export default function ModifyPermissionForm({ permissionId, onSuccess }: Props) {
  const [form, setForm] = useState({
    id: 0,
    employeeName: '',
    employeeLastName: '',
    permissionTypeId: 0,
    permissionDate: '',
  });

  const [permissionTypes, setPermissionTypes] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [success, setSuccess] = useState(false);
  const [error, setError] = useState('');

  // Cargar datos del permiso y tipos
  useEffect(() => {
    const loadData = async () => {
      try {
        const [typesRes, permissionRes] = await Promise.all([
          getPermissionTypes(),
          getPermissionById(permissionId),
        ]);

        const p = permissionRes.data;

        setPermissionTypes(typesRes.data);
        setForm({
          id: p.id,
          employeeName: p.employeeName,
          employeeLastName: p.employeeLastName,
          permissionTypeId: p.permissionTypeId ?? 0,
          permissionDate: p.permissionDate.split('T')[0],
        });
      } catch (err) {
        setError('Error loading data. Please try again.');
      } finally {
        setLoading(false);
      }
    };

    loadData();
  }, [permissionId]);

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target;
    setForm({
      ...form,
      [name]: name === 'permissionTypeId' ? parseInt(value) : value,
    });
  };

  const handleSubmit = async () => {
    if (
      !form.employeeName.trim() ||
      !form.employeeLastName.trim() ||
      !form.permissionTypeId ||
      !form.permissionDate
    ) {
      setError('All fields are required');
      return;
    }

    try {
      setSubmitting(true);
      await modifyPermission(form.id, form);
      setSuccess(true);
      if (onSuccess) onSuccess();
    } catch {
      setError('Failed to save changes');
    } finally {
      setSubmitting(false);
    }
  };

  if (loading) {
    return (
      <Stack spacing={2} alignItems="center" mt={4}>
        <CircularProgress />
        <Typography>Loading form...</Typography>
      </Stack>
    );
  }

  return (
    <>
      <Stack spacing={2} sx={{ maxWidth: 400, mt: 2 }}>
        <TextField
          label="Name"
          name="employeeName"
          value={form.employeeName}
          onChange={handleChange}
          fullWidth
          required
        />
        <TextField
          label="Lastname"
          name="employeeLastName"
          value={form.employeeLastName}
          onChange={handleChange}
          fullWidth
          required
        />
        <TextField
          select
          label="Permission Type"
          name="permissionTypeId"
          value={form.permissionTypeId}
          onChange={handleChange}
          fullWidth
          required
        >
          {permissionTypes.map((type) => (
            <MenuItem key={type.id} value={type.id}>
              {type.description}
            </MenuItem>
          ))}
        </TextField>
        <TextField
          label="Date"
          name="permissionDate"
          type="date"
          value={form.permissionDate}
          onChange={handleChange}
          InputLabelProps={{ shrink: true }}
          fullWidth
          required
        />
        <Button
          variant="contained"
          onClick={handleSubmit}
          disabled={submitting}
        >
          {submitting ? 'Saving...' : 'Save Changes'}
        </Button>
      </Stack>

      <Snackbar
        open={success}
        autoHideDuration={3000}
        onClose={() => setSuccess(false)}
      >
        <Alert severity="success">Permission modified successfully</Alert>
      </Snackbar>

      <Snackbar
        open={!!error}
        autoHideDuration={4000}
        onClose={() => setError('')}
      >
        <Alert severity="error">{error}</Alert>
      </Snackbar>
    </>
  );
}
