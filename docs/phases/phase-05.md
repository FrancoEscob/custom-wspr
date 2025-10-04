---
title: Fase 05 – Preferencias/UX + comandos de dictado
role: Implementation Droid
audience: Software engineer (Factory agent)
deliverables:
  - Panel de ajustes (idioma UI, hotkeys, VAD, modelos, salida, privacidad)
  - Comandos de dictado (puntuación, nueva línea, borrar última frase)
acceptance:
  - Preferencias persistentes; comandos aplicados en editor/salida
---
 
# Overview
- Goal: crear el panel de ajustes y soportar comandos de dictado (p. ej., "punto", "nueva línea", "borrar última frase").
- Inputs (usar como contexto): @docs/ux-spec.md, @docs/tech-decisions.md, @docs/architecture.md
- Outputs: SettingsService (persistencia JSON), UI de preferencias, parser de comandos.
 
# Tasks (checklist)
1) SettingsService: persistir idioma UI, hotkeys, VAD, modelo, salida, privacidad.
2) SettingsUI: WinUI páginas/tabs; validaciones y valores por defecto.
3) Parser de comandos con lista configurable por idioma.
4) Aplicar comandos en tiempo real al texto en mini editor.
 
# Implementation notes
- Carpeta AppData para settings.json. Validar esquemas.
- Comandos multilínea y de edición deben ser reversibles (undo simple).
 
# Acceptance criteria (detallados)
- Cambios de preferencias sobreviven restart.
- Comandos de dictado se aplican correctamente con ejemplos en ES/EN.
 
# Test plan
- Cambiar hotkeys/modelo y verificar efecto.
- Ejecutar 10 ejemplos de comandos en ES/EN.
 
# Risks & mitigations
- Colisiones de hotkeys: detección y aviso.
- Ambigüedad de comandos: prefijos claros y lista por idioma.
 
# Agent brief
- Usa como contexto: @docs/ux-spec.md, @docs/tech-decisions.md.
- Mantén el alcance a configuración y comandos; no modificar ASR.
