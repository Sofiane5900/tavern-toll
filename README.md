# Tavern's Toll

A Reigns-like tavern management game built with **C#** and **.NET 10**. You play
the innkeeper of the *Tipsy Goblin*, a tavern in the rough part of town, and try
to keep the doors open for 90 days.

<p align="center">
  <em>Balance the books. Pour the beer. Don't get shut down.</em>
</p>

---

## How it plays

Each round a character walks into the tavern with a request. You swipe the card
**left** or **right** to make your call. Every decision nudges one of four meters:

| Meter | Meaning |
|-------|---------|
| **Gold** | The coin in your register |
| **Stock** | Your reserves of beer and meat |
| **Reputation** | How much the locals love the place |
| **Authority** | How much the town guard tolerates you |

Let any meter hit its ceiling or its floor and your run is over. Balance them
long enough to outlast the season and you win. It's a game of tight trade-offs
where no choice is ever free.

## Tech stack

- **C# 13** on **.NET 10**
- **Raylib-cs 8.0.0** —  C# bindings for raylib, for graphics, input and audio
- **MSBuild / .NET CLI** for the build pipeline

## Build & run

```sh
dotnet build -c Release
dotnet run --project tavern-toll -c Release
```

> Requires the **.NET 10 SDK**.

Sound and background music can be toggled from the options menu in-game.
