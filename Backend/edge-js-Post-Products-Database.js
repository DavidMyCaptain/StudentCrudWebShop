const edge = require('edge-js');

// 1. Define your .NET / C# function
const computeCSharpTask = edge.func(`
    using System.Threading.Tasks;

    public class Startup
    {
        public async Task<object> Invoke(object input)
        {
            var payload = input.ToString();
            return " [.NET Core Processing Done] -> " + payload;
        }
    }
`);

// 2. Listen for messages coming from the main Node.js Parent Process
process.on('message', (messageFromParent) => {
    //console.log('Child Process received payload:', messageFromParent);

    // 3. Execute the C# code using the received data
    computeCSharpTask(messageFromParent.payload, function (error, cSharpResult) {
        if (error) {
            // Forward any .NET errors to the parent
            process.send({ type: 'ERROR', error: error.message });
            return;
        }

        // 4. Commmunicate the C# result back up to the parent process
        process.send({ type: 'SUCCESS', data: cSharpResult });
    });
});
