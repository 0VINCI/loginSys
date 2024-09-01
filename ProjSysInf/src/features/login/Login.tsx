import { useState } from 'react';
import { Form, Alert, Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import './Login.css';
import { login } from '../common/services/authService';

const Login = () => {
  const [email, setEmail] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [error, setError] = useState<string | null>(null);

  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    try {
      const { data, status } = await login(email, password);

      if (status === 200) {
        navigate('/report');
        setError(null);
      } else if (data) {
        setError(data.error);
      }
    } catch (error) {
      setError('Nie udało się zalogować. Sprawdź swoje dane logowania.');
    }
  };

  return (
    <div className="app-container">
      <h2>Logowanie</h2>
      {error && <Alert variant="danger">{error}</Alert>}
      <Form onSubmit={handleSubmit}>
        <Form.Group controlId="formBasicEmail">
          <Form.Label>Email</Form.Label>
          <Form.Control
            type="email"
            placeholder="Wprowadź email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </Form.Group>

        <Form.Group controlId="formBasicPassword">
          <Form.Label>Hasło</Form.Label>
          <Form.Control
            type="password"
            placeholder="Hasło"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </Form.Group>

        <Button 
          className="button-custom" 
          type="submit"
        >
          Zaloguj się
        </Button>
        <Button 
          className="button-custom" 
          variant="link"
          onClick={() => navigate('/register')}
        >
          Zarejestruj się
        </Button>
        <Button 
          className="button-custom" 
          variant="link"
          onClick={() => navigate('/password-reminder')}
        >
          Nie pamiętam hasła
        </Button>
      </Form>
    </div>
  );
};

export default Login;
