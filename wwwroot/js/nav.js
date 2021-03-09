// Header/Menu DOM elements
const header = document.querySelector('.header-content');
const mainMenuToggle = document.querySelector('#main-menu-toggle');
const mainMenu = document.querySelector('#menu-main-menu');



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
      // Header
      elementToggle(header, 'top', '0');
      header.setAttribute('aria-hidden', 'false');

      // Menu
      elementDisplay(mainMenu, 'none');

      if (window.innerWidth <= 812) {
        // Toggle
        mainMenuToggle.setAttribute('aria-hidden', 'false');
      }
    } else {
      elementToggle(header, 'top', '-120px');
      header.setAttribute('aria-hidden', 'true');
      elementDisplay(mainMenu, 'none');
      mainMenuToggle.setAttribute('aria-hidden', 'true');
      mainMenu.setAttribute('aria-hidden', 'true');

    }
  }

  prevScrollpos = currentScrollPos;
}



// Set WAI-ARIA on page load
window.onload = () => {
  if (window.innerWidth <= 812) {
    mainMenuToggle.setAttribute('aria-hidden', 'false');
    mainMenuToggle.setAttribute('aria-expanded', 'false');
    mainMenu.setAttribute('aria-hidden', 'true');
  } else {
    mainMenuToggle.setAttribute('aria-hidden', 'true');
    mainMenuToggle.setAttribute('aria-expanded', 'true');
    mainMenu.setAttribute('aria-hidden', 'false');
  }
}



// Toggle mobile menu
$('#main-menu-toggle').click(function () {
  if (mainMenu.getAttribute('aria-hidden') == 'false') {
    mainMenu.setAttribute('aria-hidden', 'true');
    mainMenuToggle.setAttribute('aria-expanded', 'false');
  } else {
    mainMenu.setAttribute('aria-hidden', 'false');
    mainMenuToggle.setAttribute('aria-expanded', 'true');
  }

  $('.main-menu ul').slideToggle(200, function () {
  });
});



// Correct WAI-ARIA on resize
window.onresize = () => {
  if (window.innerWidth <= 812) {
    mainMenu.style.display = 'none';
    mainMenu.setAttribute('aria-hidden', 'true');
    mainMenuToggle.setAttribute('aria-hidden', 'false');
    mainMenuToggle.setAttribute('aria-expanded', 'false');
  } else {
    mainMenu.setAttribute('aria-hidden', 'false');
    mainMenuToggle.setAttribute('aria-hidden', 'true');
    mainMenuToggle.setAttribute('aria-expanded', 'true');
  }
}
