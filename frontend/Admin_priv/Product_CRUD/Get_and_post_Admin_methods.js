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
async function Post_procted(product_name, product_image){
const response = await fetch('http://localhost:3001/api/protected/new_product', {
  method: 'post',
  mode: 'cors',  
  headers: {
    'Authorization': `Bearer ${token}`,
    'Content-Type': 'application/json'
  },
  body: JSON.stringify({ product: product_name, product_image: product_image })
});

if (response.ok) {
  const data = await response.text();
  console.log(data);
} else {
  const errorText = await response.text();
  console.log(errorText); 
}
}

Post_procted("Cookies", "https://upload.wikimedia.org/wikipedia/commons/c/cf/Rifles_at_the_National_Firearms_Museum.jpg");
protected();