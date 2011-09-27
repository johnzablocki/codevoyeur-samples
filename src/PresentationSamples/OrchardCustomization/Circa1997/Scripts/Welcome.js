var fullMessage = "Welcome to Code Camp NYC!";


function scroll() {
    
    var currentStatus = window.status;    
    if (currentStatus == fullMessage) {
        window.status = fullMessage[0];
    } else {
        var nextMessage = fullMessage.substr(0, currentStatus.length + 1);
        window.status = nextMessage;
    }

}

window.setInterval(scroll, 150);