$(document).ready(function () {

    
    $(".btn-rating").click(function (e) {
        e.preventDefault();
        var value = $(this).data("value");
        $("#stars").val(value);

       
        $(this).siblings().find("i").removeClass("fa-star").addClass("fa-star-o");
        $(this).prevAll().find("i").removeClass("fa-star-o").addClass("fa-star");
        $(this).find("i").removeClass("fa-star-o").addClass("fa-star");
    });

   
    $("#frm-review").submit(function (e) {
        e.preventDefault(); 

        const data = {
            productId: $("#product-id").val(),
            name: $("#name").val(),
            emailAddress: $("#email").val(),
            note: $("#comment").val(),
            stars: parseInt($("#stars").val()) || 0
        };

        $.post("/Shop/AddReview", data, function (res) {
            if (res.success) {
                location.reload();
            } else {
                alert(res.message || "Error");
            }
        });
    });

});
