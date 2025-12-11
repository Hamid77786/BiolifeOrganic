$(document).on('click', '#btnSubscribe', function () {
    var email = $('#newsletterEmail').val().trim();
    if (email === '') {
        $('#newsletterMessage').text('Please enter your email').css('color', 'red');
        return;
    }

    $.ajax({
        url: '/Newsletter/Subscribe', 
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ email: email }),
        success: function (response) {
            if (response.success) {
                $('#newsletterMessage').text(response.message).css('color', 'green');
                $('#newsletterEmail').val('');
            } else {
                $('#newsletterMessage').text(response.message).css('color', 'red');
            }
        },
        error: function () {
            $('#newsletterMessage').text('An error occurred').css('color', 'red');
        }
    });
});