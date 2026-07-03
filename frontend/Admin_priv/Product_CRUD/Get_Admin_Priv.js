const paramsString = window.location.search;
const searchParams = new URLSearchParams(paramsString);
console.log(searchParams.get("Token")); // a
token = searchParams.get("Token");
function protected(){
fetch('http://localhost:3001/api/protected', {
  method: 'GET',
  headers: {
    'Authorization': `Bearer ${token}`
  }
})
.then(response => {
  if (response.status === 401) {
     window.location.replace('http://127.0.0.1:5500/frontend/Auth/Auth.html')
  }})
  .then(resp => resp.text())
  .then(data => console.log(data))
  .catch(err => console.error(err));
}
protected();