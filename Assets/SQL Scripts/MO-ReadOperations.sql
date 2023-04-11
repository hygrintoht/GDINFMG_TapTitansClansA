USE taptitans_database;

SELECT * FROM clan;

SELECT * FROM player;

SELECT * FROM clan_leader;

SELECT * FROM clan_raid_event;

SELECT * FROM raid_score;

SELECT * FROM chat;

# get all clan members
SELECT clan.clan_name, player.username, player.country, player.title
FROM player INNER JOIN clan
ON player.clanID = clan.clanID;

# get all clan leaders
SELECT clan.clan_name, player.username
FROM clan INNER JOIN clan_leader
ON clan.clanID = clan_leader.clanID
INNER JOIN player
ON player.playerID = clan_leader.playerID;

# get all raids
SELECT clan.clan_name, raid.raidID, raid.titan_name
FROM clan_raid_event AS raid
INNER JOIN clan 
ON raid.clanID = clan.clanID;

# get all players in raids
SELECT player.username, raid.raidID, score.damage
FROM raid_score as score
INNER JOIN clan_raid_event AS raid
ON score.raidID = raid.raidID 
INNER JOIN player
ON score.playerID = player.playerID;

# get all chat messages from players
SELECT player.username, chat.messageType, chat.message
FROM player INNER JOIN chat
ON player.playerID = chat.playerID;

# get total members in clan
SELECT clan.clan_name, COUNT(DISTINCT player.playerID) AS num_clan_members
FROM player INNER JOIN clan
ON player.clanID = clan.clanID
GROUP BY clan.clanID;

# get total damage in each raid
SELECT clan.clan_name, total_damage_list.total_damage
FROM (SELECT raid_events.clanID as raid_clan, SUM(raid_player.damage) AS total_damage
	  FROM raid_event_leaderboard AS raid_player INNER JOIN clan_raid_events AS raid_events
	  ON raid_player.raidID = raid_events.raidID
	  GROUP BY raid_events.clanID) AS total_damage_list
INNER JOIN clan
ON clan.clanID = total_damage_list.raid_clan;

# get total messages sent by each player
SELECT player.username, players_list.total_messages
FROM (SELECT chat.playerID, COUNT(chat.message) AS total_messages
	  FROM chat GROUP BY chat.playerID) players_list
INNER JOIN player ON player.playerID = players_list.playerID;

# get all titan slayers in clan
SELECT player.clanID, COUNT(player.title) as titan_slayers
FROM player 
WHERE player.title = 'Titan Slayer' AND
player.clanID IN (SELECT clan.clanID FROM clan)
GROUP BY player.clanID;

# get the player with highest dps from each raid
SELECT player.username, score1.damage
FROM player INNER JOIN raid_score AS score1
WHERE player.playerID = score1.playerID
AND score1.damage >= ALL 
	(SELECT score2.damage FROM raid_score AS score2);