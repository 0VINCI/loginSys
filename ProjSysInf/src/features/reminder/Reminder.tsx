import { useState } from 'react';
import { Form, Alert, Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { passwordReminder, changePassword } from '../common/services/authService';
import './Reminder.css';

const Reminder = () => {
  const [email, setEmail] = useState<string>('');
  const [codeSent, setCodeSent] = useState<boolean>(false);
  const [resetCode, setResetCode] = useState<string>('');
  const [newPassword, setNewPassword] = useState<string>('');
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);

  const navigate = useNavigate();

  const handleSubmitReminder = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    try {
      const data = await passwordReminder(email);
      
      if (data.status >= 200 && data.status < 300) {
        setError(null);
        setCodeSent(true);
      } else {
        setError(data.data.error || 'Wystąpił błąd. Spróbuj ponownie później.');
      }
    } catch (error) {
      setError('Wystąpił problem. Spróbuj ponownie później.');
    }
  };

  const handleChangePassword = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    try {
      const data = await changePassword(email, resetCode, newPassword);
      
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
        <h2>Przypomnienie hasła</h2>
        {error && <Alert variant="danger">{error}</Alert>}
        {success && <Alert variant="success">{success}</Alert>}

        {!codeSent ? (
          <Form onSubmit={handleSubmitReminder}>
            <Form.Group controlId="formBasicEmail" className="form-group">
              <Form.Label>Email</Form.Label>
              <Form.Control
                type="email"
                placeholder="Wprowadź email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
              />
            </Form.Group>

            <Button 
              className="button-custom" 
              type="submit"
            >
              Przypomnij hasło
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
        ) : (
          <Form onSubmit={handleChangePassword}>
            <Form.Group controlId="formBasicCode" className="form-group">
              <Form.Label>Kod z e-maila</Form.Label>
              <Form.Control
                type="text"
                placeholder="Wprowadź kod"
                value={resetCode}
                onChange={(e) => setResetCode(e.target.value)}
              />
            </Form.Group>

            <Form.Group controlId="formBasicNewPassword" className="form-group">
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
        )}
      </div>
    </div>
  );
};

export default Reminder;
