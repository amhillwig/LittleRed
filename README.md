# Little Red Riding 
 - [Original Story](https://americanliterature.com/childrens-stories/little-red-riding-hood)

 - [Dialogue Script](https://docs.google.com/document/d/1SUhaQq66Av_28JC33XNLUOK8t7B5f1Np7A0H4xt5Mt8/edit?usp=drivesdk)
  
## Plot - 

  *[Opening dialogue cutscene]* The first scene begins with Red's mother telling her that she needs to take Gma her meds and a dinner because she's ill. She says to go straight there and never stray from the path, so she may get there before nightfall. Red tries to reason with her mother that it may be too dangerous for her to go alone. Mom dismisses Red's concerns and tells her to just listen and go. [Red is given a basket containing the key items] *[This opens the inventory feature, which will appear at the top of the screen]* Red has a thought bubble that implies that this is typical of her mother. [Player gains control of Red] Red may speak to the villagers, but their interactions will be limited to greetings and a scripted baker interaction, where Red will receive a baked good. *[ensures death ending happens first and shows that villagers are interactable]* 
  
  She leaves through the gates of the village and goes into the forest. The forest area is mostly one long horizontal wooded path, with occasional trails (seen only when it's still light out) that may hold something interesting. When the player tries to go off the path in the first run, Red will put up a thought bubble that will reiterate Mom's advice. *[create sense of curiosity but ensure death scenario and keep focus to main task]* Eventually, Red runs into the wolf who smelled the food she was carrying. She is prompted by the wolf to give him one of her food items [*either packed dinner or baked good will work]*, which he eats and he goes away. *[shows what can attract him, way to avoid him if approached, and possible scheme possibility to poison wolf]* Along the rest of the way through the woods, there may be visual signs that the wolf is following Red.
  
  Red arrives at Gma's, where she's alive and reading in her home. Red goes up to Gma to talk to her and tells her that she traveled all on her own to deliver what she needs. Gma tells Red that she's a very brave, smart child and is excited to spend the night with her. *[make Gma into maternal figure and build player bond with her to make her death meaningful]* Gma says they should eat the food Red brought and play games. Gma sends Red on a quick fetch quest to grab something outside within the fenced area around the home. **[possibly a food item from a garden or flowers bc ones on table or dead]** Red goes around the house to grab the item, when she hears some kind of crash/thud. When she gets back to the front door, she has a thought bubble wondering if the sound was something entering the house beause she left the door open. *[introduce door shutting idea and outcome]* She enters the house, but Gma is no longer there, but her bedroom door is now ajar. 
  
  *[Death ending cutscene]* When Red enters the bedroom, the classic Wolf-Gma disguise plays out. The wolf has on Gma's cardigan with a bonnet and is sat in her bed to try to look inconspicuous. Red comments of "Gma's" ears, eyes, hands, and teeth. As the wolf says "better to eat you with," Red has caught on to the Wolf and begs to know what he did to her Gma. The wolf sneers and pounces on Red to eat her, revealing blood from also eating Gma. *[mature rating fepending on how graphic]*

  The game goes to black, with text bubbles calling out Red's name. *[New game begins]* The game restarts at the end of Red's introduction conversation with Mom, with Mom asking "are you listening to me?" New converstaion plays out where Red urges her mother that it isn't safe to go on her own. Mom is, again, dismissive and closes the door on Red, reminding her of her advice, which has now been proven unhelpful. Red, with a new mission, continues onward.
  

### Village - 

  Interactions
  - The Baker : The town’s main provider of baked goods. Upon entering her shop, you can chat with her about your journey and she will offer some baked goods to take along with you on the long walk. You can accept or decline. You can ask her for alcohol and she will reluctantly give it to you if you can bring her some non-poisonous berries from the forest. She doesn’t remember which berry color is poison, though.
      - needed : baker character sprite, dialogue with choice variables, baked goods sprite(s), bakery background maybe
  - The Woodsman : The town's axe-bearing lumberjack. He will be standing outside his home with his axe leaned up next to him. Talking to him, he will give some vague advice on the first run or for help via escort in order to save Gma. He is blunt and uncharitable, so will need to be convinced to help. To get him to come with you, he requires you to pay him one silver coin and some alcohol. 
      - needed : woodsman character sprite (walking and dialogue), axe sprite, code to make woodsman follow red, variable for if he's willing to help,
  - The Herbalist : The town doctor that deals with herbs. Knocking on his door will prompt dialogue screen outside of his shop, where he kindly says he is busy and asks if there's something you need. He can help with giving you a silver coin via a fetch quest for forest herbs, inform you about which berry is poison, as well as help you make a poison bait for the wolf.
      - needed : herbalist character sprite, mushroom sprite, silver coin sprite, fetch quest variable, dialogue options
  - Lantern - Sat outside of Red's home. Without the lantern, Red can barely see in the forest and can't go off of the trail even after the first run. Using the lantern will allow the player to explore other parts of the forest even when it turns to night. The light will, however, attract the wolf to follow.
      - needed : lantern sprite, light effect


### Forest
  - Mushrooms - Can be found in bunches in some of the dark parts of the forest. They can be sold to the Herbalist in a fetch quest from silver. You will need five to beat .
      - needed : mushroom sprite, consumption/death variable, spawn randomization?
  - Berries - Can be collected from bushes throughout the forest map. There are four different colors that spawn (red, orange, blue, and purple), with orange being poionous. You are able to consume all types, but only find out which is poison by asking the herbalist or by eating them. They can be added to baked goods for a scheme.
      - needed : berry bush sprite, berry sprite, color randomization
  - Trail (Riverbank) : At the end a trail leading off of the main path heading south, a river can be found. The final mushroom can be found there. 
      - needed : 


## Endings :
- First death ending
- First encounter death ending -
     - if you go into the forest to forage and don't have food on you to give the wolf, he'll eat you then and there
- Good ending -
     - How to get ending:
         - talk to the woodsman
         - you beg him to come with you to Gma's house and protect you from the wolf, he will say it's a pretty hefty favor, but he'll do it for a silver coin and some wine
         - you and your mom don't have that kind of money, he tells you it's not his problem, so ask around
         - talk to the herbalist
         - ask him if you can have a silver coin and wine, he says he doesn't have wine, but he does have money. he won't give it for free and will give it to you for 5 mushrooms
         - you go to the forest and look for some, to get the last one you need to go a little further in and down a path
         - at the end of the path, you encounter the wolf who says he's hungry and asks you can do about that
         - give him food you have in your inventory,
            - if it's grandma's dinner you will need to get more food, otherwise give the food the baker gave you, if its poisoned it will increase your time
         - he will leave appeased
         - talk to the baker
         - ask for a silver coin and wine, she says she doesn't have any silver to spare but has wine, she'll give it to you if you get her some safe berries for pie
         - return herbalist about berries, give the mushrooms, he will give you a coin
         - go to forest and collect the right berries
         - return to baker and she will give you wine
         - return to woodsman and he will follow you through the woods
         - go to Gma's house with woodman
         - Gma's fetch quest again?
         - when you get back wolf has been killed by woodman in the living room
         - happy ending yippee

