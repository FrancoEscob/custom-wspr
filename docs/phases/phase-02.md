---
title: Fase 02 – Audio in + VAD
role: Implementation Droid
audience: Software engineer (Factory agent)
deliverables:
  - Captura WASAPI (micrófono) 16 kHz mono
  - VAD Silero ONNX y segmentación
acceptance:
  - Graficar waveform y marcar tramos de voz; exportar WAV temporal opcional
---
 
 # Overview
 - Goal: capturar audio del micrófono con WASAPI (NAudio) a 16 kHz mono y aplicar VAD Silero (ONNX int8) para segmentación en tiempo real, alimentando el overlay con waveform y marcadores de voz.
 - Inputs (usar como contexto): @docs/tech-decisions.md, @docs/ux-spec.md, @docs/architecture.md
 - Outputs: servicio de audio, servicio de VAD, visualización de waveform y marcadores, exportación WAV temporal (opcional) por sesión.
 
 # Tasks (checklist)
 1) Implementar capturador WASAPI (shared) con resample a 16 kHz mono PCM16.
 2) Añadir ring buffer de audio de ~30 s y cola de segmentos.
 3) Integrar Silero VAD ONNX (int8) con ventana 20–30 ms; umbrales configurables.
 4) Emitir eventos OnSpeechStart/OnSpeechEnd para UI; colorear tramos en overlay.
 5) Opción de exportar WAV temporal de cada segmento para pruebas.
 
 # Implementation notes
 - NAudio: WasapiCapture/WasapiLoopbackCapture (más adelante), MediaFoundationResampler.
 - Silero: onnxruntime CPU EP; batch 512–1024 frames; normalizar -26 dBFS.
 - Threading: procesar VAD en hilo dedicado; no bloquear UI.
 
 # Acceptance criteria (detallados)
 - Overlay muestra waveform en vivo y destaca secciones de voz.
 - Al hablar y parar, se registran eventos consistentes (<200 ms jitter).
 - Exportación WAV opcional genera archivos por segmento.
 
 # Test plan
 - Probar silencio, habla, ruido de teclado. Ajustar umbrales para falsos positivos bajos.
 - Medir latencia evento start/end con logs timestamps.
 
 # Risks & mitigations
 - Dispositivos con 48 kHz: validar resample estable.
 - Ruido ambiente: permitir sensibilidad configurable.
 
 # References
 - [3] NAudio WASAPI
 - [7] Silero VAD
 
 # Agent brief
 - Usa como contexto: @docs/tech-decisions.md, @docs/ux-spec.md, @docs/architecture.md.
 - Mantén el alcance a captura + VAD + UI básica. No integrar ASR aún.
 - Entrega servicios desacoplados (IAudioCapture, IVadService) con eventos.
 - Documenta parámetros (umbral, min_speech_frames) y valores por defecto.
