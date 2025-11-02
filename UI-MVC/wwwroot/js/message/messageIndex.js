document.addEventListener('DOMContentLoaded', function () {
    loadMessages();
});

async function loadMessages() {
    const urlSearchParams = new URLSearchParams(window.location.search);
    const userId = urlSearchParams.get("userId") || window.location.pathname.split("/").pop();

    await fetch(`/api/User/Details/${userId}`)
        .then(response => response.json())
        .then(data => {
            renderMessages(data);
        })
        .catch(error => console.log("Error:", error));

}

function renderMessages(messages) {
    
    const table = document.getElementById('messagesTable');
    table.innerHTML = '<tr><th>Message</th><th>Type</th></tr>';

    messages.forEach(message => {
        const row = table.insertRow();
        row.insertCell(0).innerText = message.content;
        if (message.type === 0) {
            row.insertCell(1).innerText = "Text";
        }else{
            row.insertCell(1).innerText = "Link";
        }
    });
}