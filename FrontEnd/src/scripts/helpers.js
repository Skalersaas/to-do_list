const getCookieByName = (name) => {
    const cookies = document.cookie.split(';');
    for (let cookie of cookies) {
        cookie = cookie.trim();
        
        if (cookie.startsWith(`${name}=`)) {
            return cookie.substring(name.length + 1);
        }
    }
    return null;
}

const getFormattedDay = (dateString) => {
    const date = new Date(dateString);
    if (isNaN(date)) return "Invalid Date";
    const monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    const month = monthNames[date.getMonth()];
    const day = date.getDate();
    return `${month} ${day}`;
}
export { getCookieByName, getFormattedDay}