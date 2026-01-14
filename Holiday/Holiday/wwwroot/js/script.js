// Add itinerary items
const input = document.getElementById("planInput");
const list = document.getElementById("itineraryList");

function addItem() {
    const text = input.value.trim();
    if (!text) return;

    const li = document.createElement("li");

    li.innerHTML = `
    ${text}
    <button class="remove">✕</button>
  `;

    li.querySelector(".remove").onclick = () => li.remove();
    list.appendChild(li);

    input.value = "";
}

function filterDestinations() {
    const term = document.getElementById("searchBox").value.toLowerCase();
    const items = document.querySelectorAll(".destination");

    items.forEach(item => {
        const found = item.textContent.toLowerCase().includes(term);
        item.style.display = found ? "block" : "none";
    });
}


function filterDestinationTable() {
    const term = document.getElementById("searchBox").value.toLowerCase();
    const rows = document.querySelectorAll(".destination-row");

    rows.forEach(row => {
        const rowText = row.innerText.toLowerCase();
        row.style.display = rowText.includes(term) ? "" : "none";
    });
}
