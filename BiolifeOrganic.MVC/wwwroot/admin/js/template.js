(function ($) {
    'use strict';

    $(function () {
       
        var body = $('body');
        var sidebar = $('.sidebar');

        if (sidebar.length) {
            sidebar.on('show.bs.collapse', '.collapse', function () {
                sidebar.find('.collapse.show').collapse('hide');
            });
        }

        $('[data-toggle="minimize"]').on("click", function () {
            body.toggleClass('sidebar-icon-only');
        });

        $(".form-check label,.form-radio label").append('<i class="input-helper"></i>');

        var proBanner = document.querySelector('#proBanner');
        var bannerClose = document.querySelector('#bannerClose');

        if (proBanner) {
            if ($.cookie('majestic-free-banner') != "true") {
                proBanner.classList.add('d-flex');
                proBanner.classList.remove('d-none');
            } else {
                proBanner.classList.add('d-none');
                proBanner.classList.remove('d-flex');
            }
        }

        if (proBanner && bannerClose) {
            bannerClose.addEventListener('click', function () {
                proBanner.classList.add('d-none');
                proBanner.classList.remove('d-flex');
                var date = new Date();
                date.setTime(date.getTime() + 24 * 60 * 60 * 1000);
                $.cookie('majestic-free-banner', "true", { expires: date });
            });
        }

       
        function insertDefaultSliderHtml(textareaId) {
            var textarea = document.getElementById(textareaId);
            if (!textarea) return; 

            const defaultHtml = `
            <div class="slide-contain slider-opt03__layout01">
              <div class="media">
                <div class="child-elememt">
                  <a href="{{BUTTON_LINK}}" class="link-to">
                    <img src="{{IMAGE}}" alt="">
                  </a>
                </div>
              </div>
              <div class="text-content">
                <i class="first-line">{{TITLE}}</i>
                <h3 class="second-line">{{SUBTITLE}}</h3>
                <p class="third-line">{{DESCRIPTION}}</p>
                <p class="buttons">
                  <a href="{{BUTTON_LINK}}" class="btn btn-bold">
                    {{BUTTON_TEXT}}
                  </a>
                </p>
              </div>
            </div>`.trim();

            if (textarea.value.trim() !== "") {
                if (!confirm("Replace existing HTML?")) return;
            }

            textarea.value = defaultHtml;
        }

        var insertButtons = document.querySelectorAll('.insert-default-html');
        insertButtons.forEach(function (btn) {
            btn.addEventListener('click', function () {
                var targetId = btn.getAttribute('data-target'); 
                insertDefaultSliderHtml(targetId);
            });
        });

    });

})(jQuery);
