function applyDiscount() {
    var code = document.getElementById('discountCode').value;
    var total = parseFloat('@Model.TotalAmount');

    fetch('/Checkout/ApplyDiscount', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ code: code, totalAmount: total })
    })
        .then(r => r.json())
        .then(data => {
            if (!data.success) {
                document.getElementById('discountMessage').innerText = data.message;
            } else {
                
                var discountedTotal = total * (1 - data.percentage / 100);
                document.querySelector('.stt-price').innerText = '₼' + discountedTotal.toFixed(2);
                document.getElementById('discountMessage').innerText = 'Discount applied: ' + data.percentage + '%';
            }
        });
}
