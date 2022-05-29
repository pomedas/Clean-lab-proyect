Summary of the Game:
Catching Chickens is a 2 Player competitive interactive game.
Each player has to bring chickens to their barnyard by moving around the play area.
The player with most chickens wins.
Bringing a chicken to your banyard adds 1 point
Twice in a game a fox will appear which can be catched and released into the opponent's barnyard
A fox will eat chickens in the field or in the barnyard if it's not stopped
If a chicken in your barnyard dies you lose points
A player can battle the fox to stop it from eating chickens
Near the end of the game a golden chicken will appear which will grant the player who captures it 3 points

From past delivery:
Assets:
For this delivery we created 3 types of assets:
 - The animals of the game with 2 types of animations idle and walk:
	· Chicken
	· GoldenChicken
	· Fox
 - The indicators:	//For indicating that an animal has been caught by the player
	· Chicken_caught
	· GoldenChicken_caught
	· Fox_caight
	· Fox_battle
 - Scene:
	· Fences that delimit the player's barnyards (we choose black and white to make the tracking easier)
	· Background field
	· Transparent barnyard objects

Animations:
As we mentioned we have two animations idle and walk for both chickens and foxes. You'll be able to see the following:
- There are chickens that are moving in circles --> they're using the walk animation
- Chickens that don't move use the idle animation which doesn't have the movement of the wings
- Fox will play the idle animation (moving tail) when it's not moving
- If you move it manually, you'll see that it does it's walk animation (strong head movement + movement of legs)
PS: we couldn't finish the fox's movement on time which is why you'll need to move it manually to see that animation


Gameplay:
We started adding some basic gamplay features, mainly thouse related to interaction so that we can test team in the near seminar:
- Player can catch any animal by going near them. When doing so a colored square will appear around them:
	· Orange: regular chicken
	· Gold: golden chicken
	· Brown: fox
	· Green: battling fox

- A chicken can ONLY be released into the player's barnyard (red --> black barnyard, blue --> white barnyard)
- Chickens in a barnyard cannot be stolen / recaught
- Fox can be caught and can ONLY be released in the oponents barnyard
- If player collides with fox inside it's barnyard it will battle it for 5 seconds
- When animal is released into barnyard it moves to a random position inside the barnyard
- You can't catch more than one animal at the same time
- We implemented a timer (although it cannot be seen yet on the screen, the log will show a game over message after 4 min)
- We implemented a point system 

New things on this delivery:
- Chickens will move in a natural manner following similar to how electrons move. Chickens will try to:
	· Avoid fox
	· Avoid player
	· Avoid corners (as they would be trapped by a player easily if they went to a corner
	· Avoid other chickens
- Implemented movement of fox
- Foxes will appear twice in a game taking into account the time (every 65 seconds, first fox appears after 1min)
- Chickens will 'die' when the fox is in the barnard or in the play area(1 random chicken dies every 10 sec)
- Game Manager implementation:
	· When there are 4 chickens left on the playarea or 1 min left, one of the chickens turns into a goldenChicken
	· Message saying who won the game
- The timer can now be seen as well as the point system.
- We have added various sound effects:
	· When chicken dies
	· When chicken runs
	· When chicken gets caught or released
	· When fox gets caught´/released
	· When you fight the fox
	· When the fox gets killed
	· Background music throughout the game
	· Main menu music
- We have also implemented a main menu, which for now is a green background like a title screen and there are two dots, a black and a white one.
  The game starts when the players go to their respective colors (black for red and white for blue) and then the scene will change to the game scene.
  The main menu also has its own music.


