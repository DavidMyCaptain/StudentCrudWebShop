const paramsString = window.location.search;
const searchParams = new URLSearchParams(paramsString);
console.log(searchParams.get("ID")); // a
var request = new XMLHttpRequest()

const app = document.getElementById('main');
const container = document.createElement('div');
app.appendChild(container)
    

request.open('GET', 'http://localhost:3000/api/products', true)

request.onload = function () {
    if (request.status >= 200 && request.status < 400) {

        var data = JSON.parse(request.responseText)
        var matchedProduct = data.find(product => product.id === Number(searchParams.get("ID")));

        const card = document.createElement('div')
        const h1 = document.createElement('h1')
        const img = document.createElement('img')

        container.setAttribute('class', 'container');
        card.setAttribute('class', 'card');

        h1.textContent = matchedProduct.name
        img.src = matchedProduct.link  
        
        card.appendChild(h1)
        card.appendChild(img)
        container.appendChild(card)
        
    } else {
        console.log('Sever responded with ' + request.status)
    }

    
}

request.send()
