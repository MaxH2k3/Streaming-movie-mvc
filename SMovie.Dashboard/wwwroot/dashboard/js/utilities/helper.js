function getTimeSender(date) {
    var currentTime = new Date();
    var createdDate = new Date(date);

    var timeSpan = currentTime - createdDate;

    if (timeSpan / (1000 * 3600 * 24) > 1) {
        return createdDate.toLocaleDateString('en-GB');
    }

    if (timeSpan / (1000 * 3600) > 1) {
        return Math.floor(timeSpan / (1000 * 3600)) + "h";
    }

    if (timeSpan / (1000 * 60) > 1) {
        return Math.floor(timeSpan / (1000 * 60)) + "m";
    }

    return Math.floor(timeSpan / 1000) + "s";
}