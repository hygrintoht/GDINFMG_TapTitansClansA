USE taptitans_database;

SELECT clan.clan_name, player.username, player.country, player.title
FROM player INNER JOIN clan
ON player.clanID = clan.clanID;