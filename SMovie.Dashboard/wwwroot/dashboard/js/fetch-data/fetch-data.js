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

const useSearchPerson = debounce(searchPerson, 500);