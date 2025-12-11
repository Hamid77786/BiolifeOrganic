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

                // Удаляем "нет комментариев"
                $("#comments .no-comments").remove();

                // Обновляем count в табе
                let currentCount = parseInt($("#review-count").text()) || 0;
                $("#review-count").text(currentCount + 1);

                // Обновляем комментарии через отдельный action (нужен LoadComments)
                $("#comments").load(`/Shop/LoadComments?productId=${data.productId}`);

                // Обновляем рейтинг через отдельный action
                $("#rating-info").load(`/Shop/LoadRatingBlock?productId=${data.productId}`);

                // Сброс формы
                $("#frm-review")[0].reset();
                $("#stars").val(0);
                $(".btn-rating i").removeClass("fa-star").addClass("fa-star-o");

            } else {
                alert(res.message || "Error");
            }
        });





    });

});

