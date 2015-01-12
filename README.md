A Grimm Journey
===========

The shared repo for our LD31 project. Its contents are the entire project folder for the game.

Notes on designing levels
-----------
A few quick notes on how to put levels together in this game:
- Template: There is a template level that you can work off; this contains all the elements you need to build a new level. !!! NOT UPDATED FOR LATEST LEVEL CHANGES YET !!!

- Audio: You'll find an object attached to the main camera that controls which BGM track plays

- Spawns:
  - The spawn points are empty transform objects that can be placed around the environment
  - The SpawnController object/script accepts these objects as items in an array and then spawns enemies at these locations based on the rules of the script
  - Make sure all your spawn points are added to the SpawnController array in the inspector
