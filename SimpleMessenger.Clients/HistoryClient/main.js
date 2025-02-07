document.addEventListener('DOMContentLoaded', (_) => {
    const button = document.getElementById('sendButton');
    button.onclick = async () => {
        const lookbackPeriod = document.getElementById('timeInput');
        const messagesList = document.getElementById('messagesList');
        const url = 'http://localhost:8080/api/messages/history/'
        const body = JSON.stringify({
            lookbackPeriod: lookbackPeriod.value + ':00'
        });

        messagesList.replaceChildren();

        console.log(body)

        fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: body
        })
        .then(response => {
            if (!response.ok){
                throw new Error('ошибка при получении результата');
            }

            return response.json();
        })
        .then(data => {
            const messages = data.messages;

            messages.forEach(msg => {
                const messageItem = document.createElement('li');
                messageItem.className = 'messageItem';         
                messageItem.textContent = `${msg.sequenceNumber} - [${convertToFormattedDate(msg.timestamp)}]: ${msg.text}`;
                messagesList.appendChild(messageItem);
            });
        })
        .catch(error => alert(error.message));
    }
})

function convertToFormattedDate(dateString) {
    let date = new Date(dateString);
    console.log(date);

    const dayOfMonth = date.getDate().toString().padStart(2, '0');
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const seconds = date.getSeconds().toString().padStart(2, '0');

    return `${dayOfMonth}.${month}.${date.getFullYear()} ${date.getHours()}:${date.getMinutes()}:${seconds}`;
}