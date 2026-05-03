// src/WebApp/src/App.tsx
import { useEffect, useState } from 'react';
import Login from './Users/Login'

function App() {
  const [data, setData] = useState<any>(null);
  // Vite looks for variables prefixed with VITE_
  const API_BASE_URL = import.meta.env.VITE_API_URL || 'http://localhost:5000';

  useEffect(() => {
    fetch(`${API_BASE_URL}/weatherforecast`)
      .then(res => res.json())
      .then(json => setData(json))
      .catch(err => console.error("API Call failed:", err));
  }, []);

  return (
    <div style={{ padding: '2rem', backgroundColor: '#282c34', color: 'white', minHeight: '100vh' }}>
      <h1>React .NET 10 WebAPI</h1>
      <p>Targeting: <code>{API_BASE_URL}</code></p>
      {data ? <pre>{JSON.stringify(data, null, 2)}</pre> : <p>Loading data from API...</p>}
      <div><Login></Login></div>
    </div>   
  );
}

export default App;