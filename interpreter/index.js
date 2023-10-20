const fs = require('fs');

const data = fs.readFileSync('./arquivo.emojo', 'utf8');

var line = data.split('\n');
var command = line[0].split(' ');
var variable = line[1].split('-');

command.forEach(element => {
    var update = element.split('x')
    var paraInt = parseInt(update[1])

    if (update[0] == 0) {
        console.log(variable[paraInt])
    }
    else {
        console.log("sem comando")
    }
});








