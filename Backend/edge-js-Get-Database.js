const edge = require('edge-js');

const helloWorld = edge.func(function () {
   async (input) => {
	   return ".NET welcomes " + input.ToString();
   }
});

helloWorld('Node.js', function (error, result) {
   if (error) throw error;
   console.log(result);
});