import './App.css'
import PrivateRoute from './features/common/privateRoute/PrivateRoute'
import Register from './features/register/Register'

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
