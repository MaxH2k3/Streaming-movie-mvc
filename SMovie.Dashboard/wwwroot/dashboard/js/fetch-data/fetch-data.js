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

const generateMovie = (englishName) => {
    $.ajax({
        url: "/Api/GenerateMovie",
        type: "GET",
        data: {
            content: englishName
        },
        success: (data) => {
            addDataMovie(data.data)

            $('button[name="ai-generate"]').find('.loading').addClass('d-none');
            $('button[name="ai-generate"]').find('.content').removeClass('d-none');
        },
        error: (err) => {
            console.log(err);
        }
    });
}

const chatBotMovie = () => {
    $.ajax({
        url: "https://api.coze.com/open_api/v2/chat",
        type: "POST",
        headers: {
            'Authorization': 'Bearer pat_SodNJ5sBbcaKvdDMOLIWjUOrSokTRscvB5sZ30HZlcV8WpMEOwp0OKIGePejC4qj',
            'Connection': 'keep-alive'
        },
        data: {
            "bot_id": "7364239808488783888",
            "stream": false,
            "user": "max",
            "query": 'Do you know "Conan" film?'
        },
        success: (data) => {
            //console.log(data.messages.find(x => x.type === "answer").content);
            console.log(data)
        },
        error: (err) => {
            console.log(err);
        }
    });

}


const useSearchPerson = debounce(searchPerson, 500);