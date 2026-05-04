import { useState, useEffect } from 'react';

// 1. Properly initialize base URL
const API_BASE_URL = import.meta.env.VITE_API_URL || 'http://localhost:5000';

// 2. Define a proper type for your user
interface User {
  id: string;
  username: string;
  email: string;
  createdAt: string;
}

function DisplayUsers() {
  // 3. Typed state
  const [users, setUsers] = useState<User[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    // 4. Get token directly inside useEffect to ensure it's current
    const token = localStorage.getItem('token');

    if (!token) {
      console.error("No token found");
      setLoading(false);
      return;
    }

    fetch(`${API_BASE_URL}/api/user/GetUsers`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      }
    })
      .then(res => {
        if (!res.ok) throw new Error('Network response was not ok');
        return res.json();
      })
      .then(json => {
        setUsers(json);
        setLoading(false);
      })
      .catch(err => {
        console.error("Get Users API call failed:", err);
        setLoading(false);
      });
  }, []);

  if (loading) return <div>Loading...</div>;

  return (
    <div>
      {users.map(user => (
        <div key={user.id}>
          <strong>{user.username}</strong> — {user.email}
        </div>
      ))}
    </div>
  );
}

export default DisplayUsers;
