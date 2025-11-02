document.addEventListener('DOMContentLoaded', function () {
    populateMessages();
});
let messageSelect;
function populateMessages() {
    messageSelect = document.getElementById('messageSelect');
    messageSelect.innerHTML = '';
    const userId = getUserIdFromUrl();
    fetch(`/api/UserMessage/NonUserMessages/${userId}`)
        .then(response => response.json())
        .then(messages => {
            messages.forEach(message => {
                const option = document.createElement('option');
                option.value = message.id;
                option.textContent = message.content;
                messageSelect.appendChild(option);
            });
        })
        .catch(error => console.error('Error fetching messages:', error));
}
function addMessageToUser() {
    const messageId = messageSelect.options[messageSelect.selectedIndex].value
    let datumInput = document.getElementById('datumInput');
    const userId = getUserIdFromUrl();

    if (datumInput.value.trim() === '') {
        datumInput = Date.now();
    }
    
    fetch('/api/UserMessage/CreateUserMessage', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json'
        },
        body: JSON.stringify( {
            "userId": userId,
            "messageId": messageId,
            "interactionDate": datumInput.value
        })
    })
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.json();
        })
    .then(() => {
        populateMessages();
        loadMessages()
    })
}
function getUserIdFromUrl() {
    const urlParts = window.location.pathname.split('/');
    return urlParts[urlParts.length - 1];
}