function refreshBasket() {
    $("#basketTable").load("/Basket/LoadBasket");
    $("#miniBasket").load("/Basket/LoadMiniBasket");
    refreshBasketBlock();
    refreshHeaderMiniBasket();
}
function refreshHeaderMiniBasket() {
    $.get("/Basket/LoadHeaderBasket", function (data) {
        $("#headerBasket").html(data);
    });
}
function refreshBasketBlock() {
    $.get("/Basket/LoadBasketBlock", function (data) {
        $("#basket-summary-container").html(data);
    });
}

function addToBasket(id) {
    $.post("/Basket/Add", { id: id }, function (res) {
        refreshBasket();
    });
}

function addToBasketNew(id) {
    let qty = parseInt($("#qty-" + id).val());
    if (!qty || qty < 1) qty = 1;
    if (qty > 5) qty = 5;
    $.post("/Basket/AddNew", { id: id, qty: qty }, function (res) {
        refreshBasket();
    });
}
function removeItem(id) {
    $.post("/Basket/Remove", { id: id }, function () {
        refreshBasket();
    });
}

let changeQtyTimer = null;
function changeQty(id, qty) {

    let qtyNum = parseInt(qty);

    if (isNaN(qtyNum) || qtyNum < 1) {
        qtyNum = 1;
    }

    if (qtyNum > 5) {
        qtyNum = 5;
    }

    qty = qtyNum;

    

    if (changeQtyTimer) clearTimeout(changeQtyTimer);

    changeQtyTimer = setTimeout(function () {
        $.post("/Basket/ChangeQty", { id: id, qty: qty }, function (res) {
           
             refreshBasket();
           
        });
    }, 300);
}





  


        
        






