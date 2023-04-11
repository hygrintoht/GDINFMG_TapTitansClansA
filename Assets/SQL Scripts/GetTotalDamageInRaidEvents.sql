USE taptitans_database;

SELECT clan.clan_name, total_damage_list.total_damage
FROM (SELECT raid_events.clanID as raid_clan, SUM(raid_player.damage) AS total_damage
	  FROM raid_event_leaderboard AS raid_player INNER JOIN clan_raid_events AS raid_events
	  ON raid_player.raidID = raid_events.raidID
	  GROUP BY raid_events.clanID) AS total_damage_list
INNER JOIN clan
ON clan.clanID = total_damage_list.raid_clan;