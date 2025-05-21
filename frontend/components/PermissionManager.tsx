import { useState } from 'react';
import {
  Container,
  Tabs,
  Tab,
  Box,
  Typography,
  Stack,
  TextField,
  Paper,
} from '@mui/material';

import RequestPermissionForm from './RequestPermissionForm';
import ModifyPermissionForm from './ModifyPermissionForm';
import PermissionList from './PermissionList';

export default function PermissionManager() {
  const [tab, setTab] = useState(0);
  const [selectedId, setSelectedId] = useState<number | null>(null);

  const handleChange = (_: React.SyntheticEvent, newValue: number) => {
    setTab(newValue);
  };

  return (
    <Container maxWidth="md" sx={{ mt: 1 }}>
      <Paper elevation={3} sx={{ p: 3 }}>
        <Tabs value={tab} onChange={handleChange} centered>
          <Tab label="Request Permission" />
          <Tab label="Modify Permission" />
          <Tab label="Permissions List" />
        </Tabs>

        <Box sx={{ mt: 2 }}>
          {tab === 0 && <RequestPermissionForm />}

          {tab === 1 && (
            <Stack spacing={2}>
              <Typography variant="subtitle1">
                Enter the ID of the permission to modify:
              </Typography>
              <TextField
                label="Permission ID"
                type="number"
                value={selectedId ?? ''}
                onChange={(e) => setSelectedId(Number(e.target.value))}
                sx={{ maxWidth: 200 }}
              />
              {selectedId && (
                <ModifyPermissionForm
                  permissionId={selectedId}
                  onSuccess={() => alert('Permission updated successfully!')}
                />
              )}
            </Stack>
          )}

          {tab === 2 && <PermissionList />}
        </Box>
      </Paper>
    </Container>
  );
}
