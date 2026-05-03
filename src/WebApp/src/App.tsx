import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import Login from './Users/Login';
import Register from './Users/Register';
import Home from './Home/Home';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Navigate to="/login" replace />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/home" element={<Home />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
