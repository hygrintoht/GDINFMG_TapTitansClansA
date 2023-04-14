USE taptitans_database;

# CLAN
INSERT INTO clan (clan_invite_code, clan_name, num_raids_completed, num_clan_morale, required_stage, required_raid_level, privacy)
VALUES ('flwper', 'TheFirst', 2, 10, 500, 1, 'private');

INSERT INTO clan (clan_invite_code, clan_name, num_raids_completed, num_clan_morale, privacy)
VALUES ('poietc', 'Tap Addicts', 1, 5, 'public');

INSERT INTO clan (clan_invite_code, clan_name, num_raids_completed, num_clan_morale, required_stage, privacy)
VALUES ('zziour', 'MastersOfTheTouch', 4, 10, 1000, 'private');

INSERT INTO clan (clan_invite_code, clan_name, privacy)
VALUES ('yweiof', 'CasualsOnly', 'public');

# PLAYER
INSERT INTO player (username, email, clanID, country, title, max_prestige_stage, artifacts_collected, crafting_power, 
					total_pet_levels, skill_points_owned, play_time, last_played, installation_date)
VALUES ('Anivia', 'ice-birb@gmail.com', 1, 'PHL', 'Titan Slayer', 2000, 20, 5, 20, 20, '325:43:26', '2022-06-12 11:32:12', '2020-08-24');

INSERT INTO player (username, email, clanID, country, title, max_prestige_stage, artifacts_collected, crafting_power, 
					total_pet_levels, skill_points_owned, play_time, last_played, installation_date)
VALUES ('Volibear', 'buffed_guardian@gmail.com', 1, 'PHL', 'Champion', 1800, 15, 3, 18, 15, '256:26:41', '2022-06-05 17:10:54', '2020-12-28');

INSERT INTO player (username, email, clanID, country, max_prestige_stage, artifacts_collected,  
					total_pet_levels, play_time, last_played, installation_date)
VALUES ('TreeWizard', 'anotha.balloon@gmail.com', 2, 'PHL', 1000, 12, 10, '101:01:01', '2022-05-26 06:26:12', '2021-02-25');

INSERT INTO player (username, clanID, country, max_prestige_stage, artifacts_collected, total_pet_levels, play_time, last_played, installation_date)
VALUES ('Major Stevens', 3, 'PHL', 1600, 8, 6, '156:19:40', '2022-06-01 15:01:30', '2021-05-05');

INSERT INTO player (username, clanID, country, max_prestige_stage, artifacts_collected, total_pet_levels, play_time, last_played, installation_date)
VALUES ('Taps', 4, 'PHL', 400, 1, 4, '54:23:54', '2022-05-26 18:10:56', '2022-04-19');

INSERT INTO player (username, email,  country, play_time, last_played, installation_date)
VALUES ('newbie', 'baby-wheels@gmail.com', 'PHL', '5:07:10', '2022-06-12 04:46:06', '2022-06-07');

# CLAN LEADER
INSERT INTO clan_leader (clanID, playerID, is_grandmaster)
VALUES (1, 1, true);

INSERT INTO clan_leader (clanID, playerID, is_grandmaster)
VALUES (2, 3, true);

INSERT INTO clan_leader (clanID, playerID, is_grandmaster)
VALUES (3, 4, true);

INSERT INTO clan_leader (clanID, playerID, is_grandmaster)
VALUES (4, 5, true);

# RAIDS
INSERT INTO clan_raid_event(titan_name, clanID)
VALUES ('Darius', 1);

INSERT INTO clan_raid_event(titan_name, clanID)
VALUES ('Sion', 1);

INSERT INTO clan_raid_event(titan_name, clanID)
VALUES ('Pops', 2);

INSERT INTO clan_raid_event(titan_name, clanID)
VALUES ('Jukk the Overseer', 3);

INSERT INTO clan_raid_event(titan_name, clanID)
VALUES ('Salty Sam', 3);

INSERT INTO clan_raid_event(titan_name, clanID)
VALUES ('Flame Spikes', 3);

INSERT INTO clan_raid_event(titan_name, clanID)
VALUES ('Guur', 3);

# RAID SCORES
INSERT INTO raid_score (raidID, playerID, attacks, damage)
VALUES (1, 1, 200, 1000000);

INSERT INTO raid_score (raidID, playerID, attacks, damage)
VALUES (2, 1, 250, 10000000);

INSERT INTO raid_score (raidID, playerID, attacks, damage)
VALUES (2, 2, 140, 100000);

INSERT INTO raid_score (raidID, playerID, attacks, damage)
VALUES (3, 3, 120, 80000);

INSERT INTO raid_score (raidID, playerID, attacks, damage)
VALUES (4, 4, 200, 1000000);

INSERT INTO raid_score (raidID, playerID, attacks, damage)
VALUES (5, 4, 200, 1000000);

INSERT INTO raid_score (raidID, playerID, attacks, damage)
VALUES (6, 4, 200, 1000000);

INSERT INTO raid_score (raidID, playerID, attacks, damage)
VALUES (7, 4, 200, 1000000);