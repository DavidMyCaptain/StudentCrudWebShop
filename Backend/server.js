const express = require('express');
const app = express();

// Middleware for parsing JSON
app.use(express.json());

let users = [
  { id: 1, name: 'Cookies', Image: 'https://upload.wikimedia.org/wikipedia/commons/b/b4/Choco_chip_cookie.png', link: "http://127.0.0.1:5500/frontend/product/product.html?ID=1" },
  { id: 2, name: 'warship', Image: 'https://upload.wikimedia.org/wikipedia/commons/thumb/2/2e/Uss_iowa_bb-61_pr.jpg/1280px-Uss_iowa_bb-61_pr.jpg', link:"http://127.0.0.1:5500/frontend/product/product.html?ID=2" },
  { id: 3, name: 'gun', Image: 'https://upload.wikimedia.org/wikipedia/commons/b/b1/Glock_17_%286825676904%29_%D0%B1%D0%B5%D0%B7_%D1%84%D0%BE%D0%BD%D0%B0.jpg', link:"http://127.0.0.1:5500/frontend/product/product.html?ID=3" },
  { id: 4, name: 'stool', Image: 'https://upload.wikimedia.org/wikipedia/commons/5/56/TabouretAFDB.jpg', link: "http://127.0.0.1:5500/frontend/product/product.html?ID=4" },
  { id: 5, name: 'chair', Image: 'https://upload.wikimedia.org/wikipedia/commons/a/ac/Plastic_Tuinstoel.jpg', link: "http://127.0.0.1:5500/frontend/product/product.html?ID=5" },
];

// GET - Retrieve all users
app.get('/api/products', (req, res) => {
  res.set('Access-Control-Allow-Origin', '*');
    res.set('Access-Control-Allow-Methods', 'GET, POST, PUT, DELETE, OPTIONS');
    res.set('Access-Control-Allow-Headers', 'Content-Type, Authorization');
    
    // Handle preflight requests
    if (req.method === 'OPTIONS') {
        return res.status(200).end();
    }
  res.json(users);
});


// GET - Retrieve a specific user
app.get('/api/products/:id', (req, res) => {
  const user = users.find(u => u.id === parseInt(req.params.id));
  if (!user) return res.status(404).json({ message: 'User not found' });
  res.json(user);
});

// POST - Create a new user
app.post('/api/products', (req, res) => {
  const newUser = {
    id: users.length + 1,
    name: req.body.name,
    email: req.body.email
  };
  users.push(newUser);
  res.status(201).json(newUser);
});

// PUT - Update a user completely
app.put('/api/products/:id', (req, res) => {
  const user = users.find(u => u.id === parseInt(req.params.id));
  if (!user) return res.status(404).json({ message: 'User not found' });

  user.name = req.body.name;
  user.email = req.body.email;

  res.json(user);
});

// DELETE - Remove a user
app.delete('/api/products/:id', (req, res) => {
  const userIndex = users.findIndex(u => u.id === parseInt(req.params.id));
  if (userIndex === -1) return res.status(404).json({ message: 'User not found' });

  const deletedUser = users.splice(userIndex, 1);
  res.json(deletedUser[0]);
});

app.listen(3000, () => {
  console.log('REST API server running on port 3000');
}); 