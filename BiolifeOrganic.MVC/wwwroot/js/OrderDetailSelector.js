document.querySelectorAll('.order-toggle').forEach(function (el) {
    el.addEventListener('click', function () {
        var orderId = this.getAttribute('data-order-id');
        var detailsRow = document.getElementById('details-' + orderId);
        var container = detailsRow.querySelector('.details-container');

        if (detailsRow.style.display === 'none') {
            if (container.innerHTML.trim() === '') {
                fetch(`/Order/Details/${orderId}`)
                    .then(res => res.text())
                    .then(html => {
                        container.innerHTML = html; 
                        detailsRow.style.display = 'table-row';
                    })
                    .catch(err => console.error(err));
            } else {
                detailsRow.style.display = 'table-row';
            }
        } else {
            detailsRow.style.display = 'none';
        }
    });
});