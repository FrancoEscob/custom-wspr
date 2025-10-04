---
title: Runbook de Fases para Factory Droids
description: Cómo ejecutar cada fase en sesiones nuevas usando @docs/phases/*.md
---

# Objetivo
Establecer un formato y un flujo de trabajo para que cualquier Implementation Droid pueda ejecutar una fase en una sesión nueva, refiriéndose únicamente al archivo de la fase (p. ej., @docs/phases/phase-03.md).

# Formato estándar de cada fase
Cada archivo de fase debe incluir, en este orden:
- Front-matter con: title, role: Implementation Droid, audience: Software engineer
- Overview: Goal, Non-goals, Dependencies, Inputs, Outputs
- Tasks (checklist) con pasos accionables
- Implementation notes (APIs, librerías, rutas, decisiones)
- Acceptance criteria (resultados verificables)
- Test plan (casos y cómo validar)
- Risks & mitigations
- Out of scope
- References (enlaces a docs y fuentes)
- Agent brief (cómo trabajar, límites de alcance)

# Cómo invocar una fase en una nueva sesión
Ejemplo de prompt inicial (copiar y pegar):

1) Cargar contexto mínimo:
"Lee y sigue exactamente @docs/phases/phase-03.md. Si algo es ambiguo, pregúntame antes de implementar. No te salgas del alcance de esta fase."

2) Aportar detalles de entorno si aplica:
".NET 8 instalado, Windows 11. Directorio de trabajo: C:\\Users\\frand\\Desktop\\DEV\\custom-wspr"

3) Pedir plan de acción y comenzar:
"Muestra tu checklist, márcalo in_progress y ejecuta los pasos. Documenta cualquier decisión."

# Buenas prácticas
- Mantener un solo item in_progress a la vez y marcar completed al finalizar cada sub-tarea.
- Preguntar antes de tomar decisiones que afecten otras fases o el alcance.
- Mantener los cambios auto-contenidos; no introducir dependencias no aprobadas.

# Notas
- Si el agente necesita investigar APIs específicas, debe agregar una breve sección de fuentes en el PR o comentario y enlazarla en la sección References de la fase.