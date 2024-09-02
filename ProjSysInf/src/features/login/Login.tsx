import { useState } from 'react';
import { Form, Alert, Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { login } from '../common/services/authService';
import './Login.css';

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
        localStorage.setItem('userId', data.userId); 
        console.log(data.history); // Debug: Sprawdź, czy dane historyczne są dostępne
        navigate('/report', { state: { history: data.history } });
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
      <div className="card">
        <h2>Logowanie</h2>
        {error && <Alert variant="danger" className="alert alert-danger">{error}</Alert>}
        <Form onSubmit={handleSubmit}>
          <Form.Group controlId="formBasicEmail" className="form-group">
            <Form.Label>Email</Form.Label>
            <Form.Control
              placeholder="Wprowadź email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
            />
          </Form.Group>

          <Form.Group controlId="formBasicPassword" className="form-group">
            <Form.Label>Hasło</Form.Label>
            <Form.Control
              type="password"
              placeholder="Wprowadź hasło"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
          </Form.Group>

          <Button className="button-custom" type="submit">
            Zaloguj się
          </Button>
        </Form>

        <div className="card-footer-text">
          <span>Nie pamiętasz hasła?</span>
          <Button 
            className="button-link" 
            onClick={() => navigate('/password-reminder')}
          >
            Przypomnij hasło
          </Button>
        </div>

        <div className="card-footer-text">
          <span>Nie masz konta?</span>
          <span className="link-custom" onClick={() => navigate('/register')}>
            Zarejestruj się
          </span>
        </div>
      </div>
    </div>
  );
};

export default Login;
