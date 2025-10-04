---
title: UX / UI – Overlay, mini editor y hotkeys
description: Flujos, estados, controles y accesibilidad
---

# Principios
- Velocidad, simplicidad y claridad visual. Overlay limpio, contrastado, con feedback inmediato.
- Operable 100% con teclado; accesible (navegación, ARIA, alto contraste).

# Atajos (configurables)
- Ctrl+Shift+M: abrir overlay de dictado.
- Alt+Space: abrir panel principal/command palette.
- Dentro del overlay: R (record/pause), Enter (finalizar), Esc (cancelar), F2 (cambiar idioma), F3 (cambiar salida), F4 (toggle VAD).

# Overlay
- Siempre‑encima (TopMost), borde redondeado, barra lateral con estado y selector ES/EN.
- Cabecera con waveform, indicador VAD, tiempo, modo.
- Zona central: 
  - Estado Idle: “Press R to start”
  - Recording: waveform activo, botón/pista de pausa (R) y finalizar (Enter)
  - Processing: spinner + previsualización parcial
  - Ready: texto final + acciones (Pegar, Teclear, Copiar, Editar)

# Mini editor flotante
- Caja de texto con opciones: limpiar, aplicar capitalización/puntuación, insertar timestamps por frase.
- Botones: Pegar/teclear/copy, cerrar.

# Panel de configuración
- Pestañas: General (idioma UI, hotkeys), Audio (dispositivo, VAD), Modelos (lista, descarga, ubicación), Salida (modos, formato), Privacidad (guardado .txt/.md, retención), Acerca de.
