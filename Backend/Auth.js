const express = require('express');
const jwt = require('jsonwebtoken');
const { expressjwt: expressJwt } = require('express-jwt');

const app = express();
app.use(express.json());
const cors = require('cors');




const secret = 'your-secret-key'; // You should store this securely


// Middleware to protect routes using JWT
const jwtMiddleware = expressJwt({ secret, algorithms: ['HS256'] });


app.use(express.json()); // Parse JSON bodies

const corsOptions = {
  origin: '*', // Your frontend origin
  methods: ['GET', 'POST', 'PUT', 'DELETE', 'PATCH', 'OPTIONS'],
  allowedHeaders: ['Content-Type', 'Authorization', 'X-Requested-With'],
  credentials: true,
  maxAge: 600 // Cache preflight response for 10 minutes
};

// Apply CORS middleware to handle preflight requests
app.use(cors(corsOptions));

// Route to login and issue a JWT token
app.post('/api/login', (req, res) => {
  console.log(req.body);

  const { username, password } = req.body;

  // In a real-world app, you'd validate the user against your database
  if (username === 'user' && password === 'password') {
    // Generate a JWT token
    const token = jwt.sign({ username }, secret, { expiresIn: '1h' });
    console.log("SUCESS");
    return res.json({ token });
  }
  return res.status(401).json({ message: 'Invalid credentials' });
});


// Protected route, accessible only with a valid JWT (Bearer token)
app.get('/api/protected', jwtMiddleware, (req, res) => {
  res.send('This is a protected route. You are authenticated with a Bearer token!');
  var payload =return_user_from_token(req.headers.authorization);
  console.log("this user has a valid token: " + payload.username);
  
});

function return_user_from_token(usertoken){
  const parts = usertoken.split('.');
  const payloadEncoded = parts[1];
  return JSON.parse(decodeBase64Url(payloadEncoded));
}

const decodeBase64Url = (base64Url) => {
  const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
  return decodeURIComponent(atob(base64).split('').map((c) => {
    return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
  }).join(''));
};

const PORT = process.env.PORT || 3001;
app.listen(PORT, () => {
  console.log(`Server is running on port ${PORT}`);
});