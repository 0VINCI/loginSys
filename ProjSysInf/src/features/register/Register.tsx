import React, { useState } from 'react';
import { Form, Alert, Button } from 'react-bootstrap';
import { userRegister } from '../../../services/registerService'; 
import { useNavigate } from 'react-router-dom';
import Logo from '../../Logo/Logo';
import './Register.css';

const Register = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');

  const navigate = useNavigate();

  const handleSubmit = async () => {

    if (password !== confirmPassword) {
      setError('Hasła nie są takie same');
      setSuccess('');
      return;
    }

    try {
      const user = { login: username, password };
      const response = await userRegister(user);

      if (response.status === 200) {
        setError('');
        setSuccess('Poprawnie zarejestrowano. Zostaniesz przekierowany do strony logowania');
        setTimeout(() => {
          navigate('/login');
        }, 3000);
      } else if (response.status === 409) {
        setError('Użytkownik już istnieje. Zaloguj się lub przypomnij hasło');
        setSuccess('');
      } else {
        setError('Wystąpił nieoczekiwany błąd.');
        setSuccess('');
      }
    } catch (error: any) {
      if (error.response && error.response.status === 409) {
        setError('Użytkownik już istnieje.');
      } else {
        setError('Nie udało się zarejestrować. Spróbuj ponownie.');
      }
      setSuccess('');
    }
  };

  return (
    <div className="app-container">
      <h2>Rejestracja</h2>
      {error && <Alert variant="danger">{error}</Alert>}
      {success && <Alert variant="success">{success}</Alert>}
      <Form onSubmit={handleSubmit}>
        <Logo />
        <Form.Group controlId="formBasicEmail">
          <Form.Label>Email</Form.Label>
          <Form.Control
            type="text"
            placeholder="Wprowadź login"
            value={username}
            onChange={(e:any) => setUsername(e.target.value)}
          />
        </Form.Group>

        <Form.Group controlId="formBasicPassword">
          <Form.Label>Hasło</Form.Label>
          <Form.Control
            type="password"
            placeholder="Hasło"
            value={password}
            onChange={(e:any) => setPassword(e.target.value)}
          />
        </Form.Group>

        <Form.Group controlId="formConfirmPassword">
          <Form.Label>Powtórz Hasło</Form.Label>
          <Form.Control
            type="password"
            placeholder="Powtórz hasło"
            value={confirmPassword}
            onChange={(e:any) => setConfirmPassword(e.target.value)}
          />
        </Form.Group>

        <Button 
          className="button-custom" 
          onClick={() => {
          }}
        >
          Zarejestruj się
        </Button>
      </Form>
    </div>
  );
};

export default Register;
