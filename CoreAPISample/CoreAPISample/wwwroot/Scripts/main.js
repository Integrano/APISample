jQuery(document).ready(function ($) {
    // browser window scroll (in pixels) after which the "back to top" link is shown
    var offset = 300,
        //browser window scroll (in pixels) after which the "back to top" link opacity is reduced
        offset_opacity = 1200,
        //duration of the top scrolling animation (in ms)
        scroll_top_duration = 500,
        //grab the "back to top" link
        $back_to_top = $('.back-top');

    //hide or show the "back to top" link
    $(window).scroll(function () {
        ($(this).scrollTop() > offset) ? $back_to_top.addClass('cd-is-visible') : $back_to_top.removeClass('cd-is-visible cd-fade-out');
        if ($(this).scrollTop() > offset_opacity) {
            $back_to_top.addClass('cd-fade-out');
        }
    });

    //smooth scroll to top
    $back_to_top.on('click', function (event) {
        event.preventDefault();
        $('html,body').animate({
            scrollTop: 0,
        }, scroll_top_duration
        );
        $("#logo-anchor").focus();
    });
    //Home Page Webpart Heading Icons
    $('#latestVideos h2.ms-webpart-titleText').append('<i class="fa fa-video-camera pull-right"></i>');
    $('#upcomingEvents h2.ms-webpart-titleText').append('<i class="fa fa-calendar pull-right"></i>');
    $('#trendingDocuments h2.ms-webpart-titleText').append('<i class="fa fa-file-text-o pull-right"></i>');

    $(".ms-core-listMenu-verticalBox .root ul.static").addClass("side-nav");
    $(".ms-core-listMenu-verticalBox .ms-core-listMenu-item:contains('Recent')").parent().hide();
    toggleNavigation();


});

/*Set active menu class*/
$('.navbar-nav li a').click(function () {
    $('.navbar-nav li').removeClass("active");
    $(this).parent.addClass("active");
});

function altValidator() {
    var imgElements = document.getElementsByTagName('img');
    for (var i = 0; i < imgElements.length; i++) {
        var currentImg = imgElements[i];
        var imageUrl = currentImg.src;
        if (!currentImg.hasAttribute('alt') && (imageUrl.indexOf("_layouts") >= 0)) {
            currentImg.setAttribute('alt', '');
        }
    }
}

//Left Navigation Custom toggle start
function toggleNavigation() {
    //$(".ms-core-listMenu-verticalBox .root ul.static").addClass("side-nav");

    $(".ms-core-listMenu-verticalBox .root ul.side-nav").each(function () {
        $("<span class='caret navbar-toggle sub-arrow'></span>").insertBefore(this);
    });

    $(".caret.navbar-toggle.sub-arrow").click(function () {
        $(this).parent("li.static").toggleClass("open");
    });

}