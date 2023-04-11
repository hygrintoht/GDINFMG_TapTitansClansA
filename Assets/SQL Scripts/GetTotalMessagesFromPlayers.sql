USE taptitans_database;

SELECT player.username, players_list.total_messages
FROM (SELECT chat.playerID, COUNT(chat.message) AS total_messages
	  FROM chat GROUP BY chat.playerID) players_list
INNER JOIN player ON player.playerID = players_list.playerID;