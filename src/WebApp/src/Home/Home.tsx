import { useState, useEffect } from 'react';

function Home() {
  const API_BASE_URL = import.meta.env.VITE_API_URL || 'http://localhost:5000';
  const [message, setMessage] = useState("");
  const [weather, setWeather] = useState<any>(null);

  useEffect(() => {
    getUserToken();
    fetch(`${API_BASE_URL}/weatherforecast`)
      .then(res => res.json())
      .then(json => setWeather(json))
      .catch(err => console.error("Weather API call failed:", err));
  }, []);

  const getUserToken = () => {
    const token = localStorage.getItem('token');
    // Basic check: if token exists, set it; otherwise, set a fallback
    setMessage(token || "No token found. Please login.");
  };

  // Define the style object
const textwrap = {
  whiteSpace: 'normal',
  overflowWrap: 'break-word', // Ensures long tokens wrap
  wordWrap: 'break-word',     // Legacy support
  maxWidth: '100%'            // Constrain to container
};

  return (
    <div style={{ padding: '2rem', backgroundColor: '#282c34', color: 'white', minHeight: '100vh' }}>
      <h1>Home Page</h1>
      <p style={textwrap}>Token: {message}</p>
      <p>Targeting: <code>{API_BASE_URL}</code></p>
      <h2>Weather Forecast</h2>
      {weather ? <pre>{JSON.stringify(weather, null, 2)}</pre> : <p>Loading weather data...</p>}
    </div>
  );
}

export default Home;
