const fetchDataAndAppendToTable = async () => {
    // Get element 
    let tableContainer = $("#table-add-person");
    let tableBody = tableContainer.children().find("tbody")
    tableContainer.on('scroll', function () {
        if (tableContainer.scrollTop + tableContainer.clientHeight >= tableContainer.scrollHeight) {
            // Gọi hàm fetch data và thêm vào bảng ở đây
            console.log("fetch data");
        }
    });

    // const response = await fetch('https://localhost:44300/api/persons');
    // const data = await response.json();

    // data.forEach(person => {
    //     const tr = document.createElement('tr');
    //     tr.innerHTML = `
    //         <input type="hidden" class="person-id" value="${person.personId}" />
    //         <td class="w-25">
    //             <img src="${person.thumbnail}" class="w-100"/>
    //         </td>
    //         <td>${person.namePerson}</td>
    //         <td>
    //             <div class="d-flex justify-content-evenly">
    //                 <a class="btn btn-primary btn-icon btn-sm rounded-pill ms-2" href="#" role="button">
    //                     <span class="btn-inner">
    //                         <i class="fa-solid fa-user-plus"></i>
    //                     </span>
    //                 </a>
    //             </div>
    //         </td>
    //     `;
    //     table.appendChild(tr);
    // });
};