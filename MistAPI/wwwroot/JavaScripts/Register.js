// FILE : Register.js
// PROJECT : SENG2020 - MistApp
// PROGRAMMER : Zemmat Hagos, Will Jessel, Eric Moutoux
// FIRST VERSION : 2026-3-10
// DESCRIPTION :
// all register functions used to call api endpoints to create a new user

async function register() {
    const user = scrubInput(document.getElementById("regUser").value);
    const email = scrubInput(document.getElementById("regEmail").value);
    const pass = scrubInput(document.getElementById("regPass").value);
    const regMsg = document.getElementById("regMsg");

    if (!user || !email || !pass) {
        displayFeedback("regMsg", 400, "Please fill in all fields.");
        return;
    }

    const specialChars = pass.match(/[^a-zA-Z0-9]/g) || [];
    if (pass.length < 10 || specialChars.length < 5) {
        displayFeedback(
            "regMsg",
            400,
            "Password must be at least 10 characters and contain at least 5 special symbols."
        );
        return;
    }

    const payload = {
        userName: user,
        userEmail: email,
        password: pass
    };

    try {
        regMsg.innerText = "Connecting to server...";

        const response = await fetch(`${CONFIG.API_BASE_URL}/register`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(payload)
        });

        if (response.ok) {
            regMsg.innerText = "Registered successfully. You can now log in.";
        } else {
            displayFeedback("regMsg", response.status, "Registration failed.");
        }
    } catch (error) {
        console.error(error);
        displayFeedback("regMsg", 500, "Connection failed.");
    }
}