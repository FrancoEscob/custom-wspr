---
title: Modelos locales ASR (CPU) 2024-2025
description: Investigación y recomendaciones para Ryzen 5 5600G (32 GB)
---

# Resumen ejecutivo
- Para dictado rápido bilingüe (ES/EN) en CPU: preferir Whisper base/small int8 con CTranslate2 (faster-whisper) [10] o Whisper "all-in-one" ONNX Runtime (Olive) int8 [12][13]. Perfiles sugeridos: “Rápido” (tiny/base distillado), “Equilibrado” (base/small), “Preciso” (medium o large‑turbo distilado) según latencia y precisión.
- VAD Silero int8 reduce latencia y falsos inicios [7][9].
- Catálogo inicial descargable y ubicación de modelos configurable.

# Opciones principales (CPU)

## Faster-Whisper (CTranslate2)
- CTranslate2 soporta int8 en CPU con muy buen throughput y baja latencia [10]. Existen checkpoints distilados como large‑v3‑turbo int8 que aumentan velocidad con caída pequeña de calidad [11].
- Pros: rendimiento notorio en CPU, soporte multilingüe de Whisper, variedad de tamaños (tiny/base/small/medium/large‑turbo).
- Contras: desde C# requiere integración vía proceso/servicio o binding nativo.

## ONNX Runtime – Whisper "all-in-one"
- Uso de Microsoft Olive para generar un único modelo ORT que integra encoder/decoder/beam y pre/post‑proceso; soporta cuantización int8 y ofrece implementación directa en C# [12]. Hay ejemplos y modelos int8 listos [13].
- Pros: integración simple en .NET, despliegue portable; buen rendimiento CPU [12][14].
- Contras: en algunos escenarios CT2 int8 sigue siendo algo más veloz [16].

## whisper.cpp (GGML/GGUF)
- Port C/C++ muy portable con cuantización 4/5‑bit [15]. En CPU puros, comparativas independientes muestran que CT2 int8 suele rendir mejor para tamaños comparables [16].

## sherpa-onnx (Zipformer/Paraformer/NeMo‑CTC)
- Librería ONNX con modelos optimizados e int8 listos para múltiples idiomas [17][18][19]. Útil como alternativa/futuro para escenarios específicos.

# Recomendaciones por perfil (Ryzen 5 5600G, 32 GB)
- Rápido (latencia baja, calidad suficiente):
  - Whisper tiny/base (CT2 int8) o large‑v3‑turbo distillado int8 [10][11].
  - Uso: notas rápidas, chat, comandos de voz.
- Equilibrado (default):
  - Whisper base/small (CT2 int8) o ORT all‑in‑one small int8 [12][13].
  - Uso: dictado continuo para emails/docs.
- Preciso (calidad prioritaria):
  - Whisper medium o large‑turbo int8 (si memoria/latencia lo permiten, con VAD y chunking 10–30 s).
  - Uso: transcripciones más largas y cuidadas.

# Idiomas
- ES/EN soportados out‑of‑the‑box por Whisper. Para v0.1 seleccionar manualmente; autodetección opcional habilitable. Para traducción, exponer "task" translate (opcional).

# Segmentación y timestamps
- VAD Silero [7][9] + segmentación corta (10–30 s) reduce latencia percibida y mejora estabilidad.
- Timestamps opcionales en el mini editor o exportación JSON.

# Gestión de modelos
- Lista curada (Hugging Face) con metadatos (tamaño, memoria, precisión esperada). Permitir:
  - Descarga/actualización desde la app.
  - Carpeta de modelos configurable.
  - Limpieza/borrado seguro.

# Benchmarks propuestos
- Métricas: RTF, latencia inicio‑a‑texto (P95), WER aproximado ES/EN, CPU/RAM.
- Comparar CT2 vs ORT en tiny/base/small con los mismos audios; medir impacto de VAD/segmentación.

# Referencias
- [7] https://github.com/snakers4/silero-vad
- [9] https://github.com/snakers4/silero-vad/wiki/Performance-Metrics
- [10] https://github.com/SYSTRAN/faster-whisper
- [11] https://huggingface.co/cstr/whisper-large-v3-turbo-int8_float32
- [12] https://medium.com/microsoftazure/build-and-deploy-fast-and-portable-speech-recognition-applications-with-onnx-runtime-and-whisper-5bf0969dd56b
- [13] https://huggingface.co/khmyznikov/whisper-int8-cpu-ort.onnx
- [14] https://onnxruntime.ai/docs/reference/compatibility.html
- [15] https://github.com/ggml-org/whisper.cpp
- [16] https://gist.github.com/geekodour/8734b3bf22b8ede61fb5bfc92ce68fe3
- [17] https://k2-fsa.github.io/sherpa/onnx/pretrained_models/offline-transducer/zipformer-transducer-models.html
- [18] https://huggingface.co/csukuangfj/sherpa-onnx-paraformer-zh-2024-03-09
- [19] https://k2-fsa.github.io/sherpa/onnx/pretrained_models/offline-ctc/nemo/index.html