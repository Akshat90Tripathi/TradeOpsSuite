# Modular Monolith — Project Overview

This repository follows a Modular Monolith architecture using Vertical Slice principles, Minimal APIs, and Entity Framework Core. The goal is clear module boundaries (feature folders), domain-first design with business rules in domain entities, and separate DbContexts per module where appropriate.

## Principles
- Vertical Slice: features own their handlers, DTOs, validation, and persistence.
- Minimal APIs: small surface area for each module; endpoints registered by module.
- EF Core directly: use EF Core for persistence, per-module `DbContext` instances.
- Domain entities: business rules live inside entities/aggregates (rich domain model).
- Feature folders: group by feature/module rather than technical layers.
- Separate DbContexts: keep persistence concerns localized to modules; share only when necessary.
- Domain events: use domain events to decouple cross-module side-effects.

## High-level layout

Root layout (example) — main modules with nested sub-modules:

```
src/
  TradingPlatform/                  -> main module (host + composition)
    TradingPlatform.API/            -> Program.cs, Middleware, Extensions, DI composition
    Modules/
      Watchlist/
        Watchlist.Application/
        Watchlist.Domain/
        Watchlist.Infrastructure/
      Portfolio/
      TradeExecution/

  OrderManagement/                  -> main module (host + composition)
    OrderManagement.API/            -> Program.cs, Middleware, Extensions, DI composition
    Modules/
      Reconciliation/
      Routing/
      Reporting/

  (No top-level `Shared/` folder)
  Note: There is no global shared module between `TradingPlatform` and `OrderManagement`. Common primitives (DTOs, value objects, integration contracts) should live inside the relevant main module (for example `TradingPlatform/Contracts` or `OrderManagement/Contracts`).

  my-app.sln
```

Top-level main modules (e.g., `TradingPlatform`, `OrderManagement`) host the runtime and expose the public API surface through their `*.API` project. Sub-modules under `Modules/` contain the vertical slices (Application, Domain, Infrastructure).

Integration points:
- The main `*.API` project provides `Program.cs`, global middleware, and composition extensions (e.g., `AddTradingPlatform()` and `MapTradingPlatformEndpoints()`).
- Sub-modules expose their registration helpers (e.g., `AddWatchlistModule(IServiceCollection)`, `MapWatchlistEndpoints(WebApplication)`) and keep domain/persistence internal.

- Each module that needs persistence defines its own `DbContext` (e.g., `OrdersDbContext`).
- Keep schema ownership per module. If two modules must share tables, consider a shared module or explicit integration boundaries.
- Register DbContexts with specific lifetimes and connection strings in module registration.
- Use migrations per module (folder-specific migrations) to keep changes isolated.
## DbContext strategy
- Prefer per-sub-module `DbContext` to keep schema ownership localized (e.g., `WatchlistDbContext`, `TradeExecutionDbContext`).
- The main module can host a shared `DbContext` only if sub-modules are strongly coupled and must participate in the same transactions.
- Register DbContexts inside the sub-module registration (called by the main module's `Add...()` composition method).
- Keep migrations per sub-module to keep changes isolated and easier to manage.
- Each module that needs persistence defines its own `DbContext` (e.g., `OrdersDbContext`).
- Keep schema ownership per module. If two modules must share tables, consider a shared module or explicit integration boundaries.
- Register DbContexts with specific lifetimes and connection strings in module registration.
- Use migrations per module (folder-specific migrations) to keep changes isolated.

## Domain events
- Domain events are raised by entities/aggregates during state changes.
- Persist entities via EF Core; after `SaveChanges` dispatch domain events (in-process) to handlers.
- Handlers perform side effects (other module integration should be via explicit application services or outbox patterns for eventual consistency when needed).

## Minimal API & Vertical Slice
- Keep each endpoint as a small vertical slice: request DTO → handler → response DTO.
- The main `*.API` project composes sub-modules and maps their endpoints. Example pattern in `Program.cs` of `TradingPlatform.API`:

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTradingPlatform(builder.Configuration); // internally registers sub-modules
var app = builder.Build();
app.MapTradingPlatformEndpoints(); // internally maps Watchlist, Portfolio, TradeExecution
app.Run();
```

## Conventions
- Top-level folders: `TradingPlatform/` and `OrderManagement/` (each contains a `*.API` host and `Modules/` subfolders).
- Sub-module folders: `Modules/{Feature}/{Application,Domain,Infrastructure}`.
- The `*.API` project is the composition root and public surface for its main module. Keep domain/persistence in sub-modules and avoid adding `Program.cs` per sub-module unless you need independent deployment.
- Keep domain model free from framework concerns (no EF Core attributes required — prefer Fluent mappings in Infrastructure).
- Use explicit DTOs for API boundaries; avoid leaking EF entities outward.

## Example: domain event dispatch (pattern)

1. Entity raises `DomainEvent` (adds to an internal list).
2. Repository persists entity via EF Core.
3. After `SaveChanges`, a dispatcher reads events and invokes handlers in-process.

For distributed or reliable cross-module communication, consider an Outbox pattern.

## Recommended next steps
- Scaffold one module (Orders) end-to-end to validate conventions.
- Add CI steps for module-specific migrations and tests.
- Decide on a domain-event dispatch approach (simple in-memory vs mediator vs outbox).

---

This README is a starting point. Tell me if you want a generated concrete example: a scaffolded `Orders` module with `DbContext`, a minimal API endpoint, and a simple domain event flow. I can create it next.

## README sync & authoring tools

To keep `README.md` consistent we store two helper files at the repo root:
- `sync-readme.md` — a Copilot/assistant prompt that instructs how to update `README.md` to the project template.
- `readme-doc-guide.md` — the README documentation guide and template describing required sections and update rules.

Usage rules:
- Use `sync-readme.md` with Copilot or an assistant to propose precise README edits.
- Follow `readme-doc-guide.md` for what content belongs in each section. Do not add historical notes or "used to" statements.
- If the assistant cannot find the appropriate section to update, it must ask for confirmation before adding new content.


## Project roadmap — Trading App & Order Management

Planned micro-apps inside this repository:

- Trading App (priority):
  - Core features: Watchlist (live feed), Portfolio, Trade screen (Buy / Sell).
  - Watchlist should support a live market feed (push/websocket) for near-real-time updates.
  - Backend-first approach: implement APIs for each feature, add frontend later.

- Order Management App (secondary):
  - Administrative and operational features for order lifecycle, reconciliation, reporting.
  - May share the same physical database but will be developed as a separate module/micro-app with clear boundaries.

Development approach

- Start with the Trading App and implement feature-by-feature (piece by piece).
- Each feature is a vertical slice: API endpoint → application handler → domain model → persistence.
- Prefer separate modules under `Modules/Trading` and `Modules/OrderManagement` following the existing conventions.
- Keep shared primitives inside the owning main module (e.g., `TradingPlatform/Contracts` or `OrderManagement/Contracts`) rather than a global `Shared/` folder.

Note: per request, no implementation will be started now—this README was updated only. When ready, I can scaffold the first Trading module and its initial APIs.

## Main modules and sub-modules

You can organize the system into top-level (main) modules that contain focused sub-modules. This keeps large bounded contexts readable while preserving modular isolation.

Suggested layout:

```
src/
  Modules/
    TradingPlatform/                -> main module (parent)
      TradingPlatform.Api/
      TradingPlatform.Application/
      TradingPlatform.Domain/
      TradingPlatform.Infrastructure/
      Modules/                       -> sub-modules folder
        Watchlist/
          Watchlist.Api/
          Watchlist.Application/
          Watchlist.Domain/
          Watchlist.Infrastructure/
        Portfolio/
        TradeExecution/

    OrderManagement/                 -> another main module
      OrderManagement.Api/
      OrderManagement.Application/
      OrderManagement.Domain/
      OrderManagement.Infrastructure/
      Modules/
        Reconciliation/
        Routing/
        Reporting/

  (No top-level `Shared/` folder)
```

Conventions and patterns

- Parent module responsibilities: register common services, configuration, and act as a composition root for its sub-modules (e.g., `AddTradingPlatform()` calls `AddWatchlist()` and `AddPortfolio()`).
- Sub-modules are full vertical slices and may have their own `DbContext` or share a parent-level context depending on cohesion and transactional needs.
- Prefer per-sub-module `DbContext` for isolation; use a parent-level context only when sub-modules share strongly-coupled aggregates.
- Map endpoints with hierarchical prefixes: `/api/trading/watchlist`, `/api/trading/portfolio`, `/api/orders/reconciliation`.
- Domain events should be raised and handled within the sub-module where possible. For cross-sub-module effects, use explicit integration (application services, domain events dispatched by parent, or an Outbox for eventual consistency).
- Keep cross-cutting contracts and shared DTOs inside the owning main module (for example `TradingPlatform/Contracts`) and expose only necessary contract assemblies to dependents.

Registration pattern example (conceptual):

```csharp
public static IServiceCollection AddTradingPlatform(this IServiceCollection services, IConfiguration config)
{
    services.Configure<TradingOptions>(config.GetSection("Trading"));
    services.AddWatchlistModule(config);
    services.AddPortfolioModule(config);
    services.AddTradeExecutionModule(config);
    return services;
}

public static WebApplication MapTradingPlatformEndpoints(this WebApplication app)
{
    app.MapWatchlistEndpoints();
    app.MapPortfolioEndpoints();
    app.MapTradeExecutionEndpoints();
    return app;
}
```

When to nest vs separate projects

- Nest (sub-modules) when the domain belongs to the same large bounded context (e.g., Trading features).
- Separate top-level modules/projects when you want strict runtime isolation, independent deployments, or different ownership (e.g., separate micro-apps for Trading and Order Management).

This structure gives you flexibility: develop Trading features as cohesive sub-modules under `TradingPlatform`, and keep Order Management similarly organized. When you're ready, I can scaffold `TradingPlatform.API` with a `Watchlist` sub-module template (Program.cs, startup DI, a sample endpoint and `DbContext`).
