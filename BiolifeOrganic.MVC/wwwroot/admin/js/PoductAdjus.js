function deleteProduct(id, element) {
    fetch(`/Admin/Product/Delete/${id}`, {
        method: `Post`
    }).
        then(response => response.text()).
        then(data => {
            element.parentNode.parentNode.remove();
        })
        .catch(err => {
            console.error(err);
            alert('Error for deleting!');

        });
}

function searchProducts() {
    let text = document.getElementById("productSearch").value;
    let categoryId = document.getElementById("categoryFilter").value;

    fetch(`/Admin/Product/Search?text=${encodeURIComponent(text)}&categoryId=${categoryId}`)
        .then(res => res.text())
        .then(html => {
            document.getElementById("productsBody").innerHTML = html;
        })
        .catch(err => console.error(err));
}