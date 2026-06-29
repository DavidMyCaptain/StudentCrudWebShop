async function getData() {
  const url = "http://localhost:3000/api/users";
  try {
    const response = await fetch(url);
    if (!response.ok) {
      throw new Error(`Response status: ${response.status}`);
    }

    const result = await response.json();
    console.log(result);
  } catch (error) {
    console.error(error.message);
  }
}
getData();
/*
var request = new XMLHttpRequest()

const app = document.getElementById('main');
const logo = document.createElement('img');
app.appendChild(logo);
const container = document.createElement('div');
app.appendChild(container)
    

request.open('GET', 'http://localhost:3000/', true)

request.onload = function () {
    if (request.status >= 200 && request.status < 400) {

        var data = JSON.parse(request.responseText)
        data.forEach(movie => {

        const card = document.createElement('div')
        const h1 = document.createElement('h1')

        container.setAttribute('class', 'container');
        card.setAttribute('class', 'card');

        h1.textContent = movie.title


        
        
        card.appendChild(h1)
        container.appendChild(card)



    });
        
    } else {
        console.log('Sever responded with ' + request.status)
    }

    
}

request.send()
*/