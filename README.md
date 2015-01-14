A Grimm Journey
===========

The shared repo for our LD31 project. Its contents are the entire project folder for the game.

Notes on designing levels
-----------
A few quick notes on how to put levels together in this game:

- Template: There is a template level that you can work off; this contains all the elements you need to build a new level. 

- Audio: You'll find an object attached to the main camera that controls which BGM track plays

- Spawns:
  - There are three spawn prefabs: WitchSpawn, TrollSpawn and WoodSpawn
  - WoodSpawns should be placed several times around the level
  - TrollSpawns should generally be placed higher up the level
  - WitchSpawns can be placed anywhere, as the witch ignores platform collisions
  - Timers and number of spawnable enemies can be adjusted in the editor
