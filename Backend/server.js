const { fork } = require('child_process'); 
const path = require('path');

// Launch the Users API child process
const usersApi = fork(path.join(__dirname, 'product_api.js'));

// Launch the Products API child process
const productsApi = fork(path.join(__dirname, 'Auth.js'));

// Handle unexpected child exits
usersApi.on('exit', (code) => console.log(`Users API exited with code ${code}`));
productsApi.on('exit', (code) => console.log(`Products API exited with code ${code}`));
