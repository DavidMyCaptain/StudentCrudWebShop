const paramsString = window.location.search;
const searchParams = new URLSearchParams(paramsString);
console.log(searchParams.get("Token")); // a
token = searchParams.get("Token");

async function protected(){
const response = await fetch('http://localhost:3001/api/protected', {
  method: 'GET',
  headers: {
    'Authorization': `Bearer ${token}`
  }
});

if (response.status === 401) {
await window.location.replace('http://127.0.0.1:5500/frontend/Auth/Auth.html');} 

else if (response.ok) {
  const data = await response.text();
  console.log(data);
} else {
  const errorText = await response.text();
  console.log(errorText); 
}

  
}
protected();