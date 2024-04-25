const connection = new signalR.HubConnectionBuilder()
    .withUrl("/notification")
    .build();

(async () => {
    try {
        await connection.start();
        console.log("Connected Successfully.");
    }
    catch (e) {
        console.error(e.toString());
    }
})();

connection.on("SendNotification", (result) => {
    try {
        listNotifs.push(result);
        createNotification(result);
    } catch (e) {
        console.error(e.toString());
    }
})

const createNotification = (notification) => {
    $("#display-notification").append(renderNotificationTag(notification));
}

const renderNotificationTag = (notification) => {
    let tag = `
        <a href="#" class="iq-sub-card">
                    <div class="d-flex align-items-center">
                        <img class="p-1 avatar-40 rounded-pill bg-soft-primary"
                             src="${notification.avatar}" alt="" loading="lazy">
                        <div class="ms-3 w-100">
                            <h6 class="mb-0 ">${notification.typeMessage}</h6>
                            <div class="d-flex justify-content-between align-items-center">
                                <p class="mb-0"><strong>${notification.displayName}</strong> ${notification.message}</p>
                                <small class="float-end font-size-12">${getTimeSender(notification.createdDate)}</small>
                            </div>
                        </div>
                    </div>
                </a>
    `;

    return tag;
}

const checkIsExistUnRead = () => {
    let index = listNotifs.findIndex((item) => !item.isRead);

    if (index > -1) {
        return true;
    }

    return false;
}

listNotifs.forEach((item) => {
    createNotification(item);
    createNotification(item);
    createNotification(item);
    createNotification(item);
    createNotification(item);
    createNotification(item);

    if (checkIsExistUnRead) {
        $(".dots").addClass("display-noti-unread")
    }
})

$(document).ready(() => {

    const element = $("#display-notification");
    element.on("scroll", () => {
        const scrollTop = element.scrollTop();
        const height = element.outerHeight();
        const scrollHeight = element[0].scrollHeight;
        console.log(scrollTop + height)
        console.log(scrollHeight)
        if (scrollTop + height >= scrollHeight) {
            getNotification();
        }
    })

});