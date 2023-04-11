USE taptitans_database;

SELECT player.username, chat.messageType, chat.message
FROM player INNER JOIN chat
ON player.playerID = chat.playerID;