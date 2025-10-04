---
title: Decisiones tecnológicas y alternativas
description: Stack propuesto para Windows 11, con justificación y comparativas
---

# Decisiones tecnológicas (Stack propuesto)

Objetivo: app de Windows 11, 100% local, con atajos globales, overlay/mini‑editor, y motores ASR locales optimizados para CPU (Ryzen 5 5600G, 32 GB RAM). Interfaz en inglés por defecto, con opción español.

## UI y plataforma de app
- Opción recomendada: WinUI 3 + Windows App SDK (C# .NET 8)
  - Ventajas: UI moderna nativa, integración Win32 (P/Invoke) para hotkeys globales y tray, ecosistema .NET maduro, buen soporte MSIX.
  - Bandeja del sistema (NotifyIcon): vía Win32 `Shell_NotifyIcon` con CsWin32 [1].
  - Hotkeys globales: `RegisterHotKey` (Win32) [2].
  - Overlay flotante siempre‑encima (TopMost) con XAML + efectos.
- Alternativas:
  - WPF: maduro, pero WinUI 3 es la vía moderna.
  - Electron/Tauri: cross‑platform, pero más pesado; objetivo es nativo‑Windows y latencia mínima.

## Captura de audio
- NAudio (C#) para micrófono vía WASAPI en 16 kHz mono PCM, y VAD previo.
- Futuro: system audio loopback (WasapiLoopbackCapture) [3][4][5]; incluso por‑proceso en Windows 10 20348+ [6].

## VAD (Voice Activity Detection)
- Recomendado: Silero VAD ONNX en CPU (int8) por precisión/latencia y footprint ~1–2 MB [7][8][9].
- Alternativas: WebRTC VAD (muy ligero, menos robusto), Pyannote (más pesado), Port de Silero a ONNX Runtime C#.

## Motores ASR locales (CPU)
- Recomendado ofrecer 2 “rutas” seleccionables:
  1) Faster‑Whisper (CTranslate2) con modelos CT2 int8 (tiny/base/small; y turbo‑distilled para velocidad) [10][11]. Muy rápido en CPU y con quantización eficiente; APIs disponibles en Python, pero para app C# preferimos ejecución mediante proceso worker o binding nativo.
  2) ONNX Runtime “all‑in‑one Whisper” (Olive) int8 para CPU, con beam search integrado en un solo modelo [12][13][14]. Sencillo de integrar desde C#, portable, buen rendimiento.
- Alternativas/Complementos:
  - whisper.cpp (GGML/GGUF): C/C++ puro, excelente portabilidad; en CPU algunos benchmarks muestran menor velocidad que CT2 int8 [15][16].
  - sherpa‑onnx: modelos Zipformer/Paraformer/Nemo‑CTC int8 listos [17][18][19] con pipelines ONNX muy prácticos; útil para soportar idiomas/escenarios específicos.
  - Intel INC INT4/INT8 para Whisper tiny/otros [20][21].

## Post‑proceso y pegado de texto
- Opciones configurables: 
  - “Type‑as‑keyboard” (SendInput/SendKeys) [22][23][24]
  - Pegar (Ctrl+V)
  - Copiar al portapapeles + notificación
  - Mini editor flotante para revisar/editar (y aplicar comandos de dictado)
- Comandos de dictado: puntuación, nueva línea, borrar última frase, capitalización.

## Empaquetado y distribución
- MSIX + App Installer para sideload y auto‑update desde URL propia [25].
  - .appinstaller (schema 2021) con `OnLaunch ShowPrompt/UpdateBlocksActivation` [26].
- Firma de código: recomendable al publicar releases (mejora confianza; opcional para uso personal inicial).

## Telemetría/privacidad
- 100% local por defecto. Sin envíos a nube.
- Opcional: guardar transcripciones .txt/.md (sin audio) en carpeta configurable; retención ajustable.

## Justificación del stack
- Nativo Windows (.NET + WinUI 3) simplifica hotkeys/overlay/tray y MSIX.
- ONNX Runtime y/o CT2 aseguran rendimiento alto en CPU con int8; futuro GPU o DirectML si se desea.
- VAD Silero reduce latencias y falsos positivos.

# Alternativas evaluadas
- Electron/Tauri: mayor huella y latencias de IPC.
- QT/C++: robusto pero mayor coste de UI en Windows para dev .NET.
- Solo whisper.cpp: integración sencilla en C++, pero menos flexible que ORT/CT2 para experimentar con múltiples modelos modernos.

# Requisitos de sistema (objetivo)
- Windows 11 x64, CPU con AVX2 (Ryzen 5 5600G ok), 8–32 GB RAM. Sin GPU requerido.

# Referencias
[1] NotifyIcon WinUI 3 con Shell_NotifyIcon [Using NotifyIcon in WinUI 3]
[2] RegisterHotKey (Win32) [MS Docs]
[3] NAudio WASAPI loopback issue/notes
[4] WASAPI loopback (Win32) [MS Docs]
[5] CSCore loopback ejemplo
[6] Application loopback por proceso (ActivateAudioInterfaceAsync sample)
[7] Silero VAD overview y rendimiento
[8] Silero VAD ONNX integrations
[9] Silero VAD performance wiki
[10] faster‑whisper CT2, modelos int8
[11] whisper‑large‑v3‑turbo CT2 int8
[12] Microsoft Olive + ORT Whisper all‑in‑one
[13] khmyznikov/whisper‑int8‑cpu‑ort.onnx
[14] ORT compatibility
[15] whisper.cpp repo
[16] Comparaciones whisper.cpp vs faster‑whisper
[17] sherpa‑onnx Zipformer
[18] sherpa‑onnx Paraformer
[19] sherpa‑onnx NeMo CTC
[20] Intel Neural Compressor Whisper INT4/INT8
[21] Intel Whisper tiny int8
[22] SendInput en .NET
[23] InputSimulator
[24] SendKeys .NET
[25] MSIX App Installer auto‑update
[26] AppInstaller schema 2021 y atributos
