function applyDiscount() {
    var code = document.getElementById('discountCode').value;

    var total = parseFloat(document.getElementById('TotalAmountHidden').value) || 0;

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

                document.getElementById('totalAmount').innerText = '₼' + discountedTotal.toFixed(2);
                document.getElementById('discountMessage').innerText = 'Discount applied: ' + data.percentage + '%';

                document.getElementById('DiscountCode').value = code;
                document.getElementById('DiscountPercent').value = data.percentage;
                document.getElementById('DiscountId').value = data.discountId;
                document.getElementById('TotalAmountHidden').value = discountedTotal.toFixed(2);
            }
        });
}

