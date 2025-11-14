# 🎮 Tetris Console – Projet C#

Un projet Tetris développé en C# (console), réalisé dans le cadre de l’ETML.
Le jeu reproduit les mécaniques classiques de Tetris tout en étant entièrement affiché dans la console : gestion des pièces, collisions, rotation, lignes complètes, score et prochaine pièce.

---

## 📌 Fonctionnalités principales

### ✔ Déplacements & rotation

* Déplacement **gauche / droite**
* Descente **accélérée**
* **Rotation** avec la barre espace
* Gestion complète des collisions

### ✔ Génération des pièces

* Pièces générées aléatoirement
* Positionnement initial centré
* Affichage de la **prochaine pièce**

### ✔ Lignes complètes

* Détection des lignes pleines
* Suppression automatique
* Mise à jour du score (+500 pts par ligne)

### ✔ Système de score

* Score actuel affiché en continu
* Nombre de lignes détruites

### ✔ Pause (touche P)

* Le jeu se fige complètement
* Les pièces déjà tombées **restent affichées**
* Texte “PAUSE” centré
* Reprise propre **sans clignotement**

### ✔ Fin de partie

La partie se termine si une nouvelle pièce ne peut pas être placée.

---

## 🧩 Structure du projet

```
Tetris/
│
├── Game.cs              → Boucle de jeu principale, logique Tetris
├── GameGrid.cs          → Gestion de la grille et des collisions
├── Block/               → Pièces du Tetris
│   ├── BlockI.cs
│   ├── BlockJ.cs
│   ├── BlockL.cs
│   ├── BlockO.cs
│   ├── BlockS.cs
│   ├── BlockT.cs
│   └── BlockZ.cs
│
├── Square.cs            → Objet graphique représentant un carré
├── Position.cs          → Gestion des coordonnées
├── Config.cs            → Constantes liées à l'affichage
├── Custom.cs            → Méthodes utilitaires
├── Start.cs             → Lancement du jeu
└── Program.cs           → Point d’entrée
```

---

## 🎮 Commandes

| Touche | Action               |
| ------ | -------------------- |
| ←      | Déplacer à gauche    |
| →      | Déplacer à droite    |
| ↓      | Descendre plus vite  |
| Espace | Rotation de la pièce |
| P      | Pause / Reprendre    |

---

## 🛠️ Technologies utilisées

* **C# / .NET Console**
* `System.Console` pour le rendu
* `GetAsyncKeyState()` via `user32.dll` pour les touches en temps réel
* `DateTime` + `Thread.Sleep()` pour le timing

---

## 📦 Installation & lancement

1. Cloner le dépôt

   ```bash
   git clone <url-du-repo>
   ```

2. Ouvrir la solution Visual Studio

   ```
   Tetris.sln
   ```

3. Lancer le projet via Visual Studio (`F5`) ou exécuter `Program.cs`.

---
