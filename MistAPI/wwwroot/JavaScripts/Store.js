// FILE : Store.js
// PROJECT : SENG2020 - MistApp
// PROGRAMMER : Zemmat Hagos, Will Jessel, Eric Moutoux
// FIRST VERSION : 2026-3-10
// DESCRIPTION :
// all functions used to buy and interact with the store

const sessionUser = JSON.parse(localStorage.getItem("sessionUser"));

if (!sessionUser) {
    window.location.href = "Login.html";
}

document.addEventListener("DOMContentLoaded", () => {
    loadGamesByName();
});

function logout() {
    localStorage.removeItem("sessionUser");
    window.location.href = "Login.html";
}

function getGamesBaseUrl() {
    return `${CONFIG.API_BASE_URL}/Games`;
}

function getLibraryBaseUrl() {
    return `${CONFIG.API_BASE_URL}/Library`;
}

async function loadGamesByName() {
    await loadGames(`${getGamesBaseUrl()}/sort/name`);
}

async function loadGamesByPrice() {
    await loadGames(`${getGamesBaseUrl()}/sort/price`);
}

async function loadGamesByGenre() {
    const genre = document.getElementById("genreInput").value.trim();

    if (!genre) {
        document.getElementById("storeMsg").innerText = "Please enter a genre.";
        return;
    }

    await loadGames(`${getGamesBaseUrl()}/genre/${encodeURIComponent(genre)}`);
}

async function loadGamesByPublisher() {
    const publisher = document.getElementById("publisherInput").value.trim();

    if (!publisher) {
        document.getElementById("storeMsg").innerText = "Please enter a publisher.";
        return;
    }

    await loadGames(`${getGamesBaseUrl()}/publisher/${encodeURIComponent(publisher)}`);
}

async function loadGames(url) {
    const storeMsg = document.getElementById("storeMsg");
    const storeList = document.getElementById("storeList");

    storeMsg.innerText = "Loading games...";
    storeList.innerHTML = "";

    try {
        const response = await fetch(url);

        if (!response.ok) {
            storeMsg.innerText = "Could not load games.";
            return;
        }

        const games = await response.json();

        if (!games || games.length === 0) {
            storeMsg.innerText = "No games found.";
            return;
        }

        storeMsg.innerText = "";
        renderGames(games);
    } catch (error) {
        console.error(error);
        storeMsg.innerText = "Connection failed while loading games.";
    }
}

function renderGames(games) {
    const storeList = document.getElementById("storeList");
    storeList.innerHTML = "";

    games.forEach(game => {
        const card = document.createElement("div");
        card.className = "store-card";

        const publisherName = game.publisher ? game.publisher.publisherName : "Unknown Publisher";

        card.innerHTML = `
            <h3>${game.gameName}</h3>
            <p><strong>Publisher:</strong> ${publisherName}</p>
            <p><strong>Genre:</strong> ${game.gameGenre ?? "Unknown"}</p>
            <p><strong>Price:</strong> $${Number(game.gamePrice).toFixed(2)}</p>
            <button type="button" data-gameid="${game.gameID}">Buy</button>
        `;

        const button = card.querySelector("button");
        button.addEventListener("click", () => buyGame(game.gameID));

        storeList.appendChild(card);
    });
}

async function buyGame(gameId) {
    const storeMsg = document.getElementById("storeMsg");

    try {
        const response = await fetch(`${getLibraryBaseUrl()}/buy`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                userID: sessionUser.userID,
                gameID: gameId
            })
        });

        const text = await response.text();

        if (response.ok) {
            storeMsg.innerText = text || "Game purchased successfully.";
        } else {
            storeMsg.innerText = text || "Purchase failed.";
        }
    } catch (error) {
        console.error(error);
        storeMsg.innerText = "Connection failed while purchasing.";
    }
}