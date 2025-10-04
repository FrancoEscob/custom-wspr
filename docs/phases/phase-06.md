---
title: Fase 06 – Empaquetado MSIX + auto‑update
role: Implementation Droid
audience: Software engineer (Factory agent)
deliverables:
  - Paquete MSIX, .appinstaller schema 2021
  - (Opcional) firma de código
acceptance:
  - Instalación por .appinstaller y actualización desde URL
---
 
 # Overview
 - Goal: empaquetar en MSIX y habilitar auto-update por .appinstaller publicado en URL.
 - Inputs (usar como contexto): @docs/packaging-and-distribution.md, @docs/plan.md
 - Outputs: archivo .msix, .appinstaller, script de build.
 
 # Tasks (checklist)
 1) Configurar proyecto empaquetado (Windows App SDK) y generar MSIX.
 2) Crear .appinstaller con UpdateSettings OnLaunch ShowPrompt=true.
 3) Publicar artefactos en una URL (GitHub Pages o almacenamiento) para pruebas.
 4) Validar upgrade de N a N+1.
 
 # Implementation notes
 - Versionado SemVer; incrementar Package Version.
 - Firmado: cert dev para pruebas; firma opcional de producción documentada.
 
 # Acceptance criteria (detallados)
 - Instalación desde .appinstaller funciona y actualiza a versión nueva sin reinstalar manualmente.
 
 # Test plan
 - Instalar v0.1.0, publicar v0.1.1 y verificar diálogo de actualización al lanzar.
 
 # Risks & mitigations
 - Requisitos de firma en entornos corporativos: documentar alternativas.
 
 # Agent brief
 - Usa como contexto: @docs/packaging-and-distribution.md, @docs/plan.md.
