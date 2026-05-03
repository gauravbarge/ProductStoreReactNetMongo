import { useState, useEffect } from 'react';

function Home() {
  const [message, setMessage] = useState("");

  useEffect(() => {
    // Calling the function on mount
    getUserToken();
  }, []);

  const getUserToken = () => {
    const token = localStorage.getItem('token');
    // Basic check: if token exists, set it; otherwise, set a fallback
    setMessage(token || "No token found. Please login.");
  };

  return (
    <div>
      <h1>Home Page</h1>
      <p>Token: {message}</p>
    </div>
  );
}

export default Home;