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
        // res может содержать count
        refreshBasket();
    });
}

function removeItem(id) {
    $.post("/Basket/Remove", { id: id }, function () {
        refreshBasket();
    });
}

// простой дебаунс
let changeQtyTimer = null;
function changeQty(id, qty) {
    // приводим qty к числу
    qty = parseInt(qty) || 0;

    // отменяем предыдущий таймер
    if (changeQtyTimer) clearTimeout(changeQtyTimer);

    changeQtyTimer = setTimeout(function () {
        $.post("/Basket/ChangeQty", { id: id, qty: qty }, function (res) {
            if (res && res.success) {
                // можно обновлять только мини-корзину и таблицу без полной перезагрузки страницы
                refreshBasket();
            }
        });
    }, 300); // 300ms — подберите значение под UX
}


