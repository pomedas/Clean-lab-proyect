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

Currently you'll see on the screen that there are already 10 chickens and a fox there. In the final version it won't be like that
but for now we decided to make sure all the triggers worked. In the next delivery we will implement the following:
- Chickens will move in a natural manner
- Implement movement of fox
- Foxes will appear twice in a game taking into account the time
- Chickens will 'die' when the fox is in the barnard or in the play area(1 chicken dies every 3 sec)
- Game Manager implementation (Message saying who won the game and other basic mecanics)


