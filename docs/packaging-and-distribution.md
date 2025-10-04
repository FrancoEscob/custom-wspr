---
title: Empaquetado y distribución (MSIX, App Installer, firma)
---

# ¿Qué es MSIX?
MSIX es el formato moderno de empaquetado para apps Windows. Permite instalaciones limpias, permisos declarativos y desinstalación sin residuos.

# Auto‑updates con App Installer
- Se genera un archivo `.appinstaller` que apunta a la URL donde publicas tus paquetes MSIX. Windows puede verificar y actualizar automáticamente la app sin pasar por Microsoft Store [25].
- Para auto‑update “al iniciar”, se recomienda usar el schema 2021 y `OnLaunch` con `ShowPrompt="true"` y `UpdateBlocksActivation="true"` [26].

# Firma de código (Code Signing)
- Es un certificado digital usado para firmar el MSIX. Aumenta la confianza de Windows/AV y evita advertencias. Para uso personal/local puedes omitirlo; para releases públicas es muy recomendable.

# Flujo sugerido de publicación
1) Compilar Release.
2) Generar paquete MSIX.
3) Generar `.appinstaller` (schema 2021) apuntando a una URL (GitHub Pages, CDN, o tu server).
4) (Opcional) Firmar el paquete con tu certificado.
5) Publicar el MSIX y appinstaller en la URL.
6) Usuarios instalan desde el .appinstaller y reciben updates.
