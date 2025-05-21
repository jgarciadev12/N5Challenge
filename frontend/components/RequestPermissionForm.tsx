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
  createPermission,
  getPermissionTypes,
} from '../services/permissionService';

interface Props {
  onSuccess?: () => void;
}

export default function RequestPermissionForm({ onSuccess }: Props) {
  const [form, setForm] = useState({
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

  useEffect(() => {
    const loadTypes = async () => {
      try {
        const res = await getPermissionTypes();
        setPermissionTypes(res.data);
      } catch {
        setError('Failed to load permission types');
      } finally {
        setLoading(false);
      }
    };
    loadTypes();
  }, []);

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
      await createPermission(form);
      setSuccess(true);
      if (onSuccess) onSuccess();
      setForm({
        employeeName: '',
        employeeLastName: '',
        permissionTypeId: 0,
        permissionDate: '',
      });
    } catch {
      setError('Failed to create permission');
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
          {submitting ? 'Saving...' : 'Request Permission'}
        </Button>
      </Stack>

      <Snackbar
        open={success}
        autoHideDuration={3000}
        onClose={() => setSuccess(false)}
      >
        <Alert severity="success">Permission requested successfully</Alert>
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
