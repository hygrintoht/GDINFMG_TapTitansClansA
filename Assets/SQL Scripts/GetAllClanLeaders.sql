USE taptitans_database;

SELECT clan.clan_name, player.username
FROM clan INNER JOIN clan_leader
ON clan.clanID = clan_leader.clanID
INNER JOIN player
ON player.playerID = clan_leader.playerID;