import { BrowserRouter, Routes, Route } from 'react-router-dom'
import './App.css'
import PrivateRoute from './features/common/privateRoute/PrivateRoute'
import Login from './features/login/Login'
import Register from './features/register/Register'
import Reminder from './features/reminder/Reminder'

function App() {

  return (
    <>
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<PrivateRoute><Register /></PrivateRoute>} />
        <Route path="/report" element={<PrivateRoute><Report /></PrivateRoute>} />
        <Route path="/reminder" element={<PrivateRoute><Reminder /></PrivateRoute>} />
      </Routes>
    </BrowserRouter>
    </>
  )
}

export default App
