const fs = require('fs');

const data = fs.readFileSync('../compiled.emojo', 'utf8');

var asciiString;

var line = data.split('\n');
var command = line[0].split(' ');
var variable = line[1].split('-');

command.forEach(element => {
    if(element == "\r")
        return

    var update = element.split('x')
    var paraInt = parseInt(update[1])


    if (update[0] == 0) {
        asciiString = Buffer.from(variable[paraInt].split(' ')).toString('ascii');
        console.log(asciiString)
    }

    else if (update[0] == 1) {
        var num = update[1].split('e')
        sum = 0

        num.forEach(index => {

            let buf = Buffer.from(variable[index].split(' '), 'ascii')
            let banana = buf.toString('ascii')
            
            sum += parseInt(String(banana).replace(/[^\w\s]/g, ""))
        })
        console.log("Soma:" + sum)
    }

    else {
        console.log("sem comando")
    }
});








