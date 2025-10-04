---
title: Fase 04 – Salida y mini editor
role: Implementation Droid
audience: Software engineer (Factory agent)
deliverables:
  - Modo teclear (SendInput), pegar (Ctrl+V), copiar
  - Mini editor flotante con acciones
acceptance:
  - Seleccionar modo y completar flujo end‑to‑end
---

# Overview
- Goal: implementar modos de salida (teclear simulado, pegar, copiar) y un mini editor flotante para previsualizar/editar texto antes de enviarlo.
- Inputs (usar como contexto): @docs/ux-spec.md, @docs/architecture.md
- Outputs: servicio de Output con modos; mini editor con acciones básicas; manejo de foco y seguridad.

# Tasks (checklist)
1) Implementar OutputService con modos: Type (SendInput), Paste (clipboard+Ctrl+V), Copy (clipboard).
2) Implementar MiniEditor (WinUI): ver/editar, acciones: Send, Copy, Clear, Close.
3) Validar foco activo antes de Type/Paste; fallback a Copy si no hay foco seguro.
4) Añadir opción de “Insert at caret” vs “Replace selection”.

# Implementation notes
- Para SendInput, usar estructuras INPUT y VK de Win32; throttling y delays entre bloques para estabilidad.
- Clipboard: set text, luego sintetizar Ctrl+V si el usuario eligió Paste.
- Manejar layouts de teclado y Unicode.

# Acceptance criteria (detallados)
- El usuario puede elegir modo y enviar texto a una app activa sin errores aparentes.
- Mini editor funciona como intermediario opcional.

# Test plan
- Probar en Notepad, Word, navegadores (inputs), y campos con restricciones.
- Forzar foco inexistente: servicio retorna error claro y propone Copy.

# Risks & mitigations
- Apps elevadas (UAC): documentar limitación; ofrecer Copy como alternativa.
- Diferencias de layout/IME: evitar suposiciones sobre teclas especiales.

# References
- [22] SendInput
- [24] Clipboard APIs

# Agent brief
- Usa como contexto: @docs/ux-spec.md.
- Mantén la lógica desacoplada de la UI.