import { useState } from 'react';
import { Form, Alert, Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import './Login.css';
import { login } from '../common/services/authService';

const Login = () => {
  const [username, setUsername] = useState<string>('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState<string | null>(null);
  
  const navigate = useNavigate();

  const handleSubmit = async () => {
    try {
      const data = await login(username, password);
      
      if (data.userId) {
        navigate('/report');

        setError('');
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
            type="text"
            placeholder="Wprowadź email"
            value={username}
            onChange={(e:string) => setUsername(e)}
          />
        </Form.Group>

        <Form.Group controlId="formBasicPassword">
          <Form.Label>Hasło</Form.Label>
          <Form.Control
            type="password"
            placeholder="Hasło"
            value={password}
            onChange={(e:string) => setPassword(e)}
          />
        </Form.Group>

        <Button 
          className="button-custom" 
          onClick={() => {
            handleSubmit();
          }}
        >
          Zaloguj się
        </Button>
        <Button 
          className="button-custom" 
          onClick={() => {
            navigate('/register');
          }}
        >
          Zarejestruj się
        </Button>

      </Form>
    </div>
  );
};

export default Login;

