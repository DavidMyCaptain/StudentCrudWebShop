const express = require('express');
const jwt = require('jsonwebtoken');
const { expressjwt: expressJwt } = require('express-jwt');
const { fork } = require('child_process'); 
const path = require('path');



const app = express();
app.use(express.json());
const cors = require('cors');
const { database } = require('pg/lib/defaults');




 const authority_level = "admin";
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
    const token = jwt.sign({
      username: username, 
      authority_level: authority_level 
      }, secret, { expiresIn: '1h' });

    console.log("SUCESS");
    return res.json({ token });
  }
  return res.status(401).json({ message: 'Invalid credentials' });
});


// Protected route, accessible only with a valid JWT (Bearer token)
app.get('/api/protected', jwtMiddleware, (req, res) => {
  res.send('This is a protected route. You are authenticated with a Bearer token!');
  get_users_database()
  var payload =return_user_from_token(req.headers.authorization);
  console.log("this user has a valid token: " + payload.username);
  
});

function return_user_from_token(usertoken){
  const parts = usertoken.split('.');
  const payloadEncoded = parts[1];
  return JSON.parse(decodeBase64Url(payloadEncoded));
}
app.post('/api/protected/new_product', jwtMiddleware, (req, res) => {
  var payload =return_user_from_token(req.headers.authorization);
  console.log("this user has a valid token: " + payload.username +" "+ payload.authority_level);
  

});

function get_users_database(){
  
  const database = fork(path.resolve(process.cwd(),'edge-js-Get-Database.js'))
  //database.send({ type: 'GREETING', payload: 'Hello from the parent process!' });

  // Listen for messages from the child process
  database.on('message', (message) => {
    //console.log('Message from child:', message);
  });
  database.on('exit', (code) => console.log(`Products API exited with code ${code}`));
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