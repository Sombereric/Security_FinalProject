// FILE : Library.js
// PROJECT : SENG2020 - MistApp
// PROGRAMMER : Zemmat Hagos, Will Jessel, Eric Moutoux
// FIRST VERSION : 2026-3-10
// DESCRIPTION :
// all functions related to the users owned library

const sessionUser = JSON.parse(localStorage.getItem("sessionUser");

if (!sessionUser) {
    window.location.href = "Login.html";
}

document.addEventListener("DOMContentLoaded", () => {
    loadLibrary();
});

function logout() {
    localStorage.removeItem("sessionUser");
    window.location.href = "Login.html";
}

async function loadLibrary() {
    const welcomeMsg = document.getElementById("welcomeMsg");
    const libraryMsg = document.getElementById("libraryMsg");
    const libraryList = document.getElementById("libraryList");

    welcomeMsg.innerText = `Welcome, ${sessionUser.userName}`;
    libraryMsg.innerText = "Loading your library...";
    libraryList.innerHTML = "";

    try {
        const response = await fetch(`${CONFIG.API_BASE_URL.replace("/authentication", "/library")}/${sessionUser.userID}`);

        if (!response.ok) {
            libraryMsg.innerText = "Could not load your library.";
            return;
        }

        const ownedGames = await response.json();

        if (!ownedGames || ownedGames.length === 0) {
            libraryMsg.innerText = "You do not own any games yet.";
            return;
        }

        libraryMsg.innerText = "";
        renderLibrary(ownedGames);
    } catch (error) {
        console.error(error);
        libraryMsg.innerText = "Connection failed while loading library.";
    }
}

function renderLibrary(ownedGames) {
    const libraryList = document.getElementById("libraryList");
    libraryList.innerHTML = "";

    ownedGames.forEach(ownedGame => {
        const game = ownedGame.game;

        if (!game) return;

        const card = document.createElement("div");
        card.className = "library-card";

        const publisherName = game.publisher ? game.publisher.publisherName : "Unknown Publisher";
        const addedDate = ownedGame.dateAdded
            ? new Date(ownedGame.dateAdded).toLocaleDateString()
            : "Unknown Date";

        card.innerHTML = `
            <h3>${game.gameName}</h3>
            <p><strong>Publisher:</strong> ${publisherName}</p>
            <p><strong>Genre:</strong> ${game.gameGenre ?? "Unknown"}</p>
            <p><strong>Price:</strong> $${Number(game.gamePrice).toFixed(2)}</p>
            <p><strong>Added:</strong> ${addedDate}</p>
            <button type="button" data-gameid="${game.gameID}">Download</button>
        `;

        const button = card.querySelector("button");
        button.addEventListener("click", () => downloadGame(game.gameID));

        libraryList.appendChild(card);
    });
}

async function downloadGame(gameId) {
    const libraryMsg = document.getElementById("libraryMsg");

    try {
        const response = await fetch(`${CONFIG.API_BASE_URL.replace("/authentication", "/library")}/download`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                userID: sessionUser.userID,
                gameID: gameId
            })
        });

        const text = await response.text();

        if (response.ok) {
            libraryMsg.innerText = text;
        } else {
            libraryMsg.innerText = text || "Download failed.";
        }
    } catch (error) {
        console.error(error);
        libraryMsg.innerText = "Connection failed while downloading.";
    }
}