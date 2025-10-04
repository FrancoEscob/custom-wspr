---
title: Fase 01 – Shell WinUI + hotkeys + tray + overlay básico
role: Implementation Droid
audience: Software engineer (Factory agent)
---

# Overview
- Goal: crear la app base con WinUI 3 (.NET 8), registrar hotkey global (Ctrl+Shift+M), icono en bandeja (tray) y un overlay mínimo con estados Idle/Recording (dummy) para validar UX y hotkeys.
- Dependencies: @docs/tech-decisions.md, @docs/ux-spec.md, @docs/architecture.md
- Outputs:
  - Proyecto WinUI 3 (solución .sln) estructurado.
  - Servicio de hotkeys con `RegisterHotKey`.
  - NotifyIcon en tray con menú (Abrir configuración, Salir).
  - Overlay (TopMost) con estados Idle/Recording (sin audio real aún).

# Tasks (checklist)
1) Crear solución WinUI 3 .NET 8 (Windows App SDK estable) y habilitar High DPI.
2) Implementar NotifyIcon via Shell_NotifyIcon (CsWin32), con menú contextual y tooltips.
3) Implementar registro de hotkey global Ctrl+Shift+M (RegisterHotKey) y handler WM_HOTKEY.
4) Implementar Overlay XAML TopMost con placeholder de waveform, labels de estado, botones (Record/Pause/Stop) inactivos.
5) Persistir preferencia de idioma UI (EN/es-ES) y atajo en un settings JSON (dummy).
6) Añadir logging básico en consola/archivo para eventos (hotkey, overlay open/close).

# Implementation notes
- WinUI 3 + Windows App SDK 1.8.x. CsWin32 para P/Invoke de Shell_NotifyIcon y RegisterHotKey.
- Overlay en una Window separada con TopMost y estilo sin bordes.
- Estructura de proyecto: src/app, src/services, src/ui.

# Acceptance criteria
- Al ejecutar: icono en tray visible y funcional; menú abre configuración (dummy) o cierra.
- Ctrl+Shift+M abre/cierra overlay. Cambios de estado Idle/Recording al hacer clic en botones (solo UI dummy).
- No hay crashes; hotkey se libera al salir.

# Test plan
- Probar hotkey en distintas apps (foreground). Validar que el overlay aparece sobre todas.
- Verificar que al matar el proceso no queden hotkeys registradas.

# Risks & mitigations
- Conflicto de hotkey: permitir reconfiguración futura y detectar error de registro.

# References
- [1] NotifyIcon WinUI 3
- [2] RegisterHotKey (Win32)

