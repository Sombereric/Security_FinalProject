// FILE : commonAuthentication.js
// PROJECT : SENG2020 - MistApp
// PROGRAMMER : Zemmat Hagos, Will Jessel, Eric Moutoux
// FIRST VERSION : 2026-3-10
// DESCRIPTION :
// all common authentication functions shared between login and register

//shared functions between the register new account and login pages
let loginAttempts = 0
let isLockedOut = false;

//used to clean user input
function scrubInput(text) {
    if (typeof text !== 'string') return text;
    let clean = text.trim();
    clean = clean.replace(/[;<>"-]/g, "");
    return clean;
}

//used to display feed back from function calls using the statuscode
function displayFeedback(targetId, statusCode, rawMessage) {
    const secureDiv = document.getElementById(targetId);
    if (!secureDiv) return;

    let displayMsg = "";
    if (statusCode === 401 || statusCode === 403) {
        displayMsg = "Invalid credentials.";
    } else if (statusCode === 409) {
        displayMsg = "That email is already in use.";
    } else if (statusCode === 429) {
        displayMsg = "Too many attempts. Wait 30 seconds.";
    } else if (statusCode === 500) {
        displayMsg = "Server error. Try again later.";
    } else {
        displayMsg = rawMessage;
    }

    secureDiv.innerText = displayMsg;
}

//used to determine if someone has attempted a login two many times within a short time frame
function handleRateLimit() {
    if (loginAttempts >= CONFIG.MAX_LOGIN_ATTEMPTS) {
        isLockedOut = true;

        const botton = document.getElementById("loginBtn");
        if (botton) botton.style.opacity = "0.5";

        setTimeout(() => {
            isLockedOut = false;
            loginAttempts = 0;
            if (botton) botton.style.opacity = "1";
            displayFeedback("loginMsg", 200, "You may try to login again.");
        }, CONFIG.COOLDOWN_MS);
    }
}