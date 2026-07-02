var attempt_username;
function Login() {
  attempt_username =document.getElementById("Username").value;
  attempt_password =document.getElementById("password").value;
  Login_Attempt();
}

function Login_Attempt(){
var token;
fetch('http://localhost:3001/api/login', 
{   
method: 'POST', 
mode: 'cors',  
headers: {'Content-Type': 'application/json'},   
body: JSON.stringify({ username: attempt_username, password: attempt_password }) 
}).then(resp => resp.json())
   .then(json => {token = json.token;
    console.log(token);
    protected();
   })
   .catch(err => console.error(err));

function protected(){
fetch('http://localhost:3001/api/protected', {
  method: 'GET',
  headers: {
    'Authorization': `Bearer ${token}`
  }
})
  .then(resp => resp.text())
  .then(data => console.log(data))
  .catch(err => console.error(err));
}
}
