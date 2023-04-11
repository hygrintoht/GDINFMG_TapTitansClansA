USE taptitans_database;

SELECT clan.clan_name, COUNT(DISTINCT player.playerID) AS num_clan_members
FROM player INNER JOIN clan
ON player.clanID = clan.clanID
GROUP BY clan.clanID;