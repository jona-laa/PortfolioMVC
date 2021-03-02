// AJAX for blog post pages
$(function () {
    $('#edit-items').on('click', '.pager a', function () {
        var url = $(this).attr('href');
        $('.container').load(url);
        $(document).scrollTop($("main").offset().top);
        return false;
    })
});



// Add hover and focus effects on projects
document.querySelectorAll(".portfolio-item_link").forEach(link => {
    link.addEventListener('mouseenter', () => {
        link.firstChild.nextElementSibling.firstElementChild.style.display = 'block';
    })

    link.addEventListener('mouseleave', () => {
        link.firstChild.nextElementSibling.firstElementChild.style.display = 'none';
    })

    link.addEventListener('focusin', () => {
        link.firstChild.nextElementSibling.firstElementChild.style.display = 'block';
    })

    link.addEventListener('focusout', () => {
        link.firstChild.nextElementSibling.firstElementChild.style.display = 'none';
    })
});



// Show hidden project info on default on touch screens(since no hover/focus capabilities)
var supportsTouch = 'ontouchstart' in window || navigator.msMaxTouchPoints;

if(supportsTouch) {
    document.querySelectorAll(".portfolio-item_link").forEach(link => {
        link.firstChild.nextElementSibling.firstElementChild.style.display = 'block';
    })
}