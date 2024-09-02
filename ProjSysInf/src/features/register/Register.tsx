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
      if (response.status < 400) {
        setError(null);
        setSuccess('Poprawnie zarejestrowano. Zostaniesz przekierowany do strony logowania');
        console.log(success); 
                
        setTimeout(() => {
          navigate('/');
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
        setError('Hasła nie są takie same!');
      } else {
        setError('Nie udało się zarejestrować. Spróbuj ponownie.');
      }
      setSuccess(null);
    }
  }

  const handleBack = () => {
    navigate('/'); 
  }
  
  return (
    <div className="register-container">
      <h2 className="register-heading">Rejestracja</h2>
      {error && <Alert variant="danger" className="register-alert">{error}</Alert>}
      {success && <Alert variant="success" className="register-alert">{success}</Alert>}
      <Form onSubmit={handleSubmit}>
        <Form.Group controlId="formBasicEmail">
          <Form.Label className="register-label">Email</Form.Label>
          <Form.Control
            type="text"
            placeholder="Wprowadź login"
            value={email}
            onChange={(e) => setUsername(e.target.value)}
            className="register-input"
          />
        </Form.Group>

        <Form.Group controlId="formBasicPassword">
          <Form.Label className="register-label">Hasło</Form.Label>
          <Form.Control
            type="password"
            placeholder="Hasło"
            value={password}
            onChange={(e) => setEmail(e.target.value)}
            className="register-input"
          />
        </Form.Group>

        <Form.Group controlId="formConfirmPassword">
          <Form.Label className="register-label">Powtórz Hasło</Form.Label>
          <Form.Control
            type="password"
            placeholder="Powtórz hasło"
            value={confirmPassword}
            onChange={(e) => setConfirmPassword(e.target.value)}
            className="register-input"
          />
        </Form.Group>

        <Button 
          className="register-button"
          type="submit"
        >
          Zarejestruj się
        </Button>
        <Button 
          className="register-button register-back-button"
          type="button"
          onClick={handleBack} 
        >
          Wróć
        </Button>
      </Form>
    </div>
  );
};

export default Register;
