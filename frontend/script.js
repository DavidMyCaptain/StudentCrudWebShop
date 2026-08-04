

var request = new XMLHttpRequest()

const app = document.getElementById('main');
const container = document.createElement('div');
app.appendChild(container)
    

request.open('GET', 'http://127.0.0.1:3000/API/products', true)

request.onload = function () {
    if (request.status >= 200 && request.status < 400) {

        var data = JSON.parse(request.responseText)
        data.forEach(product => {

        const card = document.createElement('div')
        const h1 = document.createElement('h1')
        const img = document.createElement('img')
        const a = document.createElement('a')

        container.setAttribute('class', 'container');
        card.setAttribute('class', 'card');

        h1.textContent = product.name
        img.src = product.link 
        a.href = product.src
        var Link_text = document.createTextNode('PlaceHolder');

        a.appendChild(Link_text)
        console.log(a)
        card.appendChild(a)
        card.appendChild(h1)
        card.appendChild(img)
        container.appendChild(card)



    });
        
    } else {
        console.log('Sever responded with ' + request.status)
    }

    
}

request.send()
