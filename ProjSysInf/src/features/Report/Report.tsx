import React, { useState } from 'react';
import { Table, Button, Pagination } from 'react-bootstrap';
import { useNavigate, useLocation } from 'react-router-dom';
import { logout } from '../common/services/authService';
import './Report.css';

const Report = () => {
  const location = useLocation();
  const history = location.state?.history?.$values || [];
  const navigate = useNavigate();

  const [currentPage, setCurrentPage] = useState(1);
  const itemsPerPage = 10;
  const userId = localStorage.getItem('userId');

  const handleLogout = async () => {
    try {
      await logout(userId);
      navigate('/login');
    } catch (error) {
      console.error('Nie udało się wylogować. Spróbuj ponownie.');
    }
  };

  const handleChangePassword = () => {
    navigate('/change-password');
  };

  const indexOfLastItem = currentPage * itemsPerPage;
  const indexOfFirstItem = indexOfLastItem - itemsPerPage;
  const currentItems = history.slice(indexOfFirstItem, indexOfLastItem);

  const totalPages = Math.ceil(history.length / itemsPerPage);

  const handlePageChange = (pageNumber: number) => {
    setCurrentPage(pageNumber);
  };

  return (
    <div className="app-container">
      <div className="card">
        <h2>Historia Operacji</h2>
        
        <Table striped bordered hover>
          <thead>
            <tr>
              <th>Typ Operacji</th>
              <th>Email</th>
              <th>Data i Czas</th>
            </tr>
          </thead>
          <tbody>
            {currentItems.length > 0 ? (
              currentItems.map((log: any, index: number) => (
                <tr key={index}>
                  <td>{log.operationTypeName}</td>
                  <td>{log.email}</td>
                  <td>{new Date(log.tmstmp).toLocaleString()}</td>
                </tr>
              ))
            ) : (
              <tr>
                <td colSpan={3} className="text-center">Brak danych do wyświetlenia</td>
              </tr>
            )}
          </tbody>
        </Table>

        <Pagination className='pagination'>
          {Array.from({ length: totalPages }, (_, i) => i + 1).map(number => (
            <Pagination.Item 
              key={number} 
              active={number === currentPage} 
              onClick={() => handlePageChange(number)}
            >
              {number}
            </Pagination.Item>
          ))}
        </Pagination>

        <Button 
          className="button-custom" 
          onClick={handleChangePassword}
        >
          Zmień hasło
        </Button>
        
        <Button 
          className="button-custom button-danger" 
          onClick={handleLogout}
        >
          Wyloguj się
        </Button>
      </div>
    </div>
  );
};

export default Report;
