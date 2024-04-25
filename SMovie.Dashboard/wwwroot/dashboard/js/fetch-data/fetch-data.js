const searchPerson = () => {
    let name = $("#search-name-person").val();
    
    $.ajax({
        url: "/Filter/SearchPersonName",
        type: "GET",
        data: {
            name: name,
            page: currentPagePerson,
            eachPage: 10
        },
        success: (data) => {
            data.forEach(person => {
                tagPerson(person);
            });
            currentPagePerson++;
            if (data.length === 0) {
                currentPagePerson = -1;
                return;
            }
        },
        error: (err) => {
            console.log(err);
        }
    });
}

const renderTotalCrews = () => {
    $.ajax({
        url: "/Api/TotalCrews",
        type: "GET",
        success: (data) => {
            $("#total-crews").text(data);
        },
        error: (err) => {
            console.log(err);
        }
    });
}

const renderTotalAccount = () => {
    $.ajax({
        url: "/Api/TotalAccount",
        type: "GET",
        success: (data) => {
            $("#total-account").text(data);
        },
        error: (err) => {
            console.log(err);
        }
    });
}

const renderTotalMovies = () => {
    $.ajax({
        url: "/Api/TotalMovies",
        type: "GET",
        success: (data) => {
            $("#total-movies").text(data);
        },
        error: (err) => {
            console.log(err);
        }
    });
}

const renderTotalCategory = () => {
    $.ajax({
        url: "/Api/TotalCategory",
        type: "GET",
        success: (data) => {
            $("#total-categories").text(data);
        },
        error: (err) => {
            console.log(err);
        }
    });
}

const getNotification = () => {
    $.ajax({
        url: "/Api/GetNotifications",
        type: "GET",
        data: {
            page: pageNotifs
        },
        success: (data) => {
            data.forEach(notif => {
                createNotification(notif);
                listNotifs.push(notif);
            });
            pageNotifs++;
        },
        error: (err) => {
            console.log(err);
        }
    });
}


const useSearchPerson = debounce(searchPerson, 500);