function filterByOption() {
    const option = document.getElementById("contactTypeFilter").value;

    let url = '/Admin/WebContact/ByOption';
    if (option !== '') {
        url += '?option=' + option;
    }

    fetch(url)
        .then(res => res.text())
        .then(html => {
            const doc = new DOMParser().parseFromString(html, 'text/html');
            const newTable = doc.getElementById("contactsTable");
            document.getElementById("contactsTable").innerHTML = newTable.innerHTML;
        });
}


function deleteWebContact(id, element) {
    if(!confirm('Are you sure you want to delete this contact?')) return;

fetch(`/Admin/WebContact/Delete/${id}`, {
    method: 'POST',
headers: {
    'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
        }
    })
    .then(response => {
        if(response.ok) {
            const row = document.getElementById(`contactRow_${id}`);
if(row) row.remove();
        } else {
    alert('Failed to delete contact.');
        }
    })
    .catch(err => {
    console.error(err);
alert('Error while deleting contact.');
    });
}
function setDefault(id, option) {
    fetch(`/Admin/WebContact/SetDefault`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
        },
        body: JSON.stringify({
            id: id,
            option: option
        })
    })
        .then(response => {
            if (response.ok) {
                location.reload();
            } else {
                alert('Failed to set default contact.');
            }
        });
}





