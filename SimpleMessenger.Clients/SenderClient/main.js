document.addEventListener('DOMContentLoaded', () => {
    console.log('документ загружен');

    document.getElementById('textForm')?.addEventListener('submit', (e) => {
        e.preventDefault();
        const inputText = document.getElementById('inputText');
        const inputNumber = document.getElementById('inputNumber');

        if (inputText.value) {
            const url = 'http://localhost:8080/api/messages';
            let body = JSON.stringify({
                Text: inputText.value,
                SequenceNumber: parseInt(inputNumber.value)
            });

            console.log(body);

            fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: body
            })
            .then(response => {
                if (!response.ok) {
                    response.text().then(text => console.error(text));
                } else {
                    console.log('сообщение успешно отправлено');
                }
            })
            .catch(error => alert(error.message));
        }
    })
})