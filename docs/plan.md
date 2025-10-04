---
title: Plan ejecutivo y alcance
description: Resumen, objetivos, alcance, decisiones clave y cronograma
---

# Vortex ASR – Executive Summary

Objetivo: crear una app para Windows 11 de dictado/transcripción 100% local, rápida, estética y minimalista, con overlay invocable por atajo global, soporte ES/EN, VAD, salida flexible (pegar, teclear simulado, portapapeles, mini‑editor) y gestión de múltiples modelos locales.

Hardware objetivo: AMD Ryzen 5 5600G, 32 GB RAM (sin GPU obligatoria). Interfaz: inglés por defecto, con español seleccionable.

Stack propuesto (resumen):
- UI: WinUI 3 + Windows App SDK (C# .NET 8). Hotkeys globales con `RegisterHotKey` [2], icono de bandeja con `Shell_NotifyIcon` [1].
- Audio: NAudio (WASAPI) micrófono (16 kHz, mono), futuro loopback (opcional) [3][4]. VAD: Silero VAD ONNX int8 [7][9].
- ASR (CPU): opción A) Faster‑Whisper/CTranslate2 int8 [10][11]; opción B) ONNX Runtime all‑in‑one Whisper int8 (Olive) [12][13].
- Salida: SendInput/SendKeys para tecleo [22][24], pegado Ctrl+V, portapapeles, mini‑editor flotante.
- Empaquetado: MSIX + App Installer (.appinstaller) para auto‑update sideload [25][26]. 100% local; opcional nube futuro.

MSIX/App Installer (explicación corta):
- MSIX es el formato de instalación de Windows moderno. El archivo .appinstaller es un descriptor que publicas en una URL; cuando el usuario instala desde ahí, Windows comprueba esa URL periódicamente (o al iniciar) y si hay una versión nueva, propone/realiza la actualización automáticamente, sin pasar por la Store. Para activarlo con prompts al iniciar, se usa el schema 2021 con atributos `OnLaunch` (ShowPrompt/UpdateBlocksActivation).

Modos de operación clave:
- Overlay invocado por hotkey (sugerido: Ctrl+Shift+M para dictado; Alt+Space para abrir panel/overlay). Estados: Idle → Recording → Paused → Processing → Ready. Controles: iniciar/pausar/finalizar, selector de idioma (ES/EN), salida (pegar, teclear, copiar, editor), VAD on/off.

Modelos recomendados (inicio):
- Rápido: Whisper tiny/base (CT2 int8) o large‑v3‑turbo distillado int8 [11].
- Equilibrado (default): Whisper base/small (CT2 int8) o ORT small all‑in‑one int8 [12][13].
- Preciso: Whisper medium o turbo distillado grande int8 (si memoria/latencia lo permiten).

Privacidad y datos:
- Local por defecto. Guardar solo transcripciones .txt/.md en carpeta configurable; no guardar audio (opcional habilitable). Purga rápida, logs mínimos.

Entregables por fases:
- 00 Planificación y estructura docs
- 01 Shell WinUI + hotkeys + tray + overlay básico
- 02 Audio in + VAD
- 03 Motor ASR (prioridad ORT all‑in‑one) + gestión modelos
- 04 Salida (tecleo/pegar/portapapeles) + mini‑editor
- 05 Preferencias/UX + comandos de dictado
- 06 Empaquetado MSIX + auto‑update
- 07 Pruebas/benchmarks + estabilización
- 08 Publicación OSS (README, licencia, issues, release)

Riesgos y mitigaciones:
- Latencia en CPU: usar VAD + chunking + int8; ofrecer perfiles de calidad.
- Integración CT2 en C#: iniciar con ORT all‑in‑one; CT2 como opción avanzada/worker.
- Captura/pegado en apps UAC: usar SendInput y fallback; respetar foco y evitar acciones invasivas.

KPIs principales:
- RTF ≤ 0.5–1.0 en perfiles rápido/equilibrado.
- Latencia inicio‑a‑texto ≤ 1.0–1.5 s en clips cortos.
- WER competitivo en ES/EN para dictado cotidiano.
