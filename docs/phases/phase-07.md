---
title: Fase 07 – Pruebas, benchmarks y estabilización
role: Implementation Droid
audience: Software engineer (Factory agent)
deliverables:
  - Suite de pruebas funcionales
  - Benchmarks RTF/latencia/mem
acceptance:
  - KPIs objetivo alcanzados en perfiles rápido/equilibrado
---
 
 # Overview
 - Goal: definir y ejecutar pruebas funcionales y benchmarks de performance para los perfiles rápido, equilibrado y preciso en el hardware objetivo (Ryzen 5 5600G, 32 GB RAM).
 - Inputs (usar como contexto): @docs/testing-and-benchmarks.md, @docs/model-research.md, @docs/plan.md, @docs/tech-decisions.md
 - Outputs: scripts reproducibles, dataset corto ES/EN, tabla de resultados (RTF/latencia/memoria), reporte/README de benchmarks, issues creados por desvíos.
 
 # Tasks (checklist)
 1) Dataset corto ES/EN
    - 10–15 frases por idioma (incluyendo puntuación, números, fechas) en `bench/data/` con licencias claras.
    - Generar transcripciones de referencia para inspección manual (no WER formal en esta fase).
 2) Harness de medición
    - Crear un runner (CLI o modo oculto en la app) que permita:
      a) Cargar un modelo y perfil (rápido/equilibrado/preciso)
      b) Ejecutar N repeticiones por audio
      c) Medir: RTF, latencia inicio-a-primer-texto y a-texto-final (P50/P95), uso de memoria pico
    - Exportar CSV/JSON a `bench/results/` con timestamps y metadatos (modelo, hilos, commit SHA, versión).
 3) Definir perfiles
    - Rápido: modelo pequeño, threads = max(1, cores-2)
    - Equilibrado: modelo mediano, threads = cores-1
    - Preciso: modelo grande (si cabe), threads = cores-1, con chunks más largos
    - Basarse en @docs/model-research.md para tamaños concretos.
 4) Correr benchmarks
    - Ejecutar cada perfil 3 veces; registrar P50/P95 por métrica.
    - Incluir warmup previo (1 corrida) fuera de medición.
 5) Pruebas funcionales end-to-end
    - Validar flujos de fases 02–05: captura+VAD → ASR → mini editor → salida.
    - Abrir issues por regresiones o desvíos vs objetivos.
 6) Reporte
    - Crear `docs/benchmarks/README.md` con tablas y conclusiones.
 
 # Implementation notes
 - Medición
   - RTF = processing_time / audio_duration.
   - Latencia: medir desde evento VAD start hasta primer texto visible (UI) y hasta texto final.
   - Memoria: WorkingSet/PrivateBytes máx. durante la corrida; excluir descarga de modelos.
 - Entorno controlado
   - Cerrar apps pesadas; plan de energía Alto rendimiento; minimizar ruido del sistema.
   - Ejecutar en Release, sin logs de debug.
 - Reproducibilidad
   - Registrar commit SHA, versión, modelo y opciones usadas en cada archivo de resultados.
 
 # Test plan
 - Validar que el harness corre con 2 modelos recomendados y 3 perfiles.
 - Verificar variación <15% entre repeticiones.
 - Smoke test de memoria: el pico no crece entre repeticiones (sin fugas).
 - Stress: audio continuo de 5 minutos con segmentación estable.
 
 # Acceptance criteria (medibles)
 - Existe `bench/data/` con audios ES/EN y `bench/results/` con CSV/JSON por perfil.
 - Reporte en `docs/benchmarks/README.md` resume RTF, latencia P50/P95 y memoria, comparando contra objetivos de @docs/testing-and-benchmarks.md.
 - Desvíos significativos tienen issues creados con hipótesis y próximos pasos.
 
 # Risks & mitigations
 - Variabilidad ambiental: 3 repeticiones y promedios; documentar entorno y versión de Windows.
 - Sesgo por warmup: realizar calentamiento antes de medir.
 - Medición influida por UI: contrastar con modo headless del runner para aislar.
 - Modelos grandes no caben: fallback a modelos del perfil inferior y documentar.
 
 # References
 - @docs/testing-and-benchmarks.md
 - @docs/model-research.md
 - @docs/tech-decisions.md
 
 # Agent brief
 - Carga como contexto: @docs/testing-and-benchmarks.md, @docs/model-research.md, @docs/plan.md.
 - Entrega scripts reproducibles y resultados en `bench/` y un resumen en `docs/benchmarks/README.md`.
 - No optimices prematuramente código de producción; enfócate en medición y reportabilidad.
