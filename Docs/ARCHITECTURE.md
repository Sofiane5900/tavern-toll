# Architecture — Tavern's Toll

Documentation de l'architecture de **Tavern's Toll**, jeu de gestion de taverne
façon *Reigns* en **C# 13 / .NET 10** avec **Raylib-cs 8.0.0**.

Le joueur incarne l'aubergiste du *Tipsy Goblin* et doit maintenir quatre jauges
(Gold, Stock, Reputation, Authority) équilibrées pendant 90 jours. Chaque tour,
une carte-personnage apparaît et le joueur glisse à gauche ou à droite pour
prendre une décision qui affecte les jauges.

---

## 1. Architecture en couches

Le projet suit une **architecture en couches** (n-tier émergente) où chaque
dossier correspond à une responsabilité distincte. Il n'y a **pas de couche de
persistance** : tout l'état vit en mémoire, détenu par `Program.Main` pour la
durée du processus.

```
┌─────────────────────────────────────────────────────────────┐
│  Program.cs            Composition Root + Game Loop         │  bootstrap
├─────────────────────────────────────────────────────────────┤
│  Core/Managers/        Couche Présentation (rendu Raylib)   │  affiche
│   └─ HUDManager ──► CardManager + StatBarManager            │     l'état
├─────────────────────────────────────────────────────────────┤
│  Core/Services/        Couche Logique métier                │  règles du
│   └─ CardService                                            │     jeu
├─────────────────────────────────────────────────────────────┤
│  Entities/             Couche Domaine (état pur)            │  données
│   └─ GameState (root) ─► Card ─► Choice ─► Decision         │
│        └─► StatBar[]                                        │
└─────────────────────────────────────────────────────────────┘
```

### Flux de communication

```
Program.Main
   │  new GameState()        ── construit l'état initial (domaine)
   │  new HUDManager()       ── construit la présentation
   │
   └─► à chaque frame : HUDManager.DisplayHUD(gameState, ...)
            │
            ├─► CardManager.DisplayCard(gameState, ...)     ── lit GameState, dessine via Raylib
            └─► StatBarManager.DisplayBars(gameState, ...)  ── lit GameState, dessine via Raylib
```

- Le flux va **du haut vers le bas** : `Program` instancie les couches supérieures,
  qui lisent l'état des couches inférieures.
- Les Managers **lisent** `GameState` et appellent Raylib pour dessiner.
- Aucune couche inférieure ne connaît les couches supérieures (pas de dépendance
  montante) — sauf `Entities.Card` qui référence `Raylib_cs.Texture2D`.

---

## 2. Program.cs — Composition Root & Game Loop

Point d'entrée et **racine de composition** : tout le graphe d'objets y est
construit à la main (pas de conteneur d'injection de dépendances).

**Résolutions d'écran :**
| Constante | Valeur | Rôle |
|---|---|---|
| `totalScreenWidth/Height` | 1280 × 720 | fenêtre OS paysage |
| `playableScreenWidth/Height` | 540 × 960 | surface de jeu **portrait** (mobile-like) |

**Technique de rendu :** le HUD est dessiné dans une `RenderTexture2D` au format
portrait (540×960), puis blitté/mis à l'échelle au centre de la fenêtre paysage —
une « letterbox » qui simule un écran de téléphone.

**Game loop classique :**
1. `Raylib.InitWindow(...)` + chargement de la render texture.
2. `new GameState()` et `new HUDManager()` — **câblage manuel**.
3. Boucle `while (!WindowShouldClose())` :
   - `BeginTextureMode` → `hudManager.DisplayHUD(...)` → cadre → `EndTextureMode`
   - `BeginDrawing` → fond marron → `DrawTexturePro` → `EndDrawing`
4. `CloseWindow()` à la sortie.

> Câblage actuel : `Program` instancie `GameState` et `HUDManager` ;
> `HUDManager` instancie à son tour `CardManager` et `StatBarManager` en champs privés.

---

## 3. Couche Entities (Domaine)

Entités POCO avec champs privés `_camelCase` et propriétés publiques `PascalCase`.
La validation vit dans les setters (ex. `StatBar` clampe sa valeur).

```
GameState  (aggregate root)
   ├── Bars : StatBar[]          (4 jauges : Gold, Stocks, Reputation, Authority)
   ├── CurrentCard : Card        (carte affichée)
   ├── Day : int                 (jour courant, 1..90)
   └── IsGameOver : bool         (fin de run)
              │
              ▼
           Card
           ├── Name, Avatar (Texture2D), Dialogue
           ├── LeftChoice : Choice
           └── RightChoice : Choice
                    │
                    ▼
                Choice
                ├── Text, Decision (Left | Right)
                └── GoldImpact, StockImpact, ReputationImpact, AuthorityImpact (int)
```

| Entité | Rôle |
|---|---|
| **`GameState`** | Aggregate root. Snapshot central et mutable d'une run. Son constructeur sème l'état initial (4 jauges à 50/100, première carte « The Lady », jour 1). |
| **`Card`** | Une rencontre-personnage (nom, portrait, dialogue, deux choix). |
| **`Choice`** | Une option de décision (gauche/droite) et ses 4 impacts sur les jauges. |
| **`StatBar`** | Une jauge : nom, valeur courante (clamée sur `[0, Max]`), valeur max. |
| **`Decision`** | Enum : `Left` \| `Right`. |

> Note : `Card` et `GameState` référencent `Raylib_cs.Texture2D` directement, ce
> qui couple le domaine au moteur de rendu.

---

## 4. Couche Core/Services (Logique métier)

Contient les **règles du jeu**, indépendantes du rendu.

**Distinction clé — Service vs Manager :**
- `*Service` = **logique métier** (appliquer les impacts d'un choix, faire
  avancer le jeu).
- `*Manager` = **présentation/rendu** (orchestrer les appels Raylib).

| Service | Rôle |
|---|---|
| **`CardService`** | Résoudre un choix joueur. Methode `MakeChoice(Card, StatBar)` chargée d'appliquer les impacts `*Impact` d'un `Choice` aux `StatBar`. |

---

## 5. Couche Core/Managers (Présentation)

Orchestre les appels de dessin Raylib. Chaque Manager ne fait que **lire**
`GameState` et afficher.

| Manager | Rôle |
|---|---|
| **`HUDManager`** | **Facade**. Compose `CardManager` et `StatBarManager` (champs privés) et délègue via `DisplayHUD(...)`. Point d'entrée unique du rendu. |
| **`CardManager`** | Dessine la carte courante : avatar (250×250) centré, dialogue au-dessus, nom en dessous. `DisplayCard(GameState, w, h)`. |
| **`StatBarManager`** | Dessine les 4 jauges en ligne horizontale centrée en haut de l'écran. `DisplayBars(GameState, w)`. |

```
HUDManager.DisplayHUD()
      ├──► CardManager.DisplayCard()
      └──► StatBarManager.DisplayBars()
```

`Core/UIConstant.cs` centralise les constantes d'UI (tailles de police).

---

## 6. Patrons de conception

| Patron | Où | Description |
|---|---|---|
| **Facade** | `HUDManager` | Interface unifiée sur `CardManager` + `StatBarManager`. |
| **Aggregate Root** | `GameState` | Point d'entrée unique du domaine ; agrège `StatBar[]` et `Card`. |
| **Game Loop** | `Program.Main` | init → `while` update/draw → teardown. |
| **Composition manuelle** | `Program`, `HUDManager` | Câblage par `new()`, sans conteneur DI ni interfaces. |

---

## 7. Arborescence du projet

```
tavern-toll/
├── Program.cs                 Composition Root + Game Loop
├── tavern-toll.csproj         net10.0, Raylib-cs 8.0.0
├── Assets/                   Sprites (PNG)
├── Core/
│   ├── UIConstant.cs          Constantes UI
│   ├── Managers/              Présentation (Raylib)
│   │   ├── HUDManager.cs
│   │   ├── CardManager.cs
│   │   └── StatBarManager.cs
│   └── Services/              Logique métier
│       └── CardService.cs
└── Entities/                  Domaine
    ├── GameState.cs
    ├── Card.cs
    ├── Choice.cs
    ├── StatBar.cs
    └── Enums/
        └── Decision.cs
```
