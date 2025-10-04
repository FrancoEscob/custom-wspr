---
title: Fase 08 – Publicación OSS
role: Implementation Droid
audience: Software engineer (Factory agent)
deliverables:
  - README del repo (producto/instalación)
  - Licencia (MIT/Apache‑2.0)
  - Guía de contribución y issues templates
acceptance:
  - Repositorio público listo para usuarios y contribuciones
---
 
 # Overview
 - Goal: preparar el repositorio para publicación open source y contribuciones externas.
 - Inputs (usar como contexto): @docs/plan.md, @docs/tech-decisions.md, @docs/architecture.md, @docs/ux-spec.md, @docs/packaging-and-distribution.md
 - Outputs: README completo, LICENSE, CONTRIBUTING, CODE_OF_CONDUCT, SECURITY, PRIVACY, templates de issues/PRs.
 
 # Tasks (checklist)
 1) README
    - Descripción, features, requisitos, instalación MSIX/.appinstaller, uso básico, FAQ, capturas.
 2) Licencia
    - Elegir MIT o Apache-2.0; crear archivo LICENSE en raíz.
 3) Contribución y conducta
    - CONTRIBUTING.md con guía de entorno, estilo, pruebas, CI local.
    - CODE_OF_CONDUCT.md (Contributor Covenant).
 4) Seguridad y privacidad
    - SECURITY.md: cómo reportar vulnerabilidades, alcance.
    - PRIVACY.md: procesamiento local, logs opcionales.
 5) Plantillas
    - .github/ISSUE_TEMPLATE/bug_report.md y feature_request.md
    - .github/pull_request_template.md
 6) Changelog y versionado
    - Mantener CHANGELOG.md, adoptar SemVer y etiquetar releases.
 
 # Implementation notes
 - README debe linkear a `docs/` y a `docs/packaging-and-distribution.md` para instalación.
 - Incluir sección “Roadmap” y “Estado del proyecto (alpha/beta)”.
 - Agregar badges (licencia, releases) en GitHub si aplica.
 
 # Acceptance criteria
 - Repo contiene README, LICENSE, CONTRIBUTING, CODE_OF_CONDUCT, SECURITY, PRIVACY y templates bajo .github/.
 - README explica instalación mediante .appinstaller y enlaces a descargas.
 - Se crea un primer release draft con notas y artefactos.
 
 # Test plan
 - Verificar que los templates aparecen al crear issue/PR.
 - Probar enlaces y rutas del README en clean clone.
 
 # Risks & mitigations
 - Ambigüedad de licencia: preferir MIT o Apache-2.0 y documentar terceros.
 - Expectativas de soporte: incluir sección de alcance/limitaciones.
 
 # References
 - @docs/packaging-and-distribution.md
 - @docs/plan.md
 - @docs/ux-spec.md
 
 # Agent brief
 - Carga como contexto: @docs/plan.md, @docs/tech-decisions.md, @docs/ux-spec.md.
 - Entrega archivos en raíz y .github/ necesarios para OSS.
