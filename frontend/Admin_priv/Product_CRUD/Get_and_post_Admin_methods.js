const paramsString = window.location.search;
const searchParams = new URLSearchParams(paramsString);
console.log(searchParams.get("Token")); // a
token = searchParams.get("Token");

async function protected(){
const response = await fetch('http://localhost:3000/api/protected/AuthCheck', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json'
  },
body: JSON.stringify({ token })
});

if (response.status === 401) {
await window.location.replace('http://127.0.0.1:5500/StudentCrudWebShop/frontend/index.html');} 

else if (response.ok) {
  const data = await response.text();
  console.log(data);
} else {
  const errorText = await response.text();
  console.log(errorText); 
}
}
async function Post_procted(product_name, product_image, product_id, product_description, product_Price){
  console.log("id: " + product_name);
const response = await fetch('http://localhost:3000/api/protected/new_product', {
  method: 'post',
  mode: 'cors',  
  headers: {
    'Content-Type': 'application/json'
  },
  body: JSON.stringify({ token, product_name, product_image, product_id,product_description, product_Price})
});

if (response.ok) {
  const data = await response.text();
  console.log(data);
} else {
  const errorText = await response.text();
  console.log(errorText); 
}
}

protected();