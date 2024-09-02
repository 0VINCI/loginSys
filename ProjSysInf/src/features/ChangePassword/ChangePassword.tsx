import React, { useState } from 'react';
import { Form, Alert, Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { changePassword } from '../common/services/authService';
import './ChangePassword.css';

const ChangePassword: React.FC = () => {
  const [email, setEmail] = useState<string>('');
  const [oldPassword, setOldPassword] = useState<string>('');
  const [newPassword, setNewPassword] = useState<string>('');
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);

  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    try {
      const data = await changePassword(email, oldPassword, newPassword);
      
      if (data.status >= 200 && data.status < 300) {
        setError(null);
        setSuccess('Hasło zostało pomyślnie zmienione. Zostaniesz przekierowany do strony logowania.');
        setTimeout(() => {
          navigate('/');
        }, 3000);
      } else {
        setError(data.data.error || 'Wystąpił błąd. Spróbuj ponownie później.');
      }
    } catch (error) {
      setError('Wystąpił problem. Spróbuj ponownie później.');
    }
  };

  return (
    <div className="app-container">
      <div className="card">
        <h2>Zmiana hasła</h2>
        {error && <Alert variant="danger" className="change-alert">{error}</Alert>}
        {success && <Alert variant="success" className="change-alert">{success}</Alert>}
        
        <Form onSubmit={handleSubmit}>
          <Form.Group controlId="formBasicEmail" className="form-group">
            <Form.Label>Email</Form.Label>
            <Form.Control
              placeholder="Wprowadź email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
            />
          </Form.Group>

          <Form.Group controlId="formOldPassword" className="form-group">
            <Form.Label>Stare hasło</Form.Label>
            <Form.Control
              type="password"
              placeholder="Wprowadź stare hasło"
              value={oldPassword}
              onChange={(e) => setOldPassword(e.target.value)}
            />
          </Form.Group>

          <Form.Group controlId="formNewPassword" className="form-group">
            <Form.Label>Nowe hasło</Form.Label>
            <Form.Control
              type="password"
              placeholder="Wprowadź nowe hasło"
              value={newPassword}
              onChange={(e) => setNewPassword(e.target.value)}
            />
          </Form.Group>

          <Button 
            className="button-custom" 
            type="submit"
          >
            Zmień hasło
          </Button>
          <Button 
            className="button-custom button-secondary" 
            onClick={() => {
              navigate('/');
            }}
          >
            Wróć do strony logowania
          </Button>
        </Form>
      </div>
    </div>
  );
};

export default ChangePassword;
