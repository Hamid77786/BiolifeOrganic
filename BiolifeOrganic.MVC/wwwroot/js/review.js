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

        let formData = new FormData(this);

        $.ajax({
            url: "/Shop/AddReview",
            type: "POST",
            data: formData,
            processData: false,
            contentType: false,
            success: function (res) {
                if (res.success) {

                    let productId = $("#product-id").val();

                    $("#comments .no-comments").remove();
                    $("#comments").load(`/Shop/LoadComments?productId=${productId}`);
                    $("#rating-info").load(`/Shop/LoadRatingBlock?productId=${productId}`);

                    let currentCount = parseInt($("#review-count").text()) || 0;
                    $("#review-count").text(currentCount + 1);

                    $("#frm-review")[0].reset();
                    $("#stars").val(0);
                    $(".btn-rating i").removeClass("fa-star").addClass("fa-star-o");
                }
                else {
                    alert(res.message || "Error");
                }
            }
        });
    });

});
