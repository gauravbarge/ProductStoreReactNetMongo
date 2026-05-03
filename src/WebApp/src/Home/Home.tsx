import { useState, useEffect } from 'react';

function Home() {
  const API_BASE_URL = import.meta.env.VITE_API_URL || 'http://localhost:5000';
  const [weather, setWeather] = useState<any>(null);

  useEffect(() => {
    fetch(`${API_BASE_URL}/weatherforecast`)
      .then(res => res.json())
      .then(json => setWeather(json))
      .catch(err => console.error("Weather API call failed:", err));
  }, []);

  return (
    <div style={{ padding: '2rem', backgroundColor: '#282c34', color: 'white', minHeight: '100vh' }}>
      <h1>Home Page</h1>
      <p>Targeting: <code>{API_BASE_URL}</code></p>
      <h2>Weather Forecast</h2>
      {weather ? <pre>{JSON.stringify(weather, null, 2)}</pre> : <p>Loading weather data...</p>}
    </div>
  );
}

export default Home;
