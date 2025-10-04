---
title: Arquitectura y módulos
description: Componentes, flujos, estados y contratos
---

# Componentes
- App Shell (WinUI 3): ventana de configuración, bandeja del sistema, overlay.
- Hotkeys Service (Win32): registro global de atajos (Ctrl+Shift+M, Alt+Space, etc.).
- Audio Service: captura WASAPI (NAudio), resampling a 16 kHz mono.
- VAD Service: Silero VAD ONNX (ONNX Runtime CPU) con umbrales configurables.
- Segmentation: buffer circular + cortes por VAD y/o ventanas fijas (10–30 s).
- ASR Engine Abstraction:
  - Provider ORT All‑in‑one (Whisper int8)
  - Provider CT2 Faster‑Whisper (worker externo opcional)
  - Provider sherpa‑onnx (futuro)
- Post‑process: normalización, puntuación, comandos de dictado, capitalización.
- Output Service: 
  - Keyboard typing (SendInput)
  - Paste (Ctrl+V)
  - Clipboard set
  - Mini Editor output
- Model Manager: catálogo, descarga HF, validación hashes, rutas.
- Settings Store: JSON en AppData/local.
- Logging (local) y almacenamiento de transcripciones .txt/.md.

# Flujos
1) Invocación overlay (hotkey) → estado Idle.
2) Start Recording: inicia captura WASAPI, VAD activa segmentación.
3) Envío de chunks al motor ASR (streaming o por bloque) → agregación.
4) Post‑proceso + salida (según modo) y/o editor.
5) Finalizar: persistencia .txt/.md si habilitado; notificación.

# Estados UI Overlay
- Idle, Recording, Paused, Processing, Ready, Error.

# Configuraciones clave
- Idioma (ES/EN) + autodetección opcional.
- Motor ASR y modelo seleccionado.
- VAD (umbral, min_speech, min_silence, padding).
- Modo de salida por defecto y alternativos.
- Guardado de transcripciones y carpeta.

# Contratos de provider ASR
- Transcribe(audioChunk, lang) → {text, partial?, timestamps?}
- Capabilities: multilingual, timestamps, quantization, memory footprint.
