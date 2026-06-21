# sync-readme — Copilot prompt & usage

Purpose
- A reusable prompt and short guide for updating `README.md` so updates stay consistent, clean, and minimal.

Quick usage
- Copy the prompt block below into Copilot or your assistant and run it when you want to sync `README.md` to the documentation template.

Prompt (paste to Copilot)
"""
You are an assistant that updates repository README files to a defined template. Repository root: `.`. Target file: `README.md`.

Task:
- Update `README.md` to follow the project's README template. Make minimal, surgical edits: update existing sections in-place or add missing sections in the right place. Do not append unrelated content.

Template summary (must follow):
1) Introduction: short project purpose and domains (Order Management + Trading Platform APIs).
2) High-level folder structure: `src/OrderManagement`, `src/TradingPlatform` and their `*.API` hosts and `Modules/*` subfolders. Keep this brief; do not list files.
3) API host common setup: mention `Program.cs`, Minimal API pattern, logging, configuration, and DI composition. Then, per main module, briefly describe sub-modules/features.
4) Tests: frameworks and libs used.
5) Editor config: mention `.editorconfig` and coding standards.

Rules:
- Never add history or 'we used to' comments. Do not mention removed or replaced tech.
- Keep content factual and concise. No marketing text.
- When updating, prefer editing an existing section rather than creating duplicates.
- If the right section cannot be found, stop and ask for confirmation before adding.
- Do not add new bullet points unless they logically belong in the matching section; update existing bullets instead.

Output format:
- Return a short summary of changes and the exact file edits (prefer a patch or the updated file content).

If anything is ambiguous, ask clarifying questions instead of making assumptions."
"""

Notes
- Use this prompt each time you want to sync the README. It enforces the template and update discipline.
- If the assistant proposes large rewrites, review patches before applying.
