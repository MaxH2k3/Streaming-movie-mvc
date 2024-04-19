
const changeActiveSideBar = (currentMenu) => {
    $('.nav-link').eq(0).removeClass('active');
    $('.nav-link').eq(currentMenu).addClass('active');
}

function debounce(func, delay) {
    let timeoutId;
    return function () {
        const context = this;
        const args = arguments;
        clearTimeout(timeoutId);
        timeoutId = setTimeout(function () {
            func.apply(context, args);
        }, delay);
    };
}