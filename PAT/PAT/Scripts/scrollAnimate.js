$(document).ready(function () {
    $(document).on("scroll", onScroll);

    //smoothscroll
    $('a[href^="#"]').on('click', function (e) {
        e.preventDefault();
        $(document).off("scroll");

        $('a').each(function () {
            $(this).removeClass('active');
        })
        $(this).addClass('active');

        var target = this.hash,
            menu = target;
        $target = $(target);
        $('html, body').stop().animate({
            'scrollTop': $target.offset().top + 2
        }, 500, 'swing', function () {
            window.location.hash = target;
            $(document).on("scroll", onScroll);
        });
    });
});

function onScroll(event) {
    var scrollPos = $(document).scrollTop();
    $('.hide_sp a').each(function () {
        var currLink = $(this);
        var refElement = $(currLink.attr("href"));
        if (refElement.position().top <= scrollPos && refElement.position().top + refElement.height() > scrollPos) {
            $('.hide_sp ul li a').removeClass("active");
            currLink.addClass("active");
        }
        else {
            currLink.removeClass("active");
        }
    });
}



//// Cache selectors
//var lastId,
//    topMenu = $("#top-menu"),
//    topMenuHeight = topMenu.outerHeight() + 15,
//    // All list items
//    menuItems = topMenu.find("a"),
//    // Anchors corresponding to menu items
//    scrollItems = menuItems.map(function () {
//        var item = $($(this).attr("href"));
//        if (item.length) { return item; }
//    });

//// Bind click handler to menu items
//// so we can get a fancy scroll animation
//menuItems.click(function (e) {
//    var href = $(this).attr("href"),
//        offsetTop = href === "#" ? 0 : $(href).offset().top - topMenuHeight + 1;
//    $('html, body').stop().animate({
//        scrollTop: offsetTop
//    }, 300);
//    e.preventDefault();
//});

//// Bind to scroll
//$(window).scroll(function () {
//    // Get container scroll position
//    var fromTop = $(this).scrollTop() + topMenuHeight;

//    // Get id of current scroll item
//    var cur = scrollItems.map(function () {
//        if ($(this).offset().top < fromTop)
//            return this;
//    });
//    // Get the id of the current element
//    cur = cur[cur.length - 1];
//    var id = cur && cur.length ? cur[0].id : "";

//    if (lastId !== id) {
//        lastId = id;
//        // Set/remove active class
//        menuItems
//          .parent().removeClass("active")
//          .end().filter("[href='#" + id + "']").parent().addClass("active");
//    }
//});