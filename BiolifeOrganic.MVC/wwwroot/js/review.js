$(document).ready(function () {

    $(".btn-rating").click(function (e) {
        e.preventDefault();
        var value = $(this).data("value");
        $("#stars").val(value);

        $(this).siblings().find("i").removeClass("fa-star").addClass("fa-star-o");
        $(this).prevAll().find("i").removeClass("fa-star-o").addClass("fa-star");
        $(this).find("i").removeClass("fa-star-o").addClass("fa-star");
    });

    $("#frm-review").on("submit", function (e) {
        e.preventDefault();

        let form = this;
        let formData = new FormData(form);

        formData.set("Stars", $("#stars").val());
        formData.set("ProductId", $("#product-id").val());
        formData.set("__RequestVerificationToken",
            $('input[name="__RequestVerificationToken"]').val()
        );

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
                    $("#comments").load(`/Shop/LoadComments?productId=${productId}&page=1`);
                    $("#rating-info").load(`/Shop/LoadRatingBlock?productId=${productId}`);

                    let count = parseInt($("#review-count").text()) || 0;
                    $("#review-count").text(count + 1);

                    // сброс формы
                    form.reset();
                    $("#stars").val(0);
                    $(".btn-rating i")
                        .removeClass("fa-star")
                        .addClass("fa-star-o");
                }
                else {
                    alert(res.message || "Error");
                }
            },
            error: function (xhr) {
                console.error(xhr.responseText);
                alert("Error while submitting review");
            }
        });
    });
   

});
