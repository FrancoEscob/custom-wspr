---
title: Fase 03 – Motor ASR + gestión de modelos
role: Implementation Droid
audience: Software engineer (Factory agent)
deliverables:
  - Integración ORT Whisper all‑in‑one int8
  - Selector de idioma (ES/EN) y task transcribe
  - Gestor de modelos (lista/descarga/ruta)
acceptance:
  - Transcribir clip corto y mostrar texto en overlay/editor
---
 
 # Overview
 - Goal: integrar un pipeline ASR en CPU con ONNX Runtime Whisper all‑in‑one (int8) y crear un gestor de modelos básico (listar, descargar, seleccionar carpeta y modelo), exponiendo idioma (ES/EN) y task transcribe.
 - Inputs (usar como contexto): @docs/model-research.md, @docs/tech-decisions.md, @docs/architecture.md
 - Outputs: servicio IAsrEngine (ORT), gestor de modelos con metadatos, UI para elegir modelo e idioma; transcripción de clip corto.
 
 # Tasks (checklist)
 1) Añadir referencia a Microsoft.ML.OnnxRuntime (CPU) y cargar modelo all‑in‑one .onnx.
 2) Implementar IAsrEngine.Transcribe(Stream pcm16khz) con chunking 10–30 s y timestamps opcionales.
 3) Añadir parámetro Language (es/en/auto) y Task (transcribe; translate opcional off por defecto).
 4) Implementar ModelManager: lista curada (JSON), descarga desde URLs (HF), carpeta configurable, validación hash/tamaño.
 5) Conectar eventos de VAD para disparar transcripciones por segmento; actualizar overlay con texto parcial/final.
 
 # Implementation notes
 - Entrada: PCM16 mono 16 kHz. Normalizar amplitud si es necesario.
 - ONNX Runtime SessionOptions: intra_op_num_threads = Environment.ProcessorCount – 1; graph optimization level = all; arena allocator.
 - Mantener la sesión precargada y reutilizar para evitar latencia de warmup.
 
 # Acceptance criteria (detallados)
 - Seleccionar un modelo desde la UI y transcribir un clip de micrófono capturado en la Fase 02.
 - Mostrar texto en overlay/mini editor. Latencia inicio‑a‑texto < 1.5 s en perfil equilibrado.
 
 # Test plan
 - Usar 3 audios cortos ES/EN. Validar que la transcripción aparece y que el cambio de idioma afecta resultados.
 - Simular modelo corrupto: el gestor debe reportar error claro y no bloquear la app.
 
 # Risks & mitigations
 - Modelos pesados: progreso de carga y fallback a modelos pequeños.
 - Diferencias de exactitud ES/EN: documentar y permitir cambio rápido.
 
 # References
 - [12] ORT Whisper all‑in‑one (Olive)
 - [13] Modelos int8 listos
 - [14] ONNX Runtime docs
 
 # Agent brief
 - Usa como contexto: @docs/model-research.md, @docs/tech-decisions.md, @docs/architecture.md.
 - Mantén el alcance a ORT primero; CT2 puede quedar para fase futura.
 - Expón interfaces claras (IAsrEngine, IModelManager) y no acoples a la UI.
