USE taptitans_database;

SELECT player.clanID, COUNT(player.title) as titan_slayers
FROM player 
WHERE player.title = 'Titan Slayer' AND
player.clanID IN (SELECT clan.clanID FROM clan)
GROUP BY player.clanID;