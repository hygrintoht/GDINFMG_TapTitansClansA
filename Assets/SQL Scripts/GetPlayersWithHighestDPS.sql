use taptitans_database;

SELECT player.username, score1.damage
FROM player INNER JOIN raid_score AS score1
WHERE player.playerID = score1.playerID
AND score1.damage >= ALL 
	(SELECT score2.damage FROM raid_score AS score2);