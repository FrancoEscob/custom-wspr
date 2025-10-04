---
title: Plan de pruebas y benchmarks
---

# Pruebas funcionales
- Hotkeys globales (registro, conflictos, comportamiento con apps elevadas).
- Overlay estados: Idle/Recording/Paused/Processing/Ready.
- Audio: selección de dispositivo, niveles, VAD on/off.
- Salida: tecleo, pegado, portapapeles, editor.
- Guardado de .txt/.md (privacidad configurada).

# Pruebas rendimiento
- Latencia inicio‑a‑texto (P95), RTF por perfil de modelo.
- Consumo CPU/RAM sostenido en sesiones largas.
- Efecto de VAD (falsos positivos/negativos) y parámetros.

# Benchmarks sugeridos
- Clips cortos (5–30 s) en ES/EN con ruido leve/medio.
- Evaluación WER aproximada (muestras conocidas) y velocidad (CT2 vs ORT tiny/base/small).
