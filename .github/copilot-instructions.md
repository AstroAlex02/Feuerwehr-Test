# Copilot-Anweisungen für dieses Repository

Kurz und prägnant: diese Datei enthält projektspezifische Hinweise, damit ein KI-Coding-Agent sofort produktiv arbeiten kann.

Technologie & Architektur
- Razor Pages Webanwendung (.NET 10). Quellseiten: Pages/*.cshtml.
- Statische Assets liegen in wwwroot/ (css, images, js). Das zentrale CSS ist wwwroot/css/ffw.css (Dev) und wwwroot/css/ffw.min.css (Prod, erzeugt von npm run css).

Wichtige Entwickler-Workflows
- Voraussetzungen: .NET 10 SDK, Node.js + npm.
- Lokales Entwickeln:
  1. npm install
  2. npm run css   # erstellt/aktualisiert wwwroot/css/ffw.min.css aus Quellen
  3. dotnet run    # startet die Razor Pages App
- CI: .github/workflows/dotnet.yml führt dotnet publish aus und steuert ein Windows/IIS-Deploy (Start/Stop-Skripte). Beachte: Runner name ist "ffw" und Schritte nutzen PowerShell WebAdministration.

Konventionen und wichtige Patterns
- CSS: Development arbeitet direkt mit wwwroot/css/ffw.css. Die minifizierte Datei wird bei Build/Deployment von npm-Skripten überschrieben. Änderungen an ffw.css sind der richtige Weg für schnelle Iteration.
- Hero-Bild (häufige Fehlerquelle): Es gibt eine globale img-Regel (width: auto), die das Verhalten von .hero-img überschreiben kann. Aktuelles, getestetes Pattern:

  - Datei: wwwroot/css/ffw.css
  - Wichtige Klassen: .hero (Wrapper), .hero-img (das <img> inside hero), .hero-content (Overlay / Card)

  Empfohlenes CSS-Snippet (bereits angewendet):

  .hero { /* Full-bleed wrapper, keine aspect-ratio oder background-size:cover */
	display: block; width:100vw; margin-left:-50vw; margin-right:-50vw; background:#1f2937; overflow:hidden; }

  .hero > .hero-img, img.hero-img { width:100% !important; height:auto !important; display:block; object-fit:unset; max-width:100%; }

  .hero-content { position:static; display:flex; justify-content:center; padding:1rem; }

  Ziel: Das <img> füllt immer die volle Breite, bleibt vollständig sichtbar (kein Zuschneiden) und skaliert höhenproportional.

Beispiele (HTML)
- Pages/Index.cshtml oder andere Razor-Seiten sollten folgenden Aufbau verwenden, wenn ein hero-Element benötigt wird:

  <div class="hero">
	<img class="hero-img" src="/images/hero.jpg" alt="Hero">
	<div class="hero-content">... Inhalt ...</div>
  </div>

Weitere Hinweise
- Tailwind/Flowbite: Tailwind wird via npm/CLI generiert; vermeide doppelte Styles in ffw.css vs. Tailwind-Konfiguration.
- Assets: Bilder und SVGs liegen in wwwroot/images. SVG-Logos (z. B. wwwroot/images/feuerwehr-waldenbuch-logo.svg) werden inline oder als <img> verwendet.
- Tests: Das Repository enthält derzeit keine automatisierten Unit-Tests. Anpassungen am Frontend sollten lokal in verschiedenen Viewport-Größen geprüft werden.

Wenn etwas unklar ist oder du Prioritäten (z. B. responsive Breakpoints, Bildoptimierung, srcset) ändern möchtest, antworte mit konkreten Vorgaben.
