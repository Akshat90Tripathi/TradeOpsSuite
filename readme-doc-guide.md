# README Documentation Guide

Purpose
- A concise authoring template and update rules for `README.md` so the repository docs stay consistent and easy to maintain.

README template (sections and content guidance)

1. Title & one-line summary
  - One-line explanation of the project purpose.

2. Introduction
  - Short paragraph (2–4 lines) describing what this repository provides and that it contains two main API hosts: Order Management and Trading Platform.

3. High-level folder structure
  - Single short tree showing `src/TradingPlatform`, `src/OrderManagement` and `*.API` hosts and `Modules/*` subfolders. Do NOT list internal files, services, or model names.

4. API host common setup
  - Explain that both `*.API` projects use Minimal APIs and include `Program.cs`, global middleware, DI composition, logging, and configuration orchestration. Keep this section general — do not explain each host individually here.

5. Main modules & sub-modules
  - For each main module (Trading Platform, Order Management): brief description (1–3 lines) and list sub-modules/features (watchlist, portfolio, trade execution; reconciliation, routing, reporting). Explain per-module responsibility in a single sentence.

6. Persistence & DbContext approach
  - State the per-sub-module `DbContext` recommendation and migration guidelines (one short paragraph).

7. Domain events & integration
  - Short guidance on domain events and cross-module integration patterns (in-process dispatch, Outbox for eventual consistency).

8. Tests
  - Mention test strategy and recommended frameworks/libraries (e.g., xUnit/NUnit, FluentAssertions, EF Core InMemory or Testcontainers for integration tests). Keep to a list.

9. EditorConfig & coding standards
  - Mention `.editorconfig` presence and coding conventions to follow.

10. Recommended next steps
  - Short actionable items (scaffold first module, add CI for migrations and tests).

Update rules (how to sync)
- Keep it clean: do not include historical notes ("we used to...") or removed tech.
- Minimal edits: update existing sections in-place rather than adding duplicate sections.
- Only add new items where they logically belong. If unsure which section to update, STOP and ask for confirmation.
- Avoid long prose; keep bullets short and factual.
- Each sync should check for duplicates and consolidate entries rather than appending new bullets repeatedly.

Tone & style
- Concise, factual, and professional. Prefer active voice and simple sentences. Use plain English — no marketing phrases.

Maintenance
- Keep this guide in the repository root as `readme-doc-guide.md` and use `sync-readme.md` prompt with Copilot for updates.
