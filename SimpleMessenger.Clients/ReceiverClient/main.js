let socket = new WebSocket("ws://localhost:8080/ws/");

socket.onopen = (e) => {
    alert('[open]: соединение успешно установлено');
    socket.send('я подключился');
}

socket.onmessage = (e) => {
    let data = JSON.parse(e.data);
    console.log(`[message]: получили данные от сервера`);

    let messages = document.getElementById('messages');
    let message = document.createElement('li');
    let dateString = convertToFormattedDate(data.CreatedAt);
    message.className = 'messageItem';
    message.textContent = `${data.SequenceNumber} - [${dateString}]: ${data.Text}`;
    messages.appendChild(message);
}

socket.onclose = (e) => {
    if (e.wasClean) {
        alert(`[close]: соединение успешно закрыто, код=${e.code}, причина=${e.reason}`);
    } else {
        alert('[close]: соединение прервано')
    }
}

socket.onerror = (e) => {
    alert(`[error]: ${e.message}`)
}

function convertToFormattedDate(dateString) {
    let date = new Date(dateString);
    console.log(date);

    const dayOfMonth = date.getDate().toString().padStart(2, '0');
    const month = (date.getMonth() + 1).toString().padStart(2, '0');

    return `${dayOfMonth}.${month}.${date.getFullYear()} ${date.getHours()}:${date.getMinutes()}:${date.getSeconds()}`;
}