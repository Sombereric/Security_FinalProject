// FILE : Login.js
// PROJECT : SENG2020 - MistApp
// PROGRAMMER : Zemmat Hagos, Will Jessel, Eric Moutoux
// FIRST VERSION : 2026-3-10
// DESCRIPTION :
// all login function and api endpoint hooks used to create a login

async function login() {
    if (isLockedOut) {
        displayFeedback("loginMsg", 429, "");
        return;
    }

    const email = scrubInput(document.getElementById("loginEmail").value);
    const pass = scrubInput(document.getElementById("loginPass").value);

    if (!email || !pass) {
        displayFeedback("loginMsg", 400, "Please enter your email and password.");
        return;
    }

    try {
        const response = await fetch(`${CONFIG.API_BASE_URL}/login`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                userEmail: email,
                password: pass
            })
        });

        if (response.ok) {
            loginAttempts = 0;

            const userObj = await response.json();
            localStorage.setItem("sessionUser", JSON.stringify(userObj));

            window.location.href = "Library.html";
        } else {
            loginAttempts++;
            handleRateLimit();
            displayFeedback("loginMsg", response.status, "Invalid login.");
        }
    } catch (error) {
        console.error(error);
        displayFeedback("loginMsg", 500, "Connection failed.");
    }
}