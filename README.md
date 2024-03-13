# ExoplanetGame

## Text-Protokoll zwischen RemoteRobot-Clients, ExoPlanet-Server und ControlCenter-Server
R = RemoteRobot,
E = ExoPlanet,
C = ControlCenter

| Richtung | Kommando                | Beschreibung |
|----------|-------------------------|--------------|
| R <= E   | init:SIZE\|width\|height                   | Sendet die Größe des ExoPlaneten sofort nach dem Verbindungsaufbau an den Roboter |
| R => C   | init:SIZE\|width\|height                   | Übermittelt die Größe des ExoPlaneten an das Kontrollzentrum |
| R => E   | land:POSITION\|x\|y\|direction             | Anweisung an den Roboter, an der angegebenen Position und Richtung zu landen |
| R <= E   | landed:POSITION\|x\|y\|direction           | Bestätigung der Landung mit Position und Richtung |
| R => C   | UpdatePostion:POSITION\|x\|y\|direction    | Übermittelt die neue Position und Richtung an das Kontrollzentrum |
| R => E   | scan                                       | Anweisung an den Roboter, das Feld direkt unter dem Roboter zu messen |
| R <= E   | scaned:MEASURE\|ground                     | Bestätigung der Messung mit Scanergebnis |
| R => C   | scaned:MEASURE\|ground\|x\|y               | Übermittelt das Scanergebnis und die Position an das Kontrollzentrum |
| R => E   | move                                       | Anweisung an den Roboter ein Feld in die aktuelle Richtung bewegen |
| R <= E   | moved\|POSITION\|x\|y\|direction           | Bestätigung der Bewegung mit neuer Position und Richtung |
| R => C   | UpdatePostion:POSITION\|x\|y\|direction    | Übermittelt die neue Position und Richtung an das Kontrollzentrum |
| R => E   | rotate:rotation                            | Anweisung an den Roboter, sich nach links oder rechts zu drehen |
| R <= E   | rotated:direction                          | Bestätigung der Drehung mit neuer Richtung |
| R => C   | UpdatePostion:POSITION\|x\|y\|direction    | Übermittelt die neue Position und Richtung an das Kontrollzentrum |
| R <= E   | crashed                                    | Meldet dem Roboter seine Zerstörung durch eine Kollision oder ungültige Position|
| R => E   | exit                                       | Anweisung an den Roboter, die Verbindung zu beenden und den Betrieb einzustellen|

## Beispiele

- land:POSITION|1|2|NORTH
- scan
- move
- rotate:LEFT
- exit


## Build Order

Build order should be:
1. Exoplanet
2. ControlCenter
3. RemoteRobot(s)
