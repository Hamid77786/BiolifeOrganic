function updateWishlistCount() {
    $("#wishlistCount").load("/Wishlist/MiniHeader");
}

function toggleWishlist(productId) {
    $.post("/Wishlist/Toggle", { productId: productId }, function (res) {
        if (!res.success) return;

       

        
        let btn = $(`#wish-${productId}`);
        if (res.isAdded) {
            btn.addClass("active");
        } else {
            btn.removeClass("active");
        }

        updateWishlistCount();
        
    });
}
function removeFromWishlist(productId) {
    $.post("/Wishlist/Remove", { productId: productId }, function (res) {
        if (!res.success) return;

        updateWishlistCount();
        loadWishlistList();
    });
}
function loadWishlistList() {
    $("#wishlistList").load("/Wishlist/LoadList");
}

