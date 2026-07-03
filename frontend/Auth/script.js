var attempt_username;
var token;
async function Login() {
  attempt_username =document.getElementById("Username").value;
  attempt_password =document.getElementById("password").value;

Get_Login_token().then(
function(value){
    window.location.replace('http://127.0.0.1:5500/frontend/Admin_priv/Product_CRUD/Product_Crud.html'+"?Token="+value)
}
);
}


async function Get_Login_token(){
let response = await fetch('http://localhost:3001/api/login', 
{   
method: 'POST', 
mode: 'cors',  
headers: {'Content-Type': 'application/json'},   
body: JSON.stringify({ username: attempt_username, password: attempt_password }) 
})/*.then(resp => resp.json())
   .then(json => {token = json.token
    return (json.token);
   })
   .catch(err => console.error(err));*/
   let data = await response.json();
   token = await data.token;
   return (token);

}
