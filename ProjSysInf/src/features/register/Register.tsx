import React, { useState } from 'react';
import { Form, Alert, Button } from 'react-bootstrap';
import { register } from '../common/services/authService'; 
import { useNavigate } from 'react-router-dom';
import './Register.css';

const Register: React.FC = () => {
  const [email, setUsername] = useState('');
  const [password, setEmail] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);

  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault(); 

    try {
      const data = { email, password, confirmPassword };
      const response = await register(data);
      if (response.status === 200) {
        setError(null);
        setSuccess('Poprawnie zarejestrowano. Zostaniesz przekierowany do strony logowania');
        setTimeout(() => {
          navigate('/login');
        }, 3000);
      } else if (response.status === 409) {
        setError('Użytkownik już istnieje. Zaloguj się lub przypomnij hasło');
        setSuccess(null);
      } else if (response.status === 400) {
        setError('Hasła nie są takie same!');
        setSuccess(null);
      } else {
        setError('Wystąpił nieoczekiwany błąd.');
        setSuccess(null);
      }
    } catch (error: any) {
      if (error.response && error.response.status === 409) {
        setError('Użytkownik już istnieje.');
      } else if (error.response && error.response.status === 400) {
        setError('Użytkownik już istnieje.');
      } else {
        setError('Nie udało się zarejestrować. Spróbuj ponownie.');
      }
      setSuccess(null);
    }
  }
  
  return (
    <div className="app-container">
      <h2>Rejestracja</h2>
      {error && <Alert variant="danger">{error}</Alert>}
      {success && <Alert variant="success">{success}</Alert>}
      <Form onSubmit={handleSubmit}>
        <Form.Group controlId="formBasicEmail">
          <Form.Label>Email</Form.Label>
          <Form.Control
            type="text"
            placeholder="Wprowadź login"
            value={email}
            onChange={(e) => setUsername(e.target.value)}
          />
        </Form.Group>

        <Form.Group controlId="formBasicPassword">
          <Form.Label>Hasło</Form.Label>
          <Form.Control
            type="password"
            placeholder="Hasło"
            value={password}
            onChange={(e) => setEmail(e.target.value)}
          />
        </Form.Group>

        <Form.Group controlId="formConfirmPassword">
          <Form.Label>Powtórz Hasło</Form.Label>
          <Form.Control
            type="password"
            placeholder="Powtórz hasło"
            value={confirmPassword}
            onChange={(e) => setConfirmPassword(e.target.value)}
          />
        </Form.Group>

        <Button 
          className="button-custom" 
          type="submit"
        >
          Zarejestruj się
        </Button>
      </Form>
    </div>
  );
};

export default Register;