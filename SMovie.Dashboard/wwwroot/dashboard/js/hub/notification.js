const connection = new signalR.HubConnectionBuilder()
    .withUrl("/notification")
    .build();

(async () => {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    }
    catch (e) {
        console.error(e.toString());
    }
})();

connection.on("SendNotification", (result) => {
    try {
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