// Declare body in global scope
let body;

document.addEventListener("DOMContentLoaded", function () {
    body = document.body;

    // Apply saved preferences when page loads
    if (localStorage.getItem("theme") === "dark") {
        body.classList.add("dark-mode");
    }

    if (localStorage.getItem("contrastMode") === "high-contrast") {
        body.classList.add("high-contrast");
    }

    if (localStorage.getItem("fontSize")) {
        body.style.fontSize = localStorage.getItem("fontSize");
    }
});

// Dark Mode
function toggleDarkMode() {
    if (!body) body = document.body; // Fallback if called before DOMContentLoaded
    body.classList.toggle("dark-mode");
    localStorage.setItem("theme", body.classList.contains("dark-mode") ? "dark" : "light");
}
window.toggleDarkMode = toggleDarkMode;

// High Contrast Mode
function toggleHighContrast() {
    if (!body) body = document.body; // Fallback if called before DOMContentLoaded
    body.classList.toggle("high-contrast");
    localStorage.setItem("contrastMode", body.classList.contains("high-contrast") ? "high-contrast" : "normal");
}
window.toggleHighContrast = toggleHighContrast;

// Text-to-Speech
function speakText() {
    let text = document.body.innerText;
    let speech = new SpeechSynthesisUtterance(text);
    speech.lang = 'en-GB';
    window.speechSynthesis.speak(speech);
}
window.speakText = speakText;

// Font Size Adjustment
function changeFontSize(action) {
    if (!body) body = document.body; // Fallback if called before DOMContentLoaded
    let currentSize = parseInt(window.getComputedStyle(body).fontSize);
    if (action === 'increase') {
        currentSize += 2;
    } else if (action === 'decrease') {
        currentSize -= 2;
    }
    body.style.fontSize = currentSize + "px";
    localStorage.setItem("fontSize", currentSize + "px");
}
window.changeFontSize = changeFontSize;