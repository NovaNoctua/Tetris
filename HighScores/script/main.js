const list = document.getElementById("highscoreList");

// Formatter time → dd.mm - hh.mm.ss
function formatDate(isoDate) {
  const d = new Date(isoDate);

  const pad = (n) => (n < 10 ? "0" + n : n);

  const day = pad(d.getDate());
  const month = pad(d.getMonth() + 1);
  const hours = pad(d.getHours());
  const minutes = pad(d.getMinutes());
  const seconds = pad(d.getSeconds());

  return `${day}.${month} - ${hours}.${minutes}.${seconds}`;
}

// Charger les highscores
async function loadHighscores() {
  console.log("test");

  const res = await fetch("./highscores.json");
  const data = await res.json();

  // Tri : score décroissant
  data.sort((a, b) => b.score - a.score);

  list.innerHTML = "";
  data.forEach((score) => {
    const li = document.createElement("li");

    const linesTxt = score.linesDestroyed <= 1 ? "ligne" : "lignes";

    const dateFormatted = formatDate(score.time);

    li.innerHTML = `
      <span>
          <strong>${score.player}</strong> —
          <span class="score">${score.score} pts</span> —
          ${score.linesDestroyed} ${linesTxt}
          <span class="date">${dateFormatted}</span>
      </span>
    `;
    list.appendChild(li);
  });
}

// Chargement initial
loadHighscores();
