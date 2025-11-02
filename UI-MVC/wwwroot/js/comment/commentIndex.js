document.addEventListener('DOMContentLoaded', function() {
    loadData();
});

function loadData() {
    fetch('/api/comments')
        .then(response => response.json())
        .then(data => {
            renderComments(data);
        });
}
function renderComments(comments) {
    const table = document.getElementById('commentsTable');
    table.innerHTML = '<tr><th>Text</th><th>Upvotes</th><th>Downvotes</th></tr>';
    comments.forEach(comment => {
        const row = table.insertRow();
        row.insertCell(0).innerText = comment.text;
        row.insertCell(1).innerText = comment.upvotes;
        row.insertCell(2).innerText = comment.downvotes;
    });
}

document.getElementById('refreshButton').addEventListener('click', function() {
    loadData();
});