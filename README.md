# CharacterOnPlanets

The objective of this public repository is to demostrate my ability to implement complex features inside of Unity.
This is a project I started back in highschool and was originally meant to turn into a 3D game where we can control a character under realistic gravity, on a 3D celestial system.
I had a lot of fun implementing a ton of features for this game which required a great deal of problem solving, but unfortunately had to abandon the project due to a lack of interest in actually designing the game itself; which resulted in a lack of direction for the project and massive feature creep. This is a subset of the original scripts that includes working features from the project.

The following are some cool features that was implemented:
* Realistic 3D Gravity Simulation:
  * The Gravity.cs and SetInitVelocity.cs scripts can simulate the N-body problem, providing an accurate 3D gravity simulation.
* Real Gravity Character Controls:
  * The system allows characters to stand upright on any planet while maintaining the correct up direction relative to the planet's surface, regardless of the planets absolute position, velocity, or rotation.
  * Additionally, the system allows characters to jump off one planet and fly to another.
* Third-Person Camera System:
  * I have designed a camera system from scratch that maintains the character's correct orientation at all times.
  * The camera's distance, height, and angle of view can be customized relative to the character
  * The camera's smoothness of movement and rotation can be adjusted.
  * Furthermore, the target character can be changed dynamically during runtime.
  

Some other features that were being worked on but decided not to included in this repository partly because of simplicity, and partly because of incompletion:
* A shooting system for the characters paired up with a health script that can be assigned to any object in the game including the player itself
* Inventory system - able to pick up and drop off objects in the world.
