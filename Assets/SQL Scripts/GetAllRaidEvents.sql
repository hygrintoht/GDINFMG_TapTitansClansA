USE taptitans_database;

SELECT clan.clan_name, raid.raidID, raid.titan_name
FROM clan_raid_event AS raid
INNER JOIN clan 
ON raid.clanID = clan.clanID;