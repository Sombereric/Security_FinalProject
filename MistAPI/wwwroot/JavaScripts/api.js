// FILE : api.js
// PROJECT : SENG2020 - MistApp
// PROGRAMMER : Zemmat Hagos, Will Jessel, Eric Moutoux
// FIRST VERSION : 2026-3-10
// DESCRIPTION :
// handles api calls and items

const API_BASE = "https://localhost:5001/api";

function getToken() {
    return localStorage.getItem("token");
}