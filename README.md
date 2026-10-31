# Game Hub Server

Backend pro Game Hub – frontend aplikaci v Angularu. Postaveno na **.NET 10 / ASP.NET Core** podle principů **Clean Architecture**.

> Tréninkový projekt pro praxi backend vývoje v C#.

## Struktura

```
src/
├── GameHub.Api             # ASP.NET Core Web API – vstupní bod, endpointy, DI
├── GameHub.Application     # Use-casy, DTO, rozhraní pro vnější svět (repozitáře, API klienti)
├── GameHub.Domain          # Entity, value objecty a doménová pravidla – čisté C# bez závislostí
├── GameHub.Infrastructure  # Implementace rozhraní – přístup k datům, externí služby
└── GameHub.Importer        # Konzolová aplikace pro import dat
```

Závislosti směřují dovnitř:

```
Api            ─► Application, Infrastructure
Infrastructure ─► Application, Domain
Importer       ─► Infrastructure
Application    ─► Domain
Domain         ─► (nic)
```

`Domain` je jádro a nezná žádnou jinou vrstvu. `Application` nad ním orchestruje use-casy, `Infrastructure` implementuje rozhraní z `Application` a `Api` / `Importer` vše propojují.

## Požadavky

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## Spuštění

```bash
dotnet build                                   # sestavení celé solution
dotnet run --project src/GameHub.Api           # API na http://localhost:5206
dotnet run --project src/GameHub.Importer      # import dat
```
