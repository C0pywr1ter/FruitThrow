# FruitThrow-Game
Unity Game: This game is a cooking chef simulator.Player has to grab the ingridients pressing the left button from the frigge and spawn them by spawn button. the customers orders can be fulfilled by throwing the right ingridients in the pot. 
After compliting an order the pot goes to cooking mode which can be checked by pressing the right button.Each day player can buy new pots up to 4 to increase the kitchens productivity.
The game is created to be played on Android phones by swiping on the sceen, the logic of the swiped is written in SwipeScript.
Data is being saved after each game day in SaveData script.
The  PotScript is responsible for checking which ingridients are thrown in the pot. It also has a CoockingCourutine, the script "cooks the dishes" using a timer.
The UIManager Changes pots (if player has more than 1), shows money profit, manages the ui of game and buttons, finish orders, has method for scoring an Ingridient in the pot, lets player buy pots, changes customer orders
SunController controls the Sun movement and in-game time.  
