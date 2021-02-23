// Hide header & to top on scroll
window.onscroll = () => {
  hideMenu();
  hideToTopBtn();
  alterBgColor(300, 'rgba(0, 0, 0, 0.6)', 'transparent', header);
  screen.width < 813 ? alterBgColor(300, 'rgba(0, 0, 0, 0.6)', 'transparent', mainMenu) : null;
};



// Hide Header
let prevScrollpos = window.pageYOffset;

const hideMenu = () => {
  let currentScrollPos = window.pageYOffset;

  if (window.pageYOffset > 100) {
    if (prevScrollpos > currentScrollPos) {
      elementToggle(header, 'top', '0');
      elementDisplay(mainMenu, 'none');
    } else {
      elementToggle(header, 'top', '-120px');
      elementDisplay(mainMenu, 'none');
    }
  }

  prevScrollpos = currentScrollPos;
}



// Toggle mobile menu
$('#main-menu-toggle').click(function () {
  $('.main-menu ul').slideToggle(200, function () {
  });
});
