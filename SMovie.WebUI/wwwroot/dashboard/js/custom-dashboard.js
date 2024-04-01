
const changeActiveSideBar = (currentMenu) => {
    $('.nav-link').eq(0).removeClass('active');
    $('.nav-link').eq(currentMenu).addClass('active');
}