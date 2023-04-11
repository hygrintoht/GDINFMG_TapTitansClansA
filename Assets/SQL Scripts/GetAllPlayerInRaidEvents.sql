USE taptitans_database;

SELECT player.username, raid.raidID, score.damage
FROM raid_score as score
INNER JOIN clan_raid_event AS raid
ON score.raidID = raid.raidID 
INNER JOIN player
ON score.playerID = player.playerID;