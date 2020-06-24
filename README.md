# Rs3ServerSideXPTracker




TODO:

	-ClanPoints System (awaiting reply)
	-Tic_Tac_Toe
	-Skilling Competitions (in development)
	-PVM Competitions? (might be extremelly hard to protect/Implement)
	-PVM HighScores? (might be extremelly hard to protect/Implement)
	-Tic_Tac_Toe Ranking Ladder (LuL)
	-DominionTower HighScores
	-ClueHunting Rankings (By Clue Count, can be separate tables for clue types)
	-RuneScore Ranking





BEFORE CHECKING STATS OR GAINS YOU SHOULD LOBBY SO THE RSAPI UPDATES YOUR XP VALUES!


BotPrefix - Every Message staring with !SHW will be considered a bot command

	!SHW


ADMIN COMMANDS:

[]


HOST

	Host Prefix: 

		host

		Usage - !SHW host command

	COMMANDS:
		
		new

			Usage - !shw host new -name "Skilling Comp Name Here" -start "21/06/2020 12:00:00" -end "22/06/2020 00:00:00"

				-name is the name of the competition
				-start is the date and time for it to start
				-end is the date and time for it to end


USER

	COMMANDS:

		gains

			Usage - !SHW gains username (Reponds with the xp gained since the last time the command was used)
			Usage - !SHW gains -since "2020-06-21" username (Reponds with the xp gained since the date specified if theres a record available)

			username not required IF command link was used previously		

		new

			Usage - !SHW new username

		Start tracking a new Account

		stats 

			Usage - !SHW stats username

			username not required IF command link was used previously

		Responds with the current xp and levels

		commands

			Usage - !SHW commands

		Responds with a link to the list of Commands

		link

			Usage - !SHW link username

		Creates a link between the discord account the rs3 username

