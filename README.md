# ExoplanetGame


## Table of Contents
1.  Overview
2.  Game Mechanics
3.  Features and Functions
4.  Controls


## Overview
ExoplanetGame is a console-based game developed in C# that focuses on the exploration and discovery of exoplanets. Players control a remote-controlled robot on an undiscovered planet, sending commands from the control center to explore, measure, and navigate the terrain. The game is played entirely through the console, where players issue commands to the robot and receive feedback on its actions.



## Installation
TODO


## Communication
The game involves communication between three main components:

1.  **RemoteRobot**: Represents a player character, a robotic explorer sent to explore exoplanets. The RomoteRobot communicates between the control center and the exoplanet.
2.  **ExoPlanet**: Represents the different exoplanets the player is exploring. Communicates with the RemoteRobot as it explores the planet.
3.  **ControlCenter**: Serves as the central point for receiving and transmitting instructions and data to the RemoteRobot.


## Game Mechanics

### RemoteRobot
The RemoteRobot serves as the player's character within the game , acting as a robotic explorer tasked with navigating and exploring exoplanets. These are the main features and functions of the robot:

1. **Landing Control**: The RemoteRobot receives instructions from the ControlCenter to land at specific positions on the ExoPlanet.

2. **Field Measurement**: It is able to measure the characteristics of the field directly below it, providing valuable data to the ControlCenter.

3. **Movement**: The RemoteRobot can move to the field in front of it as instructed by the ControlCenter.

4. **Turning**: It can turn left or right based on commands from the ControlCenter.

5. **Load**: Charges the robot's energy using its solar panels. The intensity of the charge depends on the weather.

6. **Confirm Actions**: After executing commands, the RemoteRobot confirms its actions by reporting the results back to the ControlCenter.


### Exoplanet
The Exolanets represents the diverse and uncharted planets that players explore in the Game. Here's how it interacts with the game:

1. **Difficulty selection**: The types of planets represent levels of difficulty. One of them can be chosen as a destination at the beginning of the game.

2. **Terrain Analysis**: Exoplanets have different planetary terrains. These can have different properties such as temperature or ground conditions. Robots can be affected, hindered, or destroyed by them.

3. **Planet Events**: The characteristics and challenges of the planet will shape the game experience and provide a variety of challenges for players. Difficult planets are characterized by events such as volcanic eruptions, snowstorms, or mysterious attacks.

### ControlCenter
The ControlCenter serves as the central hub for managing communication and coordination between the RemoteRobot and ExoPlanet. Its functions include:

1. **Robot Variants**: The control center can send out and control different types of robots. This includes robots with special resistances and enhanced capabilities.

2. **Repair Robots**: It can repair parts of a robot that are worn or damaged.

3. **Exoplanet Analysis**: The control center uses the data previously collected by the RemoteRobot to display the properties of the planet and the area discovered.

## Controls

-   Use the numeric input to navigate through the menu.
-   Press F1 to display more detailed descriptions of menu items.
