# ExoplanetGame

## Text-Protokoll zwischen RemoteRobot-Clients und ExoPlanet-Server
C = Client,
S = Server

| Richtung | Kommando                | Beschreibung |
|----------|-------------------------|--------------|
| C <= S   | init:SIZE\|width\|height | Sendet Server direkt nach Verbindungsaufbau mit der Größe des ExoPlaneten |
| C => S   | land:POSITION\|x\|y\|direction | Landen des Robot an der angegebenen Position/Richtung |
| C <= S   | landed:MEASURE\|ground  | Antwort bei erfolgreicher Landung mit dem ersten Messwert der Landeposition |
| C => S   | scan                    | Aufforderung Messung des Feldes, das direkt vor dem Roboter liegt |
| C <= S   | scaned:MEASURE\|ground  | Antwort mit dem Scanergebnis |
| C => S   | move                    | Robot ein Feld in die aktuelle Richtung bewegen |
| C <= S   | moved\|POSITION\|x\|y\|direction  | Antwort mit der neuen Position/Richtung |
| C => S   | rotate:rotation         | Nach Links/Rechts drehen |
| C <= S   | rotated:rotation        | Antwort mit neuer Richtung |
| C <= S   | crashed                 | Robot ist zerstört (z.B. nach move auf ungültige Position oder Kollision mit anderem Robot, ...)|
| C => S   | exit                    |Robot wird aufgegeben|

## Beispiele

- land:POSITION|1|2|NORTH
- scan
- move
- rotate:LEFT
- exit
