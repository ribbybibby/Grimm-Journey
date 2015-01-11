A Grimm Journey
===========

The shared repo for our LD31 project. Its contents are the entire project folder for the game.

Notes on designing levels
-----------
A few quick notes on how to put levels together in this game:
- Template: There is a template level that you can work off; this contains all the elements you need to build a new level.

- Audio: You'll find an object attached to the main camera that controls which BGM track plays

- AI Movement: 
  - The AI aren't smart enough to move themselves around the environment perfectly
  - You can direct enemies around the level better by attaching LeftBound and RightBound triggers to the ends of platforms
  - These are objects attached to platforms, with a box collider set as a trigger and tagged as LeftBound/RightBound
  - These should be used on a case by case basis to avoid enemies getting stuck at certain tricky points

- Spawns:
  - The spawn points are empty transform objects that can be placed around the environment
  - The SpawnController object/script accepts these objects as items in an array and then spawns enemies at these locations based on the rules of the script
  - Make sure all your spawn points are added to the SpawnController array in the inspector
