import { BrowserRouter, Routes, Route } from 'react-router-dom'
import './App.css'
import Login from './features/login/Login'
import Register from './features/register/Register'
import Reminder from './features/reminder/Reminder'
import Report from './features/Report/Report'
import ChangePassword from './features/ChangePassword/ChangePassword'

function App() {

  return (
    <>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/report" element={<Report />} />
        <Route path="/password-reminder" element={<Reminder />} />
        <Route path="/change-password" element={<ChangePassword />} />
      </Routes>
    </BrowserRouter>
    </>
  )
}

export default App
