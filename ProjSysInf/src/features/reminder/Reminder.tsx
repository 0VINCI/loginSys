import { useState } from 'react';
import { Form, Alert, Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { passwordReminder } from '../common/services/authService';

const Reminder = () => {
  const [email, setEmail] = useState<string>('');
  const [error, setError] = useState<string | null>(null);
  
  const navigate = useNavigate();

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    try {
      const data = await passwordReminder(email);
      
      if (data.status >= 200 && data.status < 300) {
        setError(null);
        navigate('/login');
      } else {
        setError(data.data.error || 'Wystąpił błąd. Spróbuj ponownie później.');
      }
    } catch (error) {
      setError('Wystąpił problem. Spróbuj ponownie później.');
    }
  };

  return (
    <div className="app-container">
      <h2>Przypomnienie hasła</h2>
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

        <Button 
          className="button-custom" 
          type="submit"
        >
          Przypomnij hasło
        </Button>
        <Button 
          className="button-custom" 
          onClick={() => {
            navigate('/login');
          }}
        >
          Wróć do strony logowania
        </Button>

      </Form>
    </div>
  );
};

export default Reminder;
