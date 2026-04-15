//handles authentication and api connections

const API_BASE = "https://localhost:5001/api";

function getToken() {
    return localStorage.getItem("token");
}