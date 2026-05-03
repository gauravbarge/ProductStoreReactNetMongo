import { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom'; // Added Link

function Login() {
  const navigate = useNavigate();
  const [form, setForm] = useState({
    username: "",
    password: ""
  });

  const [message, setMessage] = useState("");

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setForm({
      ...form,
      [e.target.name]: e.target.value
    });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    // FIXED: Changed '!==' to '===' 
    // You want to trigger the message if the field IS empty
    if (form.username === "") {
      setMessage("Username cannot be blank.");
      return;
    }

    if (form.password === "") {
      setMessage("Password cannot be blank.");
      return;
    }

    try {
      const response = await fetch("http://localhost:5000/api/auth/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(form)
      });

      const data = await response.json();

      if (!response.ok) {
        // FIXED: Ensure we are displaying a string, not an object
        setMessage(data.message || "Login failed.");
        return;
      }

      if (data.token) {
        localStorage.setItem('token', data.token);
        // Navigate to home after successful token storage
        navigate('/home');
      }

    } catch (error) {
      setMessage("Something went wrong. Is the server running?");
    }
  };

  return (
    <div>
      <h1>Login</h1>
      <form onSubmit={handleSubmit}>
        <input
          name="username"
          placeholder="Username"
          value={form.username}
          onChange={handleChange}
        />
        <input
          name="password"
          type="password"
          placeholder="Password"
          value={form.password}
          onChange={handleChange}
        />

        {/* FIXED: Changed label from Register to Login */}
        <button type="submit">Login</button>
      </form>
      
      <p>
        Don't have an account? <Link to="/register">Register here</Link>
      </p>
      
      {message && <p style={{ color: 'red' }}>{message}</p>}
    </div>
  );
}

export default Login;