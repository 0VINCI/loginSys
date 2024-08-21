import { useState } from 'react';
import { Form, Alert, Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { reminder } from '../common/services/authService';

const Reminder = () => {
  const [username, setUsername] = useState<string>('');
  const [error, setError] = useState('');
  
  const navigate = useNavigate();

  const handleSubmit = async () => {
    try {
      const data = await reminder(username);
      
      if (data.userId) {
        navigate('/login');

        setError('');
      } else if (data) {
        setError(data.error);
      }
    } catch (error) {
      setError('Wystąpił problem. Spróbuj ponownie później.');
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

        <Button 
          className="button-custom" 
          onClick={() => {
            handleSubmit();
          }}
        >
          Przypomnij hasło
        </Button>
        <Button 
          className="button-custom" 
          onClick={() => {
            navigate('/register');
          }}
        >
          Wróć do strony logowania
        </Button>

      </Form>
    </div>
  );
};

export default Reminder;

