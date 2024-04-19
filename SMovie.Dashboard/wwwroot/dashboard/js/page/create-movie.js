$(document).ready(() => {
    $("#search-name-person").on("input", () => {
        currentPagePerson = 1;
        $("#person-list-table").empty();
        useSearchPerson();
    });

    const element = $("#table-add-person");
    element.on("scroll", () => {
        const scrollTop = element.scrollTop();
        const height = element.outerHeight();
        const scrollHeight = element[0].scrollHeight;

        if (scrollTop + height >= scrollHeight) {
            if (currentPagePerson !== -1) {
                useSearchPerson();
            }
            
        }
    })

});

const tagPerson = (person) => {
    let tag = `
        <tr class="${isPersonSelected(person.personId) ? "person-selected" : ""}">
                                                <input type="hidden" class="person-id" value="${person.personId}" />
                                                <td class="w-25">
                                                    <img src="${person.thumbnail}" class="w-100"/>
                                                </td>
                                                <td class="name-person">${person.namePerson}</td>
                                                <td>
                                                    <div class="d-flex justify-content-evenly">
                                                        <a onclick="addPersonToCast(event)" class="${isPersonSelected(person.personId) ? "disabled" : ""} btn btn-primary btn-icon btn-sm rounded-pill ms-2" href="#" role="button">
                                                            <span class="btn-inner">
                                                                <i class="fa-solid fa-user-plus"></i>
                                                            </span>
                                                        </a>
                                                    </div>
                                                </td>
                                            </tr>
    `
    
    $("#person-list-table").append(tag);
    
}

const tagDisplayPerson = (person) => {
    let tag = `
        <tr class="text-center">
            <input type="hidden" class="person-id" value="${person.personId}" />
            <td class="w-15">
                <input type="hidden" name="thumbnail-person" value="${person.personId}" />
                <img src="${person.thumbnail}" class="w-100"/>
            </td>
            <td class="name-person">
                <input type="hidden" name="namePerson" value="${person.namePerson}" />
                ${person.namePerson}
            </td>
            <td><input name="characterName" class="text-center btn" value="" /></td>
            <td>
                <div class="d-flex align-items-center justify-content-center">
                    <a onclick="removePersonFromCast(event)" aria-current="page" href="#" class="active text-danger" title="Delete">
                        <i class="fa-solid fa-trash me-4"></i>
                    </a>
                </div>
            </td>
        </tr>
    `;
    $("#display-cast-person").append(tag);
};

const addPersonToCast = (event) => {
    let elementTr = $(event.target).closest("tr");
    let person = {
        personId: elementTr.find(".person-id").val(),
        thumbnail: elementTr.find("img").attr("src"),
        namePerson: elementTr.find(".name-person").text()
    }
    chosenPerson(elementTr);
    tagDisplayPerson(person);
    listSelectedPerson.push(person.personId);
}

const removePersonFromCast = (event) => {
    let elementTr = $(event.target).closest("tr");
    let personId = elementTr.find(".person-id").val();
    // remove attribute person
    removeChosenPerson(personId)
    // remove tag person
    elementTr.remove();
    // remove from list selected person
    removeListSelectedPerson(personId);
}

const chosenPerson = (element) => {
    element.addClass("person-selected");
    element.find("a").addClass("disabled");
}

const removeChosenPerson = (personId) => {
    let element = $(`#person-list-table tr:has(input[value="${personId}"])`);
    element.removeClass("person-selected");
    element.find("a").removeClass("disabled");
}

const removeListSelectedPerson = (personId) => {
    let index = listSelectedPerson.findIndex(person => person === personId);
    if(index !== -1) {
        listSelectedPerson.splice(index, 1);
    }
}

const isPersonSelected = (personId) => {
    let index = listSelectedPerson.findIndex(person => person === personId);
    if(index !== -1) {
        return true;
    }

    return false;
}