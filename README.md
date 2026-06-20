Feuerwehr Waldenbuch — Website

Projektübersicht

Diese Razor Pages Webanwendung stellt die offizielle Website der Feuerwehr Waldenbuch dar. Sie bietet Informationen zu Einsätzen, aktuellen Nachrichten, Terminen, Kontakten und rechtlichen Hinweisen (Impressum, Datenschutz). Die Seite dient sowohl der Öffentlichkeitsarbeit als auch als Informationsplattform für Bürger und Mitglieder der Feuerwehr.

Wichtigste Funktionen

- Anzeige von Neuigkeiten und Einsatzberichten
- Veranstaltungskalender und Termine
- Kontakt- und Informationsseiten (Impressum, Datenschutz)
- Wiederverwendbare Komponenten (Navigation, Layout)

Technologie-Stack

- .NET 10 (Razor Pages)
- Tailwind CSS + Flowbite für UI
- Node.js / NPM für CSS-Build (Tailwind CLI)

Lokales Entwickeln

1. Voraussetzungen: .NET 10 SDK, Node.js
2. Abhängigkeiten installieren: `npm install`
3. CSS erzeugen / minimieren: `npm run css` (baut `wwwroot/css/ffw.min.css`)
4. Anwendung starten: `dotnet run` oder über die IDE

Deployment

Die Anwendung kann als Standard-ASP.NET-App gehostet oder in Container bereitgestellt werden. Vor dem Deployment sicherstellen, dass die gebauten Ressourcen in `wwwroot` (z. B. `wwwroot/css/ffw.min.css`, `wwwroot/js/flowbite.min.js`) vorhanden sind.

Repository

https://github.com/iqmeta/de.feuerwehr-waldenbuch

Hinweis

Bei Bedarf können weitere Abschnitte (z. B. CI/CD, Tests oder Beitragsrichtlinien) ergänzt werden.
