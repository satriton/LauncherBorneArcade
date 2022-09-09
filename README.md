# LauncherBorneArcade
	The Launcher allows you to view club news and launch all 7Fault games from one place.

## Utilisation:
	

	Structure to be respected:
		├── Launcher
		|	├── Games
		|	|	├── Jeu1
		|	|	|	├── GameMeta
		|	|	|	|	├── logo.png
		|	|	|	├── ...
		|	|	├── Jeu2
		|	|	|	├── GameMeta
		|	|	|	|	├── logo.png
		|	|	|	├── ...
		|	|	├── ...
		|	|	├── news.txt
		|	├── ...


	For the games to be detected by the launcher, you just have to drag their builds in the Games/ folder.

	There is a zone for the news of the club (the text displayed is the one of Games/news.txt)

	For the good logo of the game to be displayed, you have to put a logo.png file in Games/<game name>/GameMeta/ (otherwise a default 7Fault logo will be shown)


## TODO:
	-Classement des jeux les plus lancés/nouveaux pour les mettre en premier
	-exctract l'image de l'exe si elle n'existe pas dans le GameMeta (https://ourcodeworld.com/articles/read/1422/how-to-extract-the-icon-from-an-executable-with-c-sharp-in-winforms)
	-changer la police du launcher
	-gestion volume ordi depuis launcher
	-Ajout d'une description pour les jeux
	-prendre le nom depuis GameMeta nomJeu.png (à la place de logo.png) ?


## Bug à fix/préventions:
	-Mettre les cadres de preview de jeu gris quand nbgames < 3
	-Tester pour choisir le bon exe si il y en a plusieurs dans les dossiers du jeu
	-Trycatch pour les loadsAssets ?

	
## Last Update:
	#Patron MVC
	#Simplification navigation entre les jeux
	#Fix bug affichage quand nbgame < 3
	#Compteur de page
	#repasser le launcher au premier plan quand on ferme le jeu
	#Inputsystem pour les contrôles	
	#Ecrant qui se grises quand on a lancé un jeu avec message adéquat
